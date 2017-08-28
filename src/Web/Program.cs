using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace Microsoft.eShopWeb
{
    public class Program
    {

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:5106")
                .UseStartup<Startup>()
                .Build();
    }
}
