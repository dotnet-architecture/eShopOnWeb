using io.unlaunch;

namespace Microsoft.eShopWeb.Web.Services
{
    public class UnlaunchService
    {
        private const string FlagKey = "catalog_reverse";

        private readonly IUnlaunchClient _client;

        public UnlaunchService(IUnlaunchClient client)
        {
            _client = client;
        }

        public bool IsEnabled(string userIdentity)
        {
            var variation = _client.GetVariation(FlagKey, userIdentity);
            return variation == "on";
        }
    }
}
