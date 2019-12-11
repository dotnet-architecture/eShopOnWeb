using Microsoft.eShopWeb.Web.Extensions;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Extensions.CacheHelpersTests
{
    public class GenerateTypesCacheKey_Should
    {
        [Fact]
        public void ReturnTypesCacheKey()
        {
            var result = CacheHelpers.GenerateTypesCacheKey();

            Assert.Equal("types", result);
        }
    }
}
