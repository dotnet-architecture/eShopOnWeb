using System;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests;

public class TransferBasket
{
    private readonly string _nonexistentAnonymousBasketBuyerId = "nonexistent-anonymous-basket-buyer-id";
    private readonly string _existentAnonymousBasketBuyerId = "existent-anonymous-basket-buyer-id";
    private readonly string _nonexistentUserBasketBuyerId = "newuser@microsoft.com";
    private readonly string _existentUserBasketBuyerId = "testuser@microsoft.com";
    private readonly IRepository<Basket> _mockBasketRepo = Substitute.For<IRepository<Basket>>();
    private readonly IAppLogger<BasketService> _mockLogger = Substitute.For<IAppLogger<BasketService>>();

    public class Results<T>
    {
        private readonly Queue<Func<T>> values = new Queue<Func<T>>();
        public Results(T result) { values.Enqueue(() => result); }
        public Results<T> Then(T value) { return Then(() => value); }
        public Results<T> Then(Func<T> value)
        {
            values.Enqueue(value);
            return this;
        }
        public T Next() { return values.Dequeue()(); }
    }

        [Fact]
    public async Task InvokesBasketRepositoryFirstOrDefaultAsyncOnceIfAnonymousBasketNotExists()
    {
            var anonymousBasket = null as Basket;
            var userBasket = new Basket(_existentUserBasketBuyerId);
            
        var results = new Results<Basket?>(anonymousBasket)
                        .Then(userBasket);


        _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithItemsSpecification>(), default).Returns(x => results.Next());          
        var basketService = new BasketService(_mockBasketRepo, _mockLogger);
        await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
        await _mockBasketRepo.Received().FirstOrDefaultAsync(Arg.Any<BasketWithItemsSpecification>(), default);
    }

    [Fact]
    public async Task TransferAnonymousBasketItemsWhilePreservingExistingUserBasketItems()
    {
        var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
        anonymousBasket.AddItem(1, 10, 1);
        anonymousBasket.AddItem(3, 55, 7);
        var userBasket = new Basket(_existentUserBasketBuyerId);
        userBasket.AddItem(1, 10, 4);
        userBasket.AddItem(2, 99, 3);

        var results = new Results<Basket>(anonymousBasket)
                        .Then(userBasket);

        _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithItemsSpecification>(), default).Returns(x => results.Next());
        var basketService = new BasketService(_mockBasketRepo, _mockLogger);
        await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
        await _mockBasketRepo.Received().UpdateAsync(userBasket, default);

        Assert.Equal(3, userBasket.Items.Count);
        Assert.Contains(userBasket.Items, x => x.CatalogItemId == 1 && x.UnitPrice == 10 && x.Quantity == 5);
        Assert.Contains(userBasket.Items, x => x.CatalogItemId == 2 && x.UnitPrice == 99 && x.Quantity == 3);
        Assert.Contains(userBasket.Items, x => x.CatalogItemId == 3 && x.UnitPrice == 55 && x.Quantity == 7);
    }

    [Fact]
    public async Task RemovesAnonymousBasketAfterUpdatingUserBasket()
    {
        var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
        var userBasket = new Basket(_existentUserBasketBuyerId);

        var results = new Results<Basket>(anonymousBasket)
                        .Then(userBasket);

        _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithItemsSpecification>(), default).Returns(x => results.Next());
        var basketService = new BasketService(_mockBasketRepo, _mockLogger);
        await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
        await _mockBasketRepo.Received().UpdateAsync(userBasket, default);
        await _mockBasketRepo.Received().DeleteAsync(anonymousBasket, default);
    }

    [Fact]
    public async Task CreatesNewUserBasketIfNotExists()
    {
        var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
        var userBasket = null as Basket;

        var results = new Results<Basket?>(anonymousBasket)
                       .Then(userBasket);

        _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithItemsSpecification>(), default).Returns(x => results.Next());
        var basketService = new BasketService(_mockBasketRepo, _mockLogger);
        await basketService.TransferBasketAsync(_existentAnonymousBasketBuyerId, _nonexistentUserBasketBuyerId);
        await _mockBasketRepo.Received().AddAsync(Arg.Is<Basket>(x => x.BuyerId == _nonexistentUserBasketBuyerId), default);
    }
}
