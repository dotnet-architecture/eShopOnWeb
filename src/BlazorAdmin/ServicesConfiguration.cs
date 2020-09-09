using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAdmin
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddBlazorServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogBrandService, CachedCatalogBrandServiceDecorator>();
            services.AddScoped<CatalogBrandService>();
            services.AddScoped<ICatalogTypeService, CachedCatalogTypeServiceDecorator>();
            services.AddScoped<CatalogTypeService>();
            services.AddScoped<ICatalogItemService, CachedCatalogItemServiceDecorator>();
            services.AddScoped<CatalogItemService>();

            return services;
        }
    }
}
