# Base resources for an Azure's Application Landing Zone

#--------------------------------------------------------------
#   Gathering data
#--------------------------------------------------------------
data "azurerm_resources" "nsg_resources" {
  resource_group_name = var.rg_name
  type                = "Microsoft.Network/networkSecurityGroups"
}
data "azurerm_resources" "kv_resources" {
  resource_group_name = var.rg_name
  type                = "Microsoft.KeyVault/vaults"
}
data "azurerm_resources" "st_resources" {
  resource_group_name = var.rg_name
  type                =  "Microsoft.Storage/storageAccounts"
}
data "azurerm_resources" "law_resources" {
  resource_group_name = var.rg_name
  type                = "Microsoft.OperationalInsights/workspaces"
}
data "azurerm_resources" "appi_resources" {
  resource_group_name = var.rg_name
  type                = "Microsoft.Insights/components"
}
data "azurerm_resources" "vnet_resources" {
  resource_group_name = var.rg_name
  type                = "Microsoft.Network/virtualNetworks"
}


#--------------------------------------------------------------
#  Generating Resource Ids list
#--------------------------------------------------------------
locals {
  expanded_st_ids = concat(
    [ for st in data.azurerm_resources.st_resources.resources : "${st.id}/blobServices/default"],
    [ for st in data.azurerm_resources.st_resources.resources : "${st.id}/fileServices/default"],
    [ for st in data.azurerm_resources.st_resources.resources : "${st.id}/queueServices/default"],
    [ for st in data.azurerm_resources.st_resources.resources : "${st.id}/tableServices/default"]
  )
  

  resource_ids = concat(
    data.azurerm_resources.nsg_resources.resources[*].id,
    data.azurerm_resources.vnet_resources.resources[*].id,
    data.azurerm_resources.kv_resources.resources[*].id,
    data.azurerm_resources.st_resources.resources[*].id,
    local.expanded_st_ids,
    data.azurerm_resources.law_resources.resources[*].id,
    data.azurerm_resources.appi_resources.resources[*].id
  )

  diag_settings = { for res_id in local.resource_ids : 
    # "${reverse(split("/", res_id))[0]}" => res_id
    length(regexall("Services", res_id)) > 0 ? "${reverse(split("/", res_id))[2]}-${replace(reverse(split("/", res_id))[1], "Services", "")}" : "${reverse(split("/", res_id))[0]}" => res_id
  }
}

#--------------------------------------------------------------
#   Diagnostics settings
#--------------------------------------------------------------
# Gather the Diagnostic categories for the selected resources
data "azurerm_monitor_diagnostic_categories" "this" {
  for_each    = local.diag_settings

  resource_id = each.value
}

# Creates the Azure Diagnostics Setting for the Resources:
resource "azurerm_monitor_diagnostic_setting" "this" {
  for_each    = local.diag_settings

  name                            = "${each.key}-diag"
  target_resource_id              = each.value
  log_analytics_workspace_id      = var.law_id
  log_analytics_destination_type  = "AzureDiagnostics"
  storage_account_id              = var.st_id

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
#*/
