#   Base variables values
team_name = "team-a"
iterator = "01"
location = "southcentralus"
base_tags = {
    test_by = "emberger"
    cust = "CPChem"
    proj = "TerraformImmersionDays"
  }

#   Virtual Network values
vnet_address_space       = ["10.3.0.0/16"]
subnets = {
  "workload-snet" = {
    address_prefixes  = ["10.3.1.0/24"]
    pe_enable         = false
    service_endpoints = ["Microsoft.Sql", "Microsoft.ServiceBus", "Microsoft.Web"]
    delegation        = []
  },
  # "loadblancer-snet" = {
  #   address_prefixes  = ["10.3.2.0/24"]
  #   service_endpoints = null
  #   pe_enable         = false
  #   delegation        = []
  # },
  "pe-snet" = {
    address_prefixes  = ["10.3.5.0/24"]
    pe_enable         = true
    service_endpoints = null
    delegation        = []
  }
}

#   Log Analytics Workspace values
law_sku = "PerGB2018"
retention_in_days = 30

#   Application Insights values
appi_application_type = "web"
