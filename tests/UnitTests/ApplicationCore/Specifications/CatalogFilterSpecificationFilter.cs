using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests
{
    public class CatalogFilterSpecificationFilter
    {
        [Theory]
        [InlineData(null, null, 5)]
        [InlineData(1, null, 3)]
        [InlineData(2, null, 2)]
        [InlineData(null, 1, 2)]
        [InlineData(null, 3, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(2, 3, 0)]
        public void MatchesExpectedNumberOfItems(int? brandId, int? typeId, int expectedCount)
        {
            var spec = new CatalogFilterSpecification(brandId, typeId);

            var result = GetTestItemCollection()
                .AsQueryable()
                .Where(spec.Criteria);

            Assert.Equal(expectedCount, result.Count());
        }

        public List<CatalogItem> GetTestItemCollection()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem() { Id = 1, CatalogBrandId = 1, CatalogTypeId= 1 },
                new CatalogItem() { Id = 2, CatalogBrandId = 1, CatalogTypeId= 2 },
                new CatalogItem() { Id = 3, CatalogBrandId = 1, CatalogTypeId= 3 },
                new CatalogItem() { Id = 4, CatalogBrandId = 2, CatalogTypeId= 1 },
                new CatalogItem() { Id = 5, CatalogBrandId = 2, CatalogTypeId= 2 },
            };
        }
    }
}
