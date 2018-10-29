using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using Steeltoe.Extensions.Configuration.CloudFoundry;

using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Infrastructure.Data;

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

                var scopedServices = scope.ServiceProvider;

                // Catalog
                var catalogDB = scopedServices.GetRequiredService<CatalogContext>();                
                catalogDB.Database.Migrate();

                var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();

                try
                {
                    CatalogContextSeed.SeedAsync(catalogDB, loggerFactory).Wait();
                }
                catch (Exception ex)
                {
                    var logger = scopedServices.GetRequiredService<ILogger<Startup>>();
                    logger.LogError(ex, "An error occurred while migrating the database. Catalog");
                }

                // Identity
                var identityDB = scopedServices.GetRequiredService<AppIdentityDbContext>();
                identityDB.Database.Migrate();
                
                try
                {                    
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = scopedServices.GetRequiredService<ILogger<Startup>>();
                    logger.LogError(ex, "An error occurred while migrating the database. Identity");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseCloudFoundryHosting()
                .UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build())
                .UseCloudFoundryHosting()
                .AddCloudFoundry()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, configBuilder) =>
                {
                    var env = builderContext.HostingEnvironment;
                    configBuilder.SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables()
                        .AddCloudFoundry();
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                });
    }
}
