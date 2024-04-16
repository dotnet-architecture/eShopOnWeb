using System;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketTests;

public class BasketRemoveItem
{
    private readonly Basket _basket;
    private readonly string _buyerId = "Test buyerId";

    public BasketRemoveItem()
    {
        _basket = new Basket(_buyerId);
    }

    [Fact]
    public void RemoveItem_RemovesItemGivenValidId()
    {
        // Arrange
        var catalogItemId = 5;
        _basket.AddItem(catalogItemId, 2.5m, 1);

        // Act
        _basket.RemoveItem(catalogItemId);

        // Assert
        Assert.DoesNotContain(_basket.Items, i => i.CatalogItemId == catalogItemId);
    }

    [Fact]
    public void RemoveItem_DoesNothingGivenInvalidId()
    {
        // Arrange
        var invalidCatalogItemId = 0;

        // Act
        _basket.RemoveItem(invalidCatalogItemId);

        // Assert
        Assert.All(_basket.Items, i => Assert.NotEqual(invalidCatalogItemId, i.CatalogItemId));
    }
}