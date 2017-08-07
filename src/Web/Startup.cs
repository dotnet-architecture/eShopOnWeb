using Microsoft.eShopWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Http;
using ApplicationCore.Interfaces;
using Infrastructure.FileSystem;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Identity;
using Web.Services;
using ApplicationCore.Services;
using Infrastructure.Data;

namespace Microsoft.eShopWeb
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<CatalogContext>(c =>
            {
                try
                {
                    c.UseInMemoryDatabase("Catalog");
                    //c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection"));
                    c.ConfigureWarnings(wb =>
                    {
                        //By default, in this application, we don't want to have client evaluations
                        wb.Log(RelationalEventId.QueryClientEvaluationWarning);
                    });
                }
                catch (System.Exception ex )
                {
                    var message = ex.Message;
                }                
            });

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));
                //options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddMemoryCache();
            services.AddScoped<ICatalogService, CachedCatalogService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<CatalogService>();
            services.Configure<CatalogSettings>(Configuration);
            services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<CatalogSettings>()));

            // TODO: Remove
            services.AddSingleton<IImageService, LocalFileImageService>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


            // Add memory cache services
            services.AddMemoryCache();

            // Add session related services.
            services.AddSession();

            services.AddMvc();

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                app.Map("/allservices", builder => builder.Run(async context =>
                {
                    var sb = new StringBuilder();
                    sb.Append("<h1>All Services</h1>");
                    sb.Append("<table><thead>");
                    sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                    sb.Append("</thead><tbody>");
                    foreach (var svc in _services)
                    {
                        sb.Append("<tr>");
                        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                        sb.Append($"<td>{svc.Lifetime}</td>");
                        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody></table>");
                    await context.Response.WriteAsync(sb.ToString());
                }));
            }
            else
            {
                app.UseExceptionHandler("/Catalog/Error");
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc();
        }

        public void ConfigureDevelopment(IApplicationBuilder app,
                                        IHostingEnvironment env,
                                        ILoggerFactory loggerFactory,
                                        UserManager<ApplicationUser> userManager)
        {
            Configure(app, env);

            //Seed Data
            CatalogContextSeed.SeedAsync(app, loggerFactory)
            .Wait();

            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            userManager.CreateAsync(defaultUser, "Pass@word1").Wait();
        }
    }
}
