using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAdmin
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddBlazorServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogLookupDataService<CatalogBrand>, CachedCatalogBrandServiceDecorator>();
            services.AddScoped<CatalogLookupDataService<CatalogBrand, CatalogBrandResponse>>();
            services.AddScoped<ICatalogLookupDataService<CatalogType>, CachedCatalogTypeServiceDecorator>();
            services.AddScoped<CatalogLookupDataService<CatalogType, CatalogTypeResponse>>();
            services.AddScoped<ICatalogItemService, CachedCatalogItemServiceDecorator>();
            services.AddScoped<CatalogItemService>();

            return services;
        }
    }
}
