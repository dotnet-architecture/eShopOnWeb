variable "resource_prefix" {
  description = "Specifies a prefix for all the resource names."
  type        = string
}

variable "app_image" {
  description = "App Containner image"
  type        = string
}

variable "location" {
  description = "(Required) Specifies the supported Azure location where the resource exists. Changing this forces a new resource to be created."
  type        = string
  default     = "WestEurope"
}

variable "resource_group_name" {
   description = "Name of the resource group in which the resources will be created"
   default     = "-rg"
}

variable "tags" {
  description = "(Optional) Specifies tags for all the resources"
  default     = {
    createdWith = "Terraform"
  }
}

variable "kv_sku_name" {
  default = "standard"
}

variable "log_analytics_workspace_name" {
  description = "Specifies the name of the log analytics workspace"
  default     = "-logws"
  type        = string
}

variable "log_analytics_retention_days" {
  description = "Specifies the number of days of the retention policy for the log analytics workspace."
  type        = number
  default     = 30
}

variable "application_insights_name" {
  description = "Specifies the name of the application insights resource."
  default     = "-insights"
  type        = string
}

variable "application_insights_application_type" {
  description = "(Required) Specifies the type of Application Insights to create. Valid values are ios for iOS, java for Java web, MobileCenter for App Center, Node.JS for Node.js, other for General, phone for Windows Phone, store for Windows Store and web for ASP.NET. Please note these values are case sensitive; unmatched values are treated as ASP.NET by Azure. Changing this forces a new resource to be created."
  type        = string
  default     = "web"
}

variable "vnet_name" {
  description = "Specifies the name of the virtual network"
  default     = "-vnet"
  type        = string
}

variable "vnet_address_space" {
  description = "Specifies the address prefix of the virtual network"
  default     =  ["10.0.0.0/16"]
  type        = list(string)
}

variable "aca_subnet_name" {
  description = "Specifies the name of the subnet"
  default     = "ContainerApps"
  type        = string
}

variable "aca_subnet_address_prefix" {
  description = "Specifies the address prefix of the Azure Container Apps environment subnet"
  default     = ["10.0.0.0/20"]
  type        = list(string)
}

variable "private_endpoint_subnet_name" {
  description = "Specifies the name of the subnet"
  default     = "PrivateEndpoints"
  type        = string
}

variable "private_endpoint_subnet_address_prefix" {
  description = "Specifies the address prefix of the private endpoints subnet"
  default     = ["10.0.16.0/24"]
  type        = list(string)
}

variable "storage_account_name" {
  description = "(Optional) Specifies the name of the storage account"
  default     = "storage"
  type        = string
}

variable "storage_account_replication_type" {
  description = "(Optional) Specifies the replication type of the storage account"
  default     = "LRS"
  type        = string

  validation {
    condition = contains(["LRS", "ZRS", "GRS", "GZRS", "RA-GRS", "RA-GZRS"], var.storage_account_replication_type)
    error_message = "The replication type of the storage account is invalid."
  }
}

variable "storage_account_kind" {
  description = "(Optional) Specifies the account kind of the storage account"
  default     = "StorageV2"
  type        = string

   validation {
    condition = contains(["Storage", "StorageV2"], var.storage_account_kind)
    error_message = "The account kind of the storage account is invalid."
  }
}

variable "storage_account_tier" {
  description = "(Optional) Specifies the account tier of the storage account"
  default     = "Standard"
  type        = string

   validation {
    condition = contains(["Standard", "Premium"], var.storage_account_tier)
    error_message = "The account tier of the storage account is invalid."
  }
}

variable "managed_environment_name" {
  description = "(Required) Specifies the name of the managed environment."
  type        = string
  default     = "-appenv"
}

variable "internal_load_balancer_enabled" {
  description = "(Optional) Should the Container Environment operate in Internal Load Balancing Mode? Defaults to false. Changing this forces a new resource to be created."
  type        = bool
  default     = false
}

variable "dapr_name" {
  description = "(Required) Specifies the name of the dapr component."
  type        = string
  default     = "statestore"
}

variable "dapr_component_type" {
  description = "(Required) Specifies the type of the dapr component."
  type        = string
  default     = "state.azure.blobstorage"
}

variable "dapr_ignore_errors" {
  description = "(Required) Specifies  if the component errors are ignored."
  type        = bool
  default     = false
}

variable "dapr_version" {
  description = "(Required) Specifies the version of the dapr component."
  type        = string
  default     = "v1"
}

variable "dapr_init_timeout" {
  description = "(Required) Specifies the init timeout of the dapr component."
  type        = string
  default     = "5s"
}

variable "dapr_scopes" {
  description = "(Required) Specifies the init timeout of the dapr component."
  type        = list
  default     = ["eshop-web"]
}

variable "container_name" {
  description = "Specifies the name of the container in the storage account."
  type        = string
  default     = "state"
}

variable "container_access_type" {
  description = "Specifies the access type of the container in the storage account."
  type        = string
  default     = "private"
}
