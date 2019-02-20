using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.eShopWeb.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class GetById
    {
        private readonly CatalogContext _catalogContext;
        private readonly OrderRepository _orderRepository;
        private OrderBuilder OrderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;
        public GetById(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "TestCatalog")
                .Options;
            _catalogContext = new CatalogContext(dbOptions);
            _orderRepository = new OrderRepository(_catalogContext);
        }

        [Fact]
        public void GetsExistingOrder()
        {
            var existingOrder = OrderBuilder.WithDefaultValues();
            _catalogContext.Orders.Add(existingOrder);
            _catalogContext.SaveChanges();
            int orderId = existingOrder.Id;
            _output.WriteLine($"OrderId: {orderId}");

            var orderFromRepo = _orderRepository.GetById(orderId);
            Assert.Equal(OrderBuilder.TestBuyerId, orderFromRepo.BuyerId);

            // Note: Using InMemoryDatabase OrderItems is available. Will be null if using SQL DB.
            var firstItem = orderFromRepo.OrderItems.FirstOrDefault();
            Assert.Equal(OrderBuilder.TestUnits, firstItem.Units);
        }
    }
}
