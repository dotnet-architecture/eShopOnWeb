using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketTests;

public class BasketTotalItems
{
    private readonly int _testCatalogItemId = 123;
    private readonly decimal _testUnitPrice = 1.23m;
    private readonly int _testQuantity = 2;
    private readonly string _buyerId = "Test buyerId";

    [Fact]
    public void ReturnsTotalQuantityWithOneItem()
    {
        var basket = new Basket(_buyerId);
        basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

        var result = basket.TotalItems;

        Assert.Equal(_testQuantity, result);
    }

    [Fact]
    public void ReturnsTotalQuantityWithMultipleItems()
    {
        var basket = new Basket(_buyerId);
        basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
        basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity*2);

        var result = basket.TotalItems;

        Assert.Equal(_testQuantity*3, result);
    }
}
