variable spn_tenant_id       {}
variable spn_subscription_id {}
variable spn_client_id       {}
variable spn_secret          {}

variable rg_name          {}
variable mssql_server_id  {}
variable key_vault_id     {}
variable application_insights_id {}

variable team_name {}
variable iterator {
  type = string
  description = "Iterator to be added to all created resources"
  default = "01"
}
variable additional_tags {
  type        = map(string)
  description = "(Optional) A mapping of tags to assign to the resource."
  default     = null
}
