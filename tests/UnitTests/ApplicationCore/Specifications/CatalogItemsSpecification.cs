using System.Collections.Generic;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;

public class CatalogItemsSpecification
{
    [Fact]
    public void MatchesSpecificCatalogItem()
    {
        var catalogItemIds = new int[] { 1 };
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogItemsSpecification(catalogItemIds);

        var result = spec.Evaluate(GetTestCollection()).ToList();

        Assert.NotNull(result);
        Assert.Single(result.ToList());
    }

    [Fact]
    public void MatchesAllCatalogItems()
    {
        var catalogItemIds = new int[] { 1, 3 };
        var spec = new eShopWeb.ApplicationCore.Specifications.CatalogItemsSpecification(catalogItemIds);

        var result = spec.Evaluate(GetTestCollection()).ToList();

        Assert.NotNull(result);
        Assert.Equal(2, result.ToList().Count);
    }

    private List<CatalogItem> GetTestCollection()
    {
        var catalogItems = new List<CatalogItem>();

        var mockCatalogItem1 = Substitute.For<CatalogItem>(1, 1, "Item 1 description", "Item 1", 1.5m, "Item1Uri");
        mockCatalogItem1.Id.Returns(1);

        var mockCatalogItem3 = Substitute.For<CatalogItem>(3, 3, "Item 3 description", "Item 3", 3.5m, "Item3Uri");
        mockCatalogItem3.Id.Returns(3);

        catalogItems.Add(mockCatalogItem1);
        catalogItems.Add(mockCatalogItem3);

        return catalogItems;
    }
}
