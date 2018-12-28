using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

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
            _basket = new Basket { BuyerId = BasketBuyerId, Id = BasketId };
            return _basket;
        }

        public Basket WithOneBasketItem()
        {
            _basket = new Basket { BuyerId = BasketBuyerId, Id = BasketId };
            _basket.AddItem(2, 3.40m, 4);
            return _basket;
        }
    }
}
