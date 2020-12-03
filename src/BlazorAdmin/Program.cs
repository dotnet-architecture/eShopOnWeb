using BlazorAdmin.Services;
using Blazored.LocalStorage;
using BlazorShared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorAdmin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#admin");

            var baseUrlConfig = new BaseUrlConfiguration();
            builder.Configuration.Bind(BaseUrlConfiguration.CONFIG_NAME, baseUrlConfig);
            builder.Services.AddScoped<BaseUrlConfiguration>(sp => baseUrlConfig);

            builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<HttpService>();

            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddScoped(sp => (CustomAuthStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());

            builder.Services.AddBlazorServices();

            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

            await ClearLocalStorageCache(builder.Services);

            await builder.Build().RunAsync();
        }

        private static async Task ClearLocalStorageCache(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var localStorageService = sp.GetRequiredService<ILocalStorageService>();

            await localStorageService.RemoveItemAsync("brands");
        }
    }
}
