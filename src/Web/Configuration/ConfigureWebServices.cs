using MediatR;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.eShopWeb.Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(BasketViewModelService).Assembly);
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            //services.AddScoped<CatalogViewModelService>();
            services.AddTransient<ICatalogViewModelService,CatalogViewModelService>();
            //services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();
            services.AddTransient<ICatalogItemViewModelService, CatalogItemViewModelService>();
            services.Configure<CatalogSettings>(configuration);
            //services.AddScoped<ICatalogViewModelService, CachedCatalogViewModelService>();            

            return services;
        }
    }
}
