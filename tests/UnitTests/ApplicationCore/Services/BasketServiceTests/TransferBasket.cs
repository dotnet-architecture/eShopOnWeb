using Microsoft.eShopWeb.ApplicationCore.Services;
using System;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests
{
    public class TransferBasket
    {
        [Fact]
        public async void ThrowsGivenNullAnonymousId()
        {
            var basketService = new BasketService(null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await basketService.TransferBasketAsync(null, "steve"));
        }

        [Fact]
        public async void ThrowsGivenNullUserId()
        {
            var basketService = new BasketService(null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await basketService.TransferBasketAsync("abcdefg", null));
        }
    }
}
