using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Web.IntegrationTests.Controllers.Api
{
    [TestClass]
    public class CatalogControllerTest : ControllerTestBase
    {
        [TestMethod]
        public async Task List_WhenFilterForBrand2Type1Page0_ThenReturnCorrectItem()
        {
            var result = await EshopOnWebClient.Catalog_ListAsync(2, 1, 0);

            Assert.AreEqual(1, result.CatalogItems.Count());
            var catalogItem = result.CatalogItems.First();
            Assert.AreEqual(2, catalogItem.Id);
            Assert.AreEqual(8.50M, catalogItem.Price);
            Assert.AreEqual("/images/products/2.png", catalogItem.PictureUri);
            Assert.AreEqual(".NET Black & White Mug", catalogItem.Name);
        }

        [TestMethod]
        public async Task List_WhenReturn1Item_ThenPreviousAndNextDisabled()
        {
            var result = await EshopOnWebClient.Catalog_ListAsync(2, 1, 0);

            Assert.AreEqual("is-disabled", result.PaginationInfo.Previous);
            Assert.AreEqual("is-disabled", result.PaginationInfo.Next);
        }
    }
}
