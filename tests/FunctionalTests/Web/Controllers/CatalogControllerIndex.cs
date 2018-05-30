using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.eShopWeb;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;

namespace FunctionalTests.Web.Controllers
{
public class CatalogControllerIndex : IClassFixture<WebApplicationFactory<Startup>>
{
public CatalogControllerIndex(WebApplicationFactory<Startup> fixture)
{
    var factory = fixture.Factories.FirstOrDefault() ?? fixture.WithWebHostBuilder(ConfigureWebHostBuilder);
    Client = factory.CreateClient();
    var host = factory.Server?.Host;
    SeedData(host);
}

private void SeedData(IWebHost host)
{
    if(host == null) { throw new ArgumentNullException("host"); }
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var catalogContext = services.GetRequiredService<CatalogContext>();
            CatalogContextSeed.SeedAsync(catalogContext, loggerFactory)
    .Wait();

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            AppIdentityDbContextSeed.SeedAsync(userManager).Wait();
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<CatalogControllerIndex>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}

private static void ConfigureWebHostBuilder(IWebHostBuilder builder)
{
    builder.UseEnvironment("Testing");
}

public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsHomePageWithProductListing()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains(".NET Bot Black Sweatshirt", stringResponse);
        }
    }
}
