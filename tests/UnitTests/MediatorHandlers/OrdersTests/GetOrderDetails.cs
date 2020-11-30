using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.OrdersTests
{
    public class GetOrderDetails
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;

        public GetOrderDetails()
        {
            var item = new OrderItem(new CatalogItemOrdered(1, "ProductName", "URI"), 10.00m, 10);
            var address = new Address(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Order order = new Order("buyerId", address, new List<OrderItem> { item });

            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockOrderRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Order>>(),default)).ReturnsAsync(new List<Order> { order });
        }

        [Fact]
        public async Task NotBeNullIfOrderExists()
        {
            var request = new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("SomeUserName", 0);

            var handler = new GetOrderDetailsHandler(_mockOrderRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task BeNullIfOrderNotFound()
        {
            var request = new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("SomeUserName", 100);

            var handler = new GetOrderDetailsHandler(_mockOrderRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
