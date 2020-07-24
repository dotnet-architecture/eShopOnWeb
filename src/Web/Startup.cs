using Ardalis.ListStartupServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using BlazorAdmin.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Web
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            ConfigureInMemoryDatabases(services);

            // use real database
            //ConfigureProductionServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<CatalogContext>(c =>
                c.UseInMemoryDatabase("Catalog"));

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<CatalogContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection")));

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            ConfigureServices(services);
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCookieSettings.Configure(services);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                       .AddDefaultUI()
                       .AddEntityFrameworkStores<AppIdentityDbContext>()
                                       .AddDefaultTokenProviders();

            services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();

            ConfigureCoreServices.Configure(services, Configuration);
            ConfigureWebServices.Configure(services, Configuration);

            // Add memory cache services
            services.AddMemoryCache();
            services.AddRouting(options =>
            {
                // Replace the type and the name used to refer to it with your own
                // IOutboundParameterTransformer implementation
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });
            services.AddMvc(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                         new SlugifyParameterTransformer()));

            });
            services.AddControllersWithViews();
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/Basket/Checkout");
            });
            services.AddHttpContextAccessor();
            services.AddHealthChecks();
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);

                config.Path = "/allservices";
            });

            // Blazor Admin Required Services for Prerendering
            services.AddScoped<HttpClient>(s =>
            {
                var navigationManager = s.GetRequiredService<NavigationManager>();
                return new HttpClient
                {
                    //TODO need to do it well
                    BaseAddress = new Uri("https://localhost:44315/")
                    //BaseAddress = new Uri(navigationManager.BaseUri)
                };
            });

            services.AddBlazoredLocalStorage();
            services.AddServerSideBlazor();
            services.AddScoped<AuthService>();

            _services = services; // used to debug registered services
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = new
                        {
                            status = report.Status.ToString(),
                            errors = report.Entries.Select(e => new
                            {
                                key = e.Key,
                                value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                            })
                        }.ToJson();
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseShowAllServicesMiddleware();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller:slugify=Home}/{action:slugify=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("home_page_health_check");
                endpoints.MapHealthChecks("api_health_check");
                //endpoints.MapBlazorHub("/admin");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }

}