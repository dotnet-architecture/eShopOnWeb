using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Web.IntegrationTests
{
    [TestClass]
    public class StartupTest
    {
        public static TestServer TestServer { get; private set; }

        [AssemblyInitialize]
        public static async Task AssemblyInitialize(TestContext _)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Hardcoded environment because Environment.GetEnvironmentVariable in test project not read Environment variable
            //https://github.com/aspnet/Tooling/issues/1089
            var environment = "Development";

            var webHostBuilder = new WebHostBuilder()
               .UseEnvironment(environment)
               .UseConfiguration(new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build());

            TestServer = new TestServer(webHostBuilder.UseStartup<Startup>());

            var catalogContext = TestServer.Services.GetRequiredService<CatalogContext>();
            var loggerFactory = TestServer.Services.GetRequiredService<ILoggerFactory>();

            if (catalogContext.Database.IsSqlServer())
            {
                catalogContext.Database.EnsureDeleted();
                catalogContext.Database.EnsureCreated();
            }

            await CatalogContextSeed.SeedAsync(catalogContext, loggerFactory);
        }
    }
}
