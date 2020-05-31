using EshopOnWeb;

namespace Web.IntegrationTests.Controllers.Api
{
    public class ControllerTestBase
    {
        protected EshopOnWebClient EshopOnWebClient { get; private set; }

        public ControllerTestBase()
        {
            var httpClient = StartupTest.TestServer.CreateClient();
            EshopOnWebClient = new EshopOnWebClient(httpClient);
        }
    }
}
