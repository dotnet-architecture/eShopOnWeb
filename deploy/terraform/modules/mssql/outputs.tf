output "conection_strings" {
    description = "Connection strings for the Azure SQL Databases created."
    value       = { for k, v in var.databases : k
            => "Server=tcp:${azurerm_mssql_server.server.fully_qualified_domain_name},1433;Initial Catalog=${k};Persist Security Info=False;User ID=${azurerm_mssql_server.server.administrator_login};Password=${azurerm_mssql_server.server.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
    }    
    sensitive   = true
}
