using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.eShopWeb.Infrastructure.Data;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopWeb.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // logging at the host level
            // https://ardalis.com/logging-and-using-services-in-startup-in-aspnet-core-apps
            var host = CreateWebHostBuilder(args)
                         .ConfigureAppConfiguration((context, configApp) =>
                         {
                             configApp.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                             configApp.AddJsonFile(
                                 $"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                                 optional: true);
                             configApp.AddEnvironmentVariables();
                             configApp.AddCommandLine(args);
                         })
                         .ConfigureLogging((context, logging) =>
                         {
                             logging.AddConfiguration(context.Configuration.GetSection("Logging"));

                             logging.AddConsole();
                             logging.AddDebug();
                         })
                        .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var config = services.GetRequiredService<IConfiguration>();
                try
                {
                    var catalogContext = services.GetRequiredService<CatalogContext>();
                    CatalogContextSeed.SeedAsync(catalogContext, loggerFactory, config, 3).Wait();

                    // seeding will only be successful if database was already created.
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:5106")
                .UseStartup<Startup>();
    }
}
