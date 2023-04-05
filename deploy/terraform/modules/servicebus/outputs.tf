output "conection_string" {
    description = "Connection strings for the ServiceBus"
    value       = azurerm_servicebus_namespace.namespace.default_primary_connection_string
    sensitive   = true
}
