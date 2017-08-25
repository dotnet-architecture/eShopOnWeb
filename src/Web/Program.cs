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

    //public static void Main(string[] args)
    //{
    //    var host = new WebHostBuilder()
    //        .UseKestrel()
    //        .UseUrls("http://0.0.0.0:5106")
    //        .UseContentRoot(Directory.GetCurrentDirectory())
    //        .ConfigureLogging(factory =>
    //        {
    //            factory.AddConsole(LogLevel.Warning);
    //            factory.AddDebug();
    //        })
    //        .UseIISIntegration()
    //        .UseStartup<Startup>()
    //        .UseApplicationInsights()
    //        .Build();

    //    host.Run();
    //}
}
