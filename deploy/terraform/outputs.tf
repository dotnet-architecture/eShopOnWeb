output "log_analytics_name" {
   value = module.log_analytics_workspace.name
}

output "log_analytics_workspace_id" {
   value = module.log_analytics_workspace.workspace_id
}

output "mssql_conection_strings" {
  value = module.mssql.conection_strings
  sensitive = true
}