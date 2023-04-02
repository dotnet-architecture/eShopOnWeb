using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class InventoryService: IInventoryService
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public InventoryService(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
    }
    public Task UpdateOrderReservedQuantity(int orderId)
    {
        using var connection = _dbConnectionFactory.CreateConnection("CatalogConnection");
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = @"
MERGE dbo.CatalogStock as tgt
USING (
    SELECT ItemOrdered_CatalogItemId AS CatalogItemId, Units 
    from dbo.OrderItems 
    where [OrderId] = @OrderId
) as src (CatalogItemId, Units)
ON tgt.Id = src.CatalogItemId
WHEN MATCHED THEN  
    UPDATE SET ReservedQuantity = ReservedQuantity + src.Units
WHEN NOT MATCHED BY TARGET THEN  
    INSERT (Id, StockQuantity, ReservedQuantity) VALUES (src.CatalogItemId, 0, src.Units);";

        var orderIdParam = command.CreateParameter();
        orderIdParam.ParameterName = "@OrderId";
        orderIdParam.Value = orderId;
        command.Parameters.Add(orderIdParam);
        
        command.ExecuteNonQuery();

        return Task.CompletedTask;
    }
}
