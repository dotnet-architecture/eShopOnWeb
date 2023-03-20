variable "name" {
  description = "(Required) Specifies the name of the log analytics workspace"
  type = string
}

variable "resource_group_name" {
  description = "(Required) Specifies the resource group name"
  type = string
}

variable "location" {
  description = "(Required) Specifies the location of the log analytics workspace"
  type = string
}

variable "sku" {
  description = "(Optional) Specifies the sku of the log analytics workspace"
  type = string
  default = "PerGB2018"
  
  validation {
    condition = contains(["Free", "Standalone", "PerNode", "PerGB2018"], var.sku)
    error_message = "The log analytics sku is incorrect."
  }
}

variable "tags" {
  description = "(Optional) Specifies the tags of the log analytics workspace"
  type        = map(any)
  default     = {}
}

variable "retention_in_days" {
  description = " (Optional) Specifies the workspace data retention in days. Possible values are either 7 (Free Tier only) or range between 30 and 730."
  type        = number
  default     = 30
}