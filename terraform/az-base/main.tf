# Base resources for an Azure's Application Landing Zone

#--------------------------------------------------------------
#  Application Landing Zone
#--------------------------------------------------------------
#   Current configuration context
data azurerm_client_config this {}

#   Resource Group
resource azurerm_resource_group this {
  name     = "rg-cpchem-${var.team_name}-${var.iterator}"
  location = var.location

  tags = var.base_tags
}

#   Storage Account
resource azurerm_storage_account this {
  name                     = replace("stcpchem${var.team_name}${var.iterator}", "-", "")
  resource_group_name      = azurerm_resource_group.this.name
  location                 = azurerm_resource_group.this.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = azurerm_resource_group.this.tags
}

#   Key vault
resource azurerm_key_vault this {
  name                        = "kv-cpchem-${var.team_name}-${var.iterator}"
  location                    = azurerm_resource_group.this.location
  resource_group_name         = azurerm_resource_group.this.name
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.this.tenant_id
  soft_delete_retention_days  = 7
  purge_protection_enabled    = false
  enable_rbac_authorization   = true
  sku_name                    = "standard"

  tags = azurerm_resource_group.this.tags
}
# resource azurerm_role_assignment kv-admin {
#   principal_id          = data.azurerm_client_config.this.client_id
#   role_definition_name  = "Key Vault Administrator"
#   scope                 = azurerm_key_vault.this.id
# }

#--------------------------------------------------------------
#   Virtual Network
#--------------------------------------------------------------
resource "azurerm_virtual_network" "this" {
  name                = lower("vnet-cpchem-${var.team_name}-${var.iterator}")
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name
  address_space       = var.vnet_address_space
  dns_servers         = var.vnet_dns_servers

  tags = azurerm_resource_group.this.tags
}

#   Subnets
resource "azurerm_subnet" "this" {
  for_each = var.subnets

  name                                           = each.key
  resource_group_name                            = azurerm_resource_group.this.name
  address_prefixes                               = each.value["address_prefixes"]
  service_endpoints                              = lookup(each.value, "service_endpoints", null)
  private_endpoint_network_policies_enabled      = lookup(each.value, "pe_enable", false)
  private_link_service_network_policies_enabled  = lookup(each.value, "pe_enable", false)
  virtual_network_name                           = azurerm_virtual_network.this.name

  dynamic "delegation" {
    for_each = lookup(each.value, "delegation", [])
    content {
      name = lookup(delegation.value, "name", null)
      dynamic "service_delegation" {
        for_each = lookup(delegation.value, "service_delegation", [])
        content {
          name    = lookup(service_delegation.value, "name", null)
          actions = lookup(service_delegation.value, "actions", null)
        }
      }
    }
  }

  depends_on = [azurerm_virtual_network.this]
}

#-------------------------
# - Network Security Groups
#-------------------------
resource "azurerm_network_security_group" "this" {
  for_each            = var.subnets

  name                = lower("nsg-${each.key}")
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name

  tags                = azurerm_resource_group.this.tags
}

#-------------------------
# - Inbound & Outbound rules for the Workload subnet NSG
#-------------------------
resource "azurerm_network_security_rule" "workload_nsg" {
  resource_group_name         = azurerm_resource_group.this.name
  network_security_group_name = azurerm_network_security_group.this["workload-snet"].name

  for_each = coalesce(var.workload_security_rules, {})

  name                         = each.key
  description                  = lookup(each.value, "description", null)
  protocol                     = lookup(each.value, "protocol", null)
  direction                    = lookup(each.value, "direction", null)
  access                       = lookup(each.value, "access", null)
  priority                     = lookup(each.value, "priority", null)
  source_address_prefix        = lookup(each.value, "source_address_prefix", null)
  source_address_prefixes      = lookup(each.value, "source_address_prefixes", null)
  destination_address_prefix   = lookup(each.value, "destination_address_prefix", null)
  destination_address_prefixes = lookup(each.value, "destination_address_prefixes", null)
  source_port_range            = lookup(each.value, "source_port_range", null)
  source_port_ranges           = lookup(each.value, "source_port_ranges", null)
  destination_port_range       = lookup(each.value, "destination_port_range", null)
  destination_port_ranges      = lookup(each.value, "destination_port_ranges", null)
}

#-------------------------
#- Associate the Network Security Groups with their Subnets
#-------------------------
resource "azurerm_subnet_network_security_group_association" "this" {
  for_each = azurerm_network_security_group.this

  network_security_group_id = azurerm_network_security_group.this[each.key].id
  subnet_id                 = azurerm_subnet.this[each.key].id
}

#--------------------------------------------------------------
#   Log Analytics Workspace
#--------------------------------------------------------------
resource "azurerm_log_analytics_workspace" "this" {
  name                               = lower("law-cpchem-${var.team_name}-${var.iterator}")
  location                           = azurerm_resource_group.this.location
  resource_group_name                = azurerm_resource_group.this.name
  sku                                = var.law_sku
  retention_in_days                  = var.retention_in_days
  internet_ingestion_enabled         = true
  internet_query_enabled             = true
  daily_quota_gb                     = var.law_sku == "Free" ? 0.5 : var.law_daily_quota_gb
  reservation_capacity_in_gb_per_day = var.law_sku == "CapacityReservation" ? var.law_reservation_capacity_in_gb_per_day : null
  tags                               = azurerm_resource_group.this.tags
}

#--------------------------------------------------------------
#   Application Insights
#--------------------------------------------------------------
resource "azurerm_application_insights" "this" {
  name                                  = lower("appi-cpchem-${var.team_name}-${var.iterator}")
  location                              = azurerm_resource_group.this.location
  resource_group_name                   = azurerm_resource_group.this.name
  workspace_id                          = azurerm_log_analytics_workspace.this.id

  application_type                      = var.appi_application_type
  retention_in_days                     = var.retention_in_days
  daily_data_cap_in_gb                  = var.appi_daily_data_cap_in_gb
  daily_data_cap_notifications_disabled = var.appi_daily_data_cap_notifications_disabled
  sampling_percentage                   = var.appi_sampling_percentage
  disable_ip_masking                    = var.appi_disable_ip_masking
  force_customer_storage_for_profiler   = var.appi_force_customer_storage_for_profiler

  # Security control: Segment cloud resources and network traffic
  internet_ingestion_enabled            = true
  internet_query_enabled                = true
  local_authentication_disabled         = false

  tags                                  = azurerm_resource_group.this.tags
}

#--------------------------------------------------------------
#   Diagnostics settings
#--------------------------------------------------------------
# Create the list of resources to get Diagnostic Settings
locals {
  diag_resource_ids = concat([
      azurerm_key_vault.this.id,
      azurerm_virtual_network.this.id,
      azurerm_application_insights.this.id,
      azurerm_storage_account.this.id,
      "${azurerm_storage_account.this.id}/blobServices/default",
      "${azurerm_storage_account.this.id}/fileServices/default",
      "${azurerm_storage_account.this.id}/queueServices/default",
      "${azurerm_storage_account.this.id}/tableServices/default",
      azurerm_log_analytics_workspace.this.id
    ],
    [for x in azurerm_network_security_group.this : x.id]
  )

  diag_settings = { for res_id in local.diag_resource_ids : 
    length(regexall("Services", res_id)) > 0 ? "${reverse(split("/", res_id))[2]}-${replace(reverse(split("/", res_id))[1], "Services", "")}" : "${reverse(split("/", res_id))[0]}" => res_id
  }
}

# Gather the Diagnostic categories for the selected resources
data "azurerm_monitor_diagnostic_categories" "this" {
  for_each    = local.diag_settings

  resource_id = each.value
}

# Creates the Azure Diagnostics Setting for the Resources:
resource "azurerm_monitor_diagnostic_setting" "this" {
  for_each                   = local.diag_settings

  name                            = "${each.key}-diag"
  target_resource_id              = each.value
  log_analytics_workspace_id      = azurerm_log_analytics_workspace.this.id
  log_analytics_destination_type  = "AzureDiagnostics"
  storage_account_id              = azurerm_storage_account.this.id

  dynamic "enabled_log" {
    for_each = lookup(data.azurerm_monitor_diagnostic_categories.this, each.key)["logs"]
    content {
      category = enabled_log.value

      retention_policy {
        enabled = true
        days    = var.retention_in_days
      }
    }
  }

  dynamic "metric" {
    for_each = lookup(data.azurerm_monitor_diagnostic_categories.this, each.key)["metrics"]
    content {
      category = metric.value
      enabled  = true

      retention_policy {
        enabled = true
        days    = var.retention_in_days
      }
    }
  }
}

