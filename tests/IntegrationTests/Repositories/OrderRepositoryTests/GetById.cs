using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.OrderRepositoryTests;

public class GetById
{
    private readonly CatalogContext _catalogContext;
    private readonly EfRepository<Order> _orderRepository;
    private OrderBuilder OrderBuilder { get; } = new OrderBuilder();
    private readonly ITestOutputHelper _output;
    public GetById(ITestOutputHelper output)
    {
        _output = output;
        var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "TestCatalog")
            .Options;
        _catalogContext = new CatalogContext(dbOptions);
        _orderRepository = new EfRepository<Order>(_catalogContext);
    }

    [Fact]
    public async Task GetsExistingOrder()
    {
        var existingOrder = OrderBuilder.WithDefaultValues();
        _catalogContext.Orders.Add(existingOrder);
        _catalogContext.SaveChanges();
        int orderId = existingOrder.Id;
        _output.WriteLine($"OrderId: {orderId}");

        var orderFromRepo = await _orderRepository.GetByIdAsync(orderId);
        Assert.Equal(OrderBuilder.TestBuyerId, orderFromRepo.BuyerId);

        // Note: Using InMemoryDatabase OrderItems is available. Will be null if using SQL DB.
        // Use the OrderWithItemsByIdSpec instead of just GetById to get the full aggregate
        var firstItem = orderFromRepo.OrderItems.FirstOrDefault();
        Assert.Equal(OrderBuilder.TestUnits, firstItem.Units);
    }
}
