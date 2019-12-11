using Microsoft.eShopWeb.Web.Extensions;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Extensions.CacheHelpersTests
{
    public class GenerateBrandsCacheKey_Should
    {
        [Fact]
        public void ReturnBrandsCacheKey()
        {
            var result = CacheHelpers.GenerateBrandsCacheKey();

            Assert.Equal("brands", result);
        }
    }
}
