using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

namespace Microsoft.eShopWeb.UnitTests
{
    public class BasketWithItems
    {
        private readonly int _testBasketId = 123;
        private readonly string _buyerId = "Test buyerId";

        [Fact]
        public void MatchesBasketWithGivenBasketId()
        {
            var spec = new BasketWithItemsSpecification(_testBasketId);

            var result = GetTestBasketCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criterias.FirstOrDefault());

            Assert.NotNull(result);
            Assert.Equal(_testBasketId, result.Id);
        }

        [Fact]
        public void MatchesNoBasketsIfBasketIdNotPresent()
        {
            int badBasketId = -1;
            var spec = new BasketWithItemsSpecification(badBasketId);

            Assert.False(GetTestBasketCollection()
                .AsQueryable()
                .Any(spec.Criterias.FirstOrDefault()));
        }

        [Fact]
        public void MatchesBasketWithGivenBuyerId()
        {
            var spec = new BasketWithItemsSpecification(_buyerId);

            var result = GetTestBasketCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criterias.FirstOrDefault());

            Assert.NotNull(result);
            Assert.Equal(_buyerId, result.BuyerId);
        }

        [Fact]
        public void MatchesNoBasketsIfBuyerIdNotPresent()
        {
            string badBuyerId = "badBuyerId";
            var spec = new BasketWithItemsSpecification(badBuyerId);

            Assert.False(GetTestBasketCollection()
                .AsQueryable()
                .Any(spec.Criterias.FirstOrDefault()));
        }

        public List<Basket> GetTestBasketCollection()
        {
            var basket1Mock = new Mock<Basket>(_buyerId);
            basket1Mock.SetupGet(s => s.Id).Returns(1);
            var basket2Mock = new Mock<Basket>(_buyerId);
            basket2Mock.SetupGet(s => s.Id).Returns(2);
            var basket3Mock = new Mock<Basket>(_buyerId);
            basket3Mock.SetupGet(s => s.Id).Returns(_testBasketId);

            return new List<Basket>()
            {
                basket1Mock.Object,
                basket2Mock.Object,
                basket3Mock.Object
            };
        }
    }
}
