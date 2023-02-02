variable spn_tenant_id       {}
variable spn_subscription_id {}
variable spn_client_id       {}
variable spn_secret          {}


variable law_id             {}
variable rg_name            {}
variable st_id              {}

variable retention_in_days  {
  type        = string
  description = "(Required) The workspace level data retention in days. Possible values range between `30` and `730 (2 years)`."
}
