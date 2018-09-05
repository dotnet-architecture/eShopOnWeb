using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.eShopWeb.Infrastructure.Data;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace Microsoft.eShopWeb.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                        .Build();

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
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
		    WebHost
				.CreateDefaultBuilder(args)
				.UseKestrel(opts =>
				{
					opts.ListenLocalhost(5106);
					opts.ListenLocalhost(44388, lo =>
					{
						lo.UseHttps($@"{Directory.GetCurrentDirectory()}\\Certificates\\eShop.pfx", "D0tNet@");
					});
				})
				.UseUrls("http://0.0.0.0:5106", "https://0.0.0.0:44388")
				.UseIISIntegration()
				.UseStartup<Startup>();
    }
}
