using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using System;
using Microsoft.Extensions.Logging;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.eShopWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var catalogContext = services.GetRequiredService<CatalogContext>();
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                    CatalogContextSeed.SeedAsync(catalogContext, loggerFactory)
            .Wait();

                    // move to IdentitySeed method
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
                    userManager.CreateAsync(defaultUser, "Pass@word1").Wait();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:5106")
                .UseStartup<Startup>()
                .Build();
    }
}
