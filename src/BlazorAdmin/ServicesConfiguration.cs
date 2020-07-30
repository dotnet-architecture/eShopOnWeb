using BlazorAdmin.Services.CatalogItemServices;
using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAdmin
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddBlazorServices(this IServiceCollection services)
        {
            services.AddScoped<Create>();
            services.AddScoped<ListPaged>();
            services.AddScoped<Delete>();
            services.AddScoped<Edit>();
            services.AddScoped<GetById>();

            services.AddScoped<ICatalogBrandService, CachedCatalogBrandServiceDecorator>();
            services.AddScoped<CatalogBrandService>();
            services.AddScoped<BlazorAdmin.Services.CatalogTypeServices.List>();

            return services;
        }
    }
}
