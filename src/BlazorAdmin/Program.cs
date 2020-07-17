using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorAdmin.Network;
using BlazorAdmin.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace BlazorAdmin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("admin");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddTransient(sp => new SecureHttpClient(sp.GetRequiredService<HttpClient>()));

            builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


            // TODO: Sort out how to implement these with custom AuthService
            builder.Services.AddSingleton<IAuthorizationPolicyProvider, DefaultAuthorizationPolicyProvider>();
            builder.Services.AddSingleton<IAuthorizationService, DefaultAuthorizationService>();
            builder.Services.AddSingleton<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();
            builder.Services.AddSingleton<IAuthorizationHandlerContextFactory, DefaultAuthorizationHandlerContextFactory>();
            builder.Services.AddSingleton<IAuthorizationEvaluator, DefaultAuthorizationEvaluator>();

            await builder.Build().RunAsync();
        }
    }
}
