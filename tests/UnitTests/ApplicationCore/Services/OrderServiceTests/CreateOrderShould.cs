using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Events;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.OrderServiceTests;
public class CreateOrderShould
{
    class TestData
    {
        public const int ValidBasketId = 1;
        public static readonly Address ValidAddress = new Address("Ribera del Duero, s/n", "Soria", "Soria", "Spain", "42000");
        public static readonly string ValidBuyerId = Guid.NewGuid().ToString();
        public static readonly Basket ValidBasket;
        public static readonly CatalogItem CatalogItem1;
        public static readonly Order ValidOrder;

        static TestData()
        {
            var catalogItem1Mock = new Mock<CatalogItem>(1, 1, "Item 1", "Item 1", 20.5m, "TestUri1");           
            catalogItem1Mock.SetupGet(x => x.Id).Returns(1);
            CatalogItem1 = catalogItem1Mock.Object;
            
            ValidBasket = new Basket(ValidBuyerId);
            ValidBasket.AddItem(CatalogItem1.Id, CatalogItem1.Price);
            var validOrderMock = new Mock<Order>(
                ValidBuyerId, 
                ValidAddress, 
                new[] 
                { 
                    new OrderItem(new CatalogItemOrdered(CatalogItem1.Id, CatalogItem1.Name, CatalogItem1.PictureUri), 30, 1) 
                }.ToList()
                );
            validOrderMock.SetupGet(x => x.Id).Returns(1);
            ValidOrder = validOrderMock.Object;
        }
    }

    [Fact]
    public async Task Save_new_order_for_buyer()
    {
        _itemRepositoryMock.Setup(m => m.ListAsync(It.IsAny<ISpecification<CatalogItem>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { TestData.CatalogItem1 }.ToList());

        _basketRepositoryMock.Setup(m => m.FirstOrDefaultAsync(It.IsAny<ISpecification<Basket>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(TestData.ValidBasket);

        await _orderService.CreateOrderAsync(TestData.ValidBasketId, TestData.ValidAddress);

        _orderRepositoryMock.Verify(m => m.AddAsync(
            It.Is<Order>(o => o.BuyerId == TestData.ValidBuyerId), 
            It.IsAny<CancellationToken>()), 
        Times.Once);
    }

    [Fact]
    public async Task Publish_an_OrderPaymentSucceeded_event_for_the_new_order()
    {
        _itemRepositoryMock.Setup(m => m.ListAsync(It.IsAny<ISpecification<CatalogItem>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { TestData.CatalogItem1 }.ToList());

        _basketRepositoryMock.Setup(m => m.FirstOrDefaultAsync(It.IsAny<ISpecification<Basket>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(TestData.ValidBasket);

        await _orderService.CreateOrderAsync(TestData.ValidBasketId, TestData.ValidAddress);

        _eventPublisher.Verify(m => m.PublishEvent(
            It.Is<OrderPaymentSucceeded>(e => e.OrderId == 0),
            It.IsAny<CancellationToken>()),
        Times.Once);
    }


    public CreateOrderShould()
    {
        _uriComposerMock.Setup(m => m.ComposePicUri(It.IsAny<string>()))
            .Returns<string>(t => $"https//test-images/{t}");


        _orderService = new OrderService(_basketRepositoryMock.Object, _itemRepositoryMock.Object, _orderRepositoryMock.Object, _eventPublisher.Object, _uriComposerMock.Object);
    }

    OrderService _orderService;
    Mock<IRepository<Basket>> _basketRepositoryMock = new Mock<IRepository<Basket>>();
    Mock<IRepository<CatalogItem>> _itemRepositoryMock = new Mock<IRepository<CatalogItem>>();
    Mock<IRepository<Order>> _orderRepositoryMock = new Mock<IRepository<Order>>();
    Mock<IEventPublisher<OrderPaymentSucceeded>> _eventPublisher = new Mock<IEventPublisher<OrderPaymentSucceeded>>();
    Mock<IUriComposer> _uriComposerMock = new Mock<IUriComposer>();
}
