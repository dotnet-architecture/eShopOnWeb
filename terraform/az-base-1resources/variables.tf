variable spn_tenant_id       {}
variable spn_subscription_id {}
variable spn_client_id       {}
variable spn_secret          {}


variable "team_name" {}
variable iterator {
  type = string
  description = "Iterator to be added to all created resources"
  default = "01"
}
variable "location" {}
variable "base_tags" {}

#   Virtual Network
variable "vnet_address_space" {
  type        = list(string)
  description = "(Required) Virtual network address space in the format of CIDR range."
}
variable "vnet_dns_servers" {
  type        = list(string)
  description = "(Optional) Virtual network DNS server IP address."
  default     = []
}

#   Subnets
variable "subnets" {
  description = "(Optional) The virtual network subnets with their properties:<br></br><ul><li>[map key] used as `name`: (Required) The name of the subnet. </li><li>`address_prefixes`: (Optional) The address prefixes to use for the subnet. </li><li>`pe_enable`: (Optional) Enable or Disable network policies for the private link endpoint & private link service on the subnet. </li><li>`service_endpoints`: (Optional) The list of Service endpoints to associate with the subnet. Possible values include: `Microsoft.AzureActiveDirectory`, `Microsoft.AzureCosmosDB`, `Microsoft.ContainerRegistry`, `Microsoft.EventHub`, `Microsoft.KeyVault`, `Microsoft.ServiceBus`, `Microsoft.Sql`, `Microsoft.Storage` and `Microsoft.Web`. </li><li>`delegation` </li><ul><li>`name`: (Required) A name for this delegation. </li><li>`service_delegation` </li><ul><li>`name`: (Required) The name of service to delegate to. Possible values include `Microsoft.ApiManagement/service`, `Microsoft.AzureCosmosDB/clusters`, `Microsoft.BareMetal/AzureVMware`, `Microsoft.BareMetal/CrayServers`, `Microsoft.Batch/batchAccounts`, `Microsoft.ContainerInstance/containerGroups`, `Microsoft.ContainerService/managedClusters`, `Microsoft.Databricks/workspaces`, `Microsoft.DBforMySQL/flexibleServers`, `Microsoft.DBforMySQL/serversv2`, `Microsoft.DBforPostgreSQL/flexibleServers`, `Microsoft.DBforPostgreSQL/serversv2`, `Microsoft.DBforPostgreSQL/singleServers`, `Microsoft.HardwareSecurityModules/dedicatedHSMs`, `Microsoft.Kusto/clusters`, `Microsoft.Logic/integrationServiceEnvironments`, `Microsoft.MachineLearningServices/workspaces`, `Microsoft.Netapp/volumes`, `Microsoft.Network/managedResolvers`, `Microsoft.PowerPlatform/vnetaccesslinks`, `Microsoft.ServiceFabricMesh/networks`, `Microsoft.Sql/managedInstances`, `Microsoft.Sql/servers`, `Microsoft.StoragePool/diskPools`, `Microsoft.StreamAnalytics/streamingJobs`, `Microsoft.Synapse/workspaces`, `Microsoft.Web/hostingEnvironments`, and `Microsoft.Web/serverFarms`. </li><li>`actions`:(Optional) A list of Actions which should be delegated. This list is specific to the service to delegate to. Possible values include `Microsoft.Network/networkinterfaces/*`, `Microsoft.Network/virtualNetworks/subnets/action`, `Microsoft.Network/virtualNetworks/subnets/join/action`, `Microsoft.Network/virtualNetworks/subnets/prepareNetworkPolicies/action` and `Microsoft.Network/virtualNetworks/subnets/unprepareNetworkPolicies/action`.</ul></ul> "
  type = map(object({
    address_prefixes  = list(string)
    pe_enable         = bool
    service_endpoints = list(string)
    delegation = list(object({
      name = string
      service_delegation = list(object({
        name    = string
        actions = list(string)
      }))
    }))
  }))
  default = {}
}

#   NSG Security rules
variable "workload_security_rules" {
  type = map(object({
    description                  = string
    protocol                     = string
    direction                    = string
    access                       = string
    priority                     = number
    source_address_prefix        = string
    source_address_prefixes      = list(string)
    destination_address_prefix   = string
    destination_address_prefixes = list(string)
    source_port_range            = string
    source_port_ranges           = list(string)
    destination_port_range       = string
    destination_port_ranges      = list(string)
  }))
  description = <<EOT
    <p>(Optional) The Network Security rules with their properties:</p>
    <ul>
      <li>[map key] used as `name`: (Required) The name of the security rule. This needs to be unique across all Rules in the Network Security Group.</li>
      <li>`description`: (Optional) A description for this rule. Restricted to 140 characters.</li>
      <li>`protocol`: (Required) Network protocol this rule applies to. Possible values include `Tcp`, `Udp`, `Icmp`, `Esp`, `Ah` or `*` (which matches all).</li>
      <li>`direction`: (Required) The direction specifies if rule will be evaluated on incoming or outgoing traffic. Possible values are `Inbound` and `Outbound`.</li>
      <li>`access`: (Required) Specifies whether network traffic is allowed or denied. Possible values are `Allow` and `Deny`.</li>
      <li>`priority`: (Required) Specifies the priority of the rule. The value can be between 100 and 4096. The priority number must be unique for each rule in the collection. The lower the priority number, the higher the priority of the rule.</li>
      <li>`source_address_prefix`: (Optional) CIDR or source IP range or `*` to match any IP. Tags such as `VirtualNetwork`, `AzureLoadBalancer` and `Internet` can also be used. This is required if `source_address_prefixes` is not specified.</li>
      <li>`source_address_prefixes`: (Optional) List of source address prefixes. Tags may not be used. This is required if `source_address_prefix` is not specified.</li>
      <li>`destination_address_prefix`: (Optional) CIDR or destination IP range or `*` to match any IP. Tags such as `VirtualNetwork`, `AzureLoadBalancer` and `Internet` can also be used. Besides, it also supports all available Service Tags like `Sql.WestEurope`, `Storage.EastUS`, etc. You can list the available service tags with: `az network list-service-tags --location northcentralus`. This is required if `destination_address_prefixes` is not specified.</li>
      <li>`destination_address_prefixes`: (Optional) List of destination address prefixes. Tags may not be used. This is required if `destination_address_prefix` is not specified.</li>
      <li>`source_port_range`: (Optional) Source Port or Range. Integer or range between `0` and `65535` or `*` to match any. This is required if `source_port_ranges` is not specified.</li>
      <li>`source_port_ranges`: (Optional) List of source ports or port ranges. This is required if `source_port_range` is not specified.</li>
      <li>`destination_port_range`: (Optional) Destination Port or Range. Integer or range between `0` and `65535` or `*` to match any. This is required if `destination_port_ranges` is not specified.</li>
      <li>`destination_port_ranges`: (Optional) List of destination ports or port ranges. This is required if `destination_port_range` is not specified.</li>
    </ul>
  EOT
  default = {
    "IBD-DenyAll" = {
      description                                  = "Inbound Deny All Traffic"
      priority                                     = 4090
      direction                                    = "Inbound"
      access                                       = "Deny"
      protocol                                     = "*"
      source_port_range                            = "*"
      source_port_ranges                           = null
      destination_port_range                       = "*"
      destination_port_ranges                      = null
      source_address_prefix                        = "*"
      source_address_prefixes                      = null
      destination_address_prefix                   = "*"
      destination_address_prefixes                 = null
      source_application_security_group_names      = null
      destination_application_security_group_names = null
    },
    "OBD-DenyAll" = {
      description                                  = "Outbound Deny All Traffic"
      priority                                     = 4090
      direction                                    = "Outbound"
      access                                       = "Deny"
      protocol                                     = "*"
      source_port_range                            = "*"
      source_port_ranges                           = null
      destination_port_range                       = "*"
      destination_port_ranges                      = null
      source_address_prefix                        = "*"
      source_address_prefixes                      = null
      destination_address_prefix                   = "*"
      destination_address_prefixes                 = null
      source_application_security_group_names      = null
      destination_application_security_group_names = null
    }
  }
}

#--------------------------
# - Log Analytics Workspace settings
#--------------------------
variable "law_sku" {
  type        = string
  description = "(Required) Specifies the SKU of the Log Analytics Workspace. Possible values are `Free`, `PerNode`, `Premium`, `Standard`, `Standalone`, `Unlimited`, and `PerGB2018`."
}
variable "retention_in_days" {
  type        = string
  description = "(Required) The workspace level data retention in days. Possible values range between `30` and `730 (2 years)`."
}
variable "law_daily_quota_gb" {
  type        = number
  description = "(Optional) The workspace daily quota for ingestion in `GB`. The default value `-1` indicates `unlimited`"
  default     = -1
}
variable "law_reservation_capacity_in_gb_per_day" {
  type        = number
  description = "(Optional) The capacity reservation level in `GB` for this workspace. Must be in increments of `100` between `100` and `5000`."
  default     = 100
}


#--------------------------
# - Application Insights settings
#--------------------------
variable "appi_application_type" {
  type        = string
  description = "(Required) Specifies the type of Application Insights to create."
}
variable "appi_daily_data_cap_in_gb" {
  type        = number
  description = "(Optional) Specifies the Application Insights component daily data volume cap in GB."
  default     = null
}
variable "appi_daily_data_cap_notifications_disabled" {
  type        = bool
  description = "(Optional) Specifies if a notification email will be send when the daily data volume cap is met."
  default     = true
}
variable "appi_sampling_percentage" {
  type        = number
  description = "(Optional) Specifies the percentage of the data produced by the monitored application that is sampled for Application Insights telemetry."
  default     = null
}
variable "appi_disable_ip_masking" {
  type        = bool
  description = "(Optional) By default the real client ip is masked as 0.0.0.0 in the logs. Use this argument to disable masking and log the real client ip."
  default     = false
}
variable "appi_force_customer_storage_for_profiler" {
  type        = bool
  description = "(Optional) Specifies if the Application Insights component force users to create their own storage account for profiling."
  default     = false
}
