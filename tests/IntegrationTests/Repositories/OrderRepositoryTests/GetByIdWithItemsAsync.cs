using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.UnitTests.Builders;
using Xunit;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.OrderRepositoryTests;

public class GetByIdWithItemsAsync
{
    private readonly CatalogContext _catalogContext;
    private readonly EfRepository<Order> _orderRepository;
    private OrderBuilder OrderBuilder { get; } = new OrderBuilder();

    public GetByIdWithItemsAsync()
    {
        var dbOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "TestCatalog")
            .Options;
        _catalogContext = new CatalogContext(dbOptions);
        _orderRepository = new EfRepository<Order>(_catalogContext);
    }

    [Fact]
    public async Task GetOrderAndItemsByOrderIdWhenMultipleOrdersPresent()
    {
        //Arrange
        var itemOneUnitPrice = 5.50m;
        var itemOneUnits = 2;
        var itemTwoUnitPrice = 7.50m;
        var itemTwoUnits = 5;

        var firstOrder = OrderBuilder.WithDefaultValues();
        _catalogContext.Orders.Add(firstOrder);
        int firstOrderId = firstOrder.Id;

        var secondOrderItems = new List<OrderItem>();
        secondOrderItems.Add(new OrderItem(OrderBuilder.TestCatalogItemOrdered, itemOneUnitPrice, itemOneUnits));
        secondOrderItems.Add(new OrderItem(OrderBuilder.TestCatalogItemOrdered, itemTwoUnitPrice, itemTwoUnits));
        var secondOrder = OrderBuilder.WithItems(secondOrderItems);
        _catalogContext.Orders.Add(secondOrder);
        int secondOrderId = secondOrder.Id;

        _catalogContext.SaveChanges();

        //Act
        var spec = new OrderWithItemsByIdSpec(secondOrderId);
        var orderFromRepo = await _orderRepository.FirstOrDefaultAsync(spec);

        //Assert
        Assert.Equal(secondOrderId, orderFromRepo.Id);
        Assert.Equal(secondOrder.OrderItems.Count, orderFromRepo.OrderItems.Count);
        Assert.Equal(1, orderFromRepo.OrderItems.Count(x => x.UnitPrice == itemOneUnitPrice));
        Assert.Equal(1, orderFromRepo.OrderItems.Count(x => x.UnitPrice == itemTwoUnitPrice));
        Assert.Equal(itemOneUnits, orderFromRepo.OrderItems.SingleOrDefault(x => x.UnitPrice == itemOneUnitPrice).Units);
        Assert.Equal(itemTwoUnits, orderFromRepo.OrderItems.SingleOrDefault(x => x.UnitPrice == itemTwoUnitPrice).Units);
    }
}
