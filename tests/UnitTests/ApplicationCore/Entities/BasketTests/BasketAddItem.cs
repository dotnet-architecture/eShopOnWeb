using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using System;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.BasketTests
{
    public class BasketAddItem
    {
        private readonly int _testCatalogItemId = 123;
        private readonly decimal _testUnitPrice = 1.23m;
        private readonly int _testQuantity = 2;
        private readonly string _buyerId = "Test buyerId";

        [Fact]
        public void AddsBasketItemIfNotPresent()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.Equal(_testCatalogItemId, firstItem.CatalogItemId);
            Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
            Assert.Equal(_testQuantity, firstItem.Quantity);
        }

        [Fact]
        public void IncrementsQuantityOfItemIfPresent()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.Equal(_testQuantity * 2, firstItem.Quantity);
        }

        [Fact]
        public void KeepsOriginalUnitPriceIfMoreItemsAdded()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testCatalogItemId, _testUnitPrice * 2, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
        }

        [Fact]
        public void DefaultsToQuantityOfOne()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice);

            var firstItem = basket.Items.Single();
            Assert.Equal(1, firstItem.Quantity);
        }

        [Fact]
        public void CantAddItemWithNegativeQuantity()
        {
            var basket = new Basket(_buyerId);

            Assert.Throws<ArgumentOutOfRangeException>(() => basket.AddItem(_testCatalogItemId, _testUnitPrice, -1));
        }

        [Fact]
        public void CantModifyQuantityToNegativeNumber()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice);

            Assert.Throws<ArgumentOutOfRangeException>(() => basket.AddItem(_testCatalogItemId, _testUnitPrice, -2));
        }
    }
}
