using System;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;

public class CatalogStock : BaseEntity
{
    public int StockQuantity { get; private set; }
    public int ReservedQuantity { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private CatalogStock() { }

    public CatalogStock(int catalogItemId,
        int stockQuantity,
        int reservedQuantity)
    {
        Id = catalogItemId;
        StockQuantity = stockQuantity;
        ReservedQuantity = reservedQuantity;
    }

    public void UpdateStockQuantity(int quantity)
    {
        StockQuantity = quantity;
    }

    public void UpdateReservedQuantity(int quantity)
    {
        ReservedQuantity = quantity;
    }
}
