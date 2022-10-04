using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests;

public class AddItemToBasket
{
    private readonly string _buyerId = "Test buyerId";
    private readonly Mock<IRepository<Basket>> _mockBasketRepo = new();
    private readonly Mock<IAppLogger<BasketService>> _mockLogger = new();

    [Fact]
    public async Task InvokesBasketRepositoryGetBySpecAsyncOnce()
    {
        var basket = new Basket(_buyerId);
        basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<int>());
        _mockBasketRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default)).ReturnsAsync(basket);

        var basketService = new BasketService(_mockBasketRepo.Object, _mockLogger.Object);

        await basketService.AddItemToBasket(basket.BuyerId, 1, 1.50m);

        _mockBasketRepo.Verify(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default), Times.Once);
    }

    [Fact]
    public async Task InvokesBasketRepositoryUpdateAsyncOnce()
    {
        var basket = new Basket(_buyerId);
        basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<int>());
        _mockBasketRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default)).ReturnsAsync(basket);

        var basketService = new BasketService(_mockBasketRepo.Object, _mockLogger.Object);

        await basketService.AddItemToBasket(basket.BuyerId, 1, 1.50m);

        _mockBasketRepo.Verify(x => x.UpdateAsync(basket, default), Times.Once);
    }
}
