using System.Collections.Generic;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;

public class CatalogFilterSpecification
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
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogFilterSpecification(brandId, typeId);

        var result = spec.Evaluate(GetTestItemCollection()).ToList();

        Assert.Equal(expectedCount, result.Count());
    }

    public List<CatalogItem> GetTestItemCollection()
    {
        return new List<CatalogItem>()
            {
                new CatalogItem(1, 1, "Description", "Name", 0, "FakePath"),
                new CatalogItem(2, 1, "Description", "Name", 0, "FakePath"),
                new CatalogItem(3, 1, "Description", "Name", 0, "FakePath"),
                new CatalogItem(1, 2, "Description", "Name", 0, "FakePath"),
                new CatalogItem(2, 2, "Description", "Name", 0, "FakePath"),
            };
    }
}
