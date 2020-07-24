using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorAdmin.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAdmin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("admin");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddSingleton(sp => (CustomAuthStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());

            await builder.Build().RunAsync();
        }
    }
}
