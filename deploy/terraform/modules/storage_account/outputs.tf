output "name" {
  description = "Specifies the name of the storage account."
  value       = azurerm_storage_account.storage_account.name
}

output "id" {
  description = "Specifies the resource id of the storage account."
  value       = azurerm_storage_account.storage_account.id
}

output "primary_access_key" {
  description = "Specifies the primary access key of the storage account."
  value       = azurerm_storage_account.storage_account.primary_access_key
}

output "principal_id" {
  description = "Specifies the principal id of the system assigned managed identity of the storage account."
  value       = azurerm_storage_account.storage_account.identity[0].principal_id
}

output "primary_blob_endpoint" {
  description = "Specifies the primary blob endpoint of the storage account."
  value       = azurerm_storage_account.storage_account.primary_blob_endpoint
}