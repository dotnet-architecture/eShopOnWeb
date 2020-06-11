namespace eShopOnBlazorWasm.Server
{
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.eShopWeb.Infrastructure.Data;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;
  using System;

  public class Program
  {
    public static IHostBuilder CreateHostBuilder(string[] aArgumentArray) =>
      Host.CreateDefaultBuilder(aArgumentArray)
        .ConfigureWebHostDefaults
        (
          aWebHostBuilder =>
          {

            aWebHostBuilder.UseStaticWebAssets();
            aWebHostBuilder.UseStartup<Startup>();
          }
        );

    public static async System.Threading.Tasks.Task Main(string[] aArgumentArray)
    {
      IHost host = CreateHostBuilder(aArgumentArray).Build();

      using (IServiceScope serviceScope = host.Services.CreateScope())
      {
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        try
        {
          CatalogContext catalogContext = serviceProvider.GetRequiredService<CatalogContext>();
          await CatalogContextSeed.SeedAsync(catalogContext, loggerFactory);

          //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
          //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
          //await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
        }
        catch (Exception ex)
        {
          ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
          logger.LogError(ex, "An error occurred seeding the DB.");
        }
      }

      host.Run();
    }
  }
}
