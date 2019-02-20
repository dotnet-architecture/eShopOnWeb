using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketTests
{
    public class BasketAddItem
    {
        private int _testCatalogItemId = 123;
        private decimal _testUnitPrice = 1.23m;
        private int _testQuantity = 2;

        [Fact]
        public void AddsBasketItemIfNotPresent()
        {
            var basket = new Basket();
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.Equal(_testCatalogItemId, firstItem.CatalogItemId);
            Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
            Assert.Equal(_testQuantity, firstItem.Quantity);
        }

        [Fact]
        public void IncrementsQuantityOfItemIfPresent()
        {
            var basket = new Basket();
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.Equal(_testQuantity * 2, firstItem.Quantity);
        }

        [Fact]
        public void KeepsOriginalUnitPriceIfMoreItemsAdded()
        {
            var basket = new Basket();
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testCatalogItemId, _testUnitPrice * 2, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
        }

        [Fact]
        public void DefaultsToQuantityOfOne()
        {
            var basket = new Basket();
            basket.AddItem(_testCatalogItemId, _testUnitPrice);

            var firstItem = basket.Items.Single();
            Assert.Equal(1, firstItem.Quantity);
        }
    }
}
