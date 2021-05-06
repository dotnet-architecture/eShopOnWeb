using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.UnitTests.Builders;
using System.Collections.Generic;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Components.Forms;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.OrderTests
{
    public class OrderTotal
    {
        private decimal _testUnitPrice = 42m;

        [Fact]
        public void IsZeroForNewOrder()
        {
            var order = new OrderBuilder().WithNoItems();

            Assert.Equal(0, order.Total());
        }

        [Fact]
        public void IsCorrectGiven1Item()
        {
            var builder = new OrderBuilder();
            var items = new List<OrderItem>
            {
                new OrderItem(builder.TestCatalogItemOrdered, _testUnitPrice, 1)
            };
            var order = new OrderBuilder().WithItems(items);
            Assert.Equal(_testUnitPrice, order.Total());
        }

        [Fact]
        public void IsCorrectGiven3Items()
        {
            var builder = new OrderBuilder();
            var order = builder.WithDefaultValues();

            Assert.Equal(builder.TestUnitPrice * builder.TestUnits, order.Total());
        }

        [Fact]
        public void ShouldApply50PercentDiscount()
        {
            //Given
            var price = 1.23m;
            var testUnits = 5;
            var catalogItemOrdered = new CatalogItemOrdered(123, "Test product", "uri");
            var orderItem = new OrderItem(catalogItemOrdered, price, testUnits);
            var itemList = new List<OrderItem>() {orderItem};
            var discountList = new List<IDiscount>() {new FiveOrMoreDiscount()};

            var order = new Order("buyerId", new AddressBuilder().WithDefaultValues(), itemList, discountList);

            //When
            var total = order.Total();
            
            //Then
            var expectedTotal = (price * testUnits) / 2;
            
            Assert.Equal(expectedTotal, total);
        }
    }
}