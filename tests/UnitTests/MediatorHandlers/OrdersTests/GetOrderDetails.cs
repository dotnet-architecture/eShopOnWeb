using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.OrdersTests;

public class GetOrderDetails
{
    private readonly IReadRepository<Order> _mockOrderRepository =  Substitute.For<IReadRepository<Order>>();
    
    public GetOrderDetails()
    {
        var item = new OrderItem(new CatalogItemOrdered(1, "ProductName", "URI"), 10.00m, 10);
        var address = new Address("", "", "", "", "");
        Order order = new Order("buyerId", address, new List<OrderItem> { item });
                
        _mockOrderRepository.FirstOrDefaultAsync(Arg.Any<OrderWithItemsByIdSpec>(), default)
            .Returns(order);
    }

    [Fact]
    public async Task NotBeNullIfOrderExists()
    {
        var request = new eShopWeb.Web.Features.OrderDetails.GetOrderDetails("SomeUserName", 0);

        var handler = new GetOrderDetailsHandler(_mockOrderRepository);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
    }
}
