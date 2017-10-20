using ApplicationCore.Entities.OrderAggregate;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UnitTests.Builders;
using Xunit;

namespace IntegrationTests.Repositories.OrderRepositoryTests
{
    public class GetById
    {
        private readonly CatalogContext _catalogContext;
        private readonly OrderRepository _orderRepository;
        private string _testBuyerId = "12345";
        public GetById()
        {
            var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "TestCatalog")
                .Options;
            _catalogContext = new CatalogContext(dbOptions);
            _orderRepository = new OrderRepository(_catalogContext);
        }

        [Fact]
        public void GetsExistingOrder()
        {
            var existingOrder = new Order(_testBuyerId, new AddressBuilder().WithDefaultValues(), new List<OrderItem>());
            _catalogContext.Orders.Add(existingOrder);
            _catalogContext.SaveChanges();
            int orderId = existingOrder.Id;

            var orderFromRepo = _orderRepository.GetById(orderId);
            Assert.Equal(_testBuyerId, orderFromRepo.BuyerId);
        }
    }
}
