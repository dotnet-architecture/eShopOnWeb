variable spn_tenant_id       {}
variable spn_subscription_id {}
variable spn_client_id       {}
variable spn_secret          {}


#   Dependencies
variable "key_vault_id" {
  type        = string
  description = "(Required) Specifies the existing Key Vault Id where you want to store AZ Sql Server Password and CMK Key."
}

#   General settings
variable "team_name" {}
variable iterator {
  type = string
  description = "Iterator to be added to all created resources"
  default = "01"
}

variable "additional_tags" {
  type        = map(string)
  description = "(Optional) A mapping of tags to assign to the resource."
  default     = null
}

#   SQL Server settings
variable "administrator_login_name" {
  type        = string
  description = "(Required) The administrator username of Azure SQL Server."
  default     = "sql-admin"
}
variable "azuresql_version" {
  type        = string
  description = "(Optional) Specifies the version of Azure SQL Server to use. Valid values are: `2.0` (for v11 server) and `12.0` (for v12 server)"
  default     = "12.0"
}
variable "assign_identity" {
  type        = bool
  description = "(Optional) Specifies whether to enable Managed System Identity for the Azure SQL Server."
  default     = true
}
variable "outbound_network_restriction_enabled" {
  type        = string
  description = "(Optional) Whether outbound network traffic is restricted for this server. Defaults to `false`."
  default     = true
}
variable "connection_policy" {
  type        = string
  description = "(Optional) The connection policy the server will use. Possible values are `Default`, `Proxy`, and `Redirect`. Defaults to `Default`."
  default     = null
}
variable "cmk_enabled_transparent_data_encryption" {
  type        = bool
  description = "(Optional) Enable Azure SQL Transparent Data Encryption (TDE) with customer-managed key?"
  default     = true
}
