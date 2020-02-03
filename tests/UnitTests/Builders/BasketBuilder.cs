using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Moq;

namespace Microsoft.eShopWeb.UnitTests.Builders
{
    public class BasketBuilder
    {
        private Basket _basket;      
        public string BasketBuyerId => "testbuyerId@test.com";

        public int BasketId => 1;

        public BasketBuilder()
        {
            _basket = WithNoItems();
        }

        public Basket Build()
        {
            return _basket;
        }

        public Basket WithNoItems()
        {
            var basketMock = new Mock<Basket>(BasketBuyerId);
            basketMock.SetupGet(s => s.Id).Returns(BasketId);

            _basket = basketMock.Object;
            return _basket;
        }

        public Basket WithOneBasketItem()
        {
            var basketMock = new Mock<Basket>(BasketBuyerId);
            _basket = basketMock.Object;
            _basket.AddItem(2, 3.40m, 4);
            return _basket;
        }
    }
}
