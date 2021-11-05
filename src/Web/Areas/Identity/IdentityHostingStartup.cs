using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Microsoft.eShopWeb.Web.Areas.Identity.IdentityHostingStartup))]
namespace Microsoft.eShopWeb.Web.Areas.Identity;

public class IdentityHostingStartup : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
        });
    }
}
