using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.OrdersTests;

public class GetMyOrders
{
    private readonly IReadRepository<Order> _mockOrderRepository = Substitute.For<IReadRepository<Order>>();

    public GetMyOrders()
    {
        var item = new OrderItem(new CatalogItemOrdered(1, "ProductName", "URI"), 10.00m, 10);
        var address = new Address("", "", "", "", "");
        Order order = new Order("buyerId", address, new List<OrderItem> { item });
              
        _mockOrderRepository.ListAsync(Arg.Any<ISpecification<Order>>(), default).Returns(new List<Order> { order });
    }

    [Fact]
    public async Task NotReturnNullIfOrdersArePresIent()
    {
        var request = new eShopWeb.Web.Features.MyOrders.GetMyOrders("SomeUserName");

        var handler = new GetMyOrdersHandler(_mockOrderRepository);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
    }
}
