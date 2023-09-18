using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
//using Moq;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests;

public class DeleteBasket
{
    private readonly string _buyerId = "Test buyerId";
    private readonly IRepository<Basket> _mockBasketRepo = Substitute.For<IRepository<Basket>>();
    private readonly IAppLogger<BasketService> _mockLogger = Substitute.For<IAppLogger<BasketService>>();

    [Fact]
    public async Task ShouldInvokeBasketRepositoryDeleteAsyncOnce()
    {
        var basket = new Basket(_buyerId);
        basket.AddItem(1, 1.1m, 1);
        basket.AddItem(2, 1.1m, 1);
        _mockBasketRepo.GetByIdAsync(Arg.Any<int>(), default)
            .Returns(basket);
        var basketService = new BasketService(_mockBasketRepo, _mockLogger);

        await basketService.DeleteBasketAsync(1);

        await _mockBasketRepo.Received().DeleteAsync(Arg.Any<Basket>(), default);
    }
}
