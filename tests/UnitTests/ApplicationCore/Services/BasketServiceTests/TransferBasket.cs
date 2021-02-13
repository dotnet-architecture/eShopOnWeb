using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests
{
    public class TransferBasket
    {
        private readonly string _nonexistentAnonymousBasketBuyerId = "nonexistent-anonymous-basket-buyer-id";
        private readonly string _existentAnonymousBasketBuyerId = "existent-anonymous-basket-buyer-id";
        private readonly string _nonexistentUserBasketBuyerId = "newuser@microsoft.com";
        private readonly string _existentUserBasketBuyerId = "testuser@microsoft.com";
        private readonly Mock<IAsyncRepository<Basket>> _mockBasketRepo;

        public TransferBasket()
        {
            _mockBasketRepo = new Mock<IAsyncRepository<Basket>>();
        }

        [Fact]
        public async Task ThrowsGivenNullAnonymousId()
        {
            var basketService = new BasketService(null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await basketService.TransferBasketAsync(null, "steve"));
        }

        [Fact]
        public async Task ThrowsGivenNullUserId()
        {
            var basketService = new BasketService(null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await basketService.TransferBasketAsync("abcdefg", null));
        }

        [Fact]
        public async Task InvokesBasketRepositoryFirstOrDefaultAsyncOnceIfAnonymousBasketNotExists()
        {
            var anonymousBasket = null as Basket;
            var userBasket = new Basket(_existentUserBasketBuyerId);
            _mockBasketRepo.SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default))
                .ReturnsAsync(anonymousBasket)
                .ReturnsAsync(userBasket);
            var basketService = new BasketService(_mockBasketRepo.Object, null);
            await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
            _mockBasketRepo.Verify(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default), Times.Once);
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
            _mockBasketRepo.SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default))
                .ReturnsAsync(anonymousBasket)
                .ReturnsAsync(userBasket);
            var basketService = new BasketService(_mockBasketRepo.Object, null);
            await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
            _mockBasketRepo.Verify(x => x.UpdateAsync(userBasket, default), Times.Once);
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
            _mockBasketRepo.SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default))
                .ReturnsAsync(anonymousBasket)
                .ReturnsAsync(userBasket);
            var basketService = new BasketService(_mockBasketRepo.Object, null);
            await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
            _mockBasketRepo.Verify(x => x.UpdateAsync(userBasket, default), Times.Once);
            _mockBasketRepo.Verify(x => x.DeleteAsync(anonymousBasket, default), Times.Once);
        }

        [Fact]
        public async Task CreatesNewUserBasketIfNotExists()
        {
            var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
            var userBasket = null as Basket;
            _mockBasketRepo.SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default))
                .ReturnsAsync(anonymousBasket)
                .ReturnsAsync(userBasket);
            var basketService = new BasketService(_mockBasketRepo.Object, null);
            await basketService.TransferBasketAsync(_existentAnonymousBasketBuyerId, _nonexistentUserBasketBuyerId);
            _mockBasketRepo.Verify(x => x.AddAsync(It.Is<Basket>(x => x.BuyerId == _nonexistentUserBasketBuyerId), default), Times.Once);
        }
    }
}
