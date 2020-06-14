namespace eShopOnBlazorWasm.Server
{
  using AutoMapper;
  using FluentValidation.AspNetCore;
  using MediatR;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.ResponseCompression;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.OpenApi.Models;
  using Swashbuckle.AspNetCore.Swagger;
  using System;
  using System.IO;
  using System.Linq;
  using System.Net.Mime;
  using System.Reflection;
  using eShopOnBlazorWasm.Features.Bases;
  using Microsoft.eShopWeb.Infrastructure.Data;
  using Microsoft.eShopWeb.ApplicationCore.Interfaces;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.eShopWeb.ApplicationCore.Services;
  using Microsoft.eShopWeb;

  public class Startup
  {
    const string SwaggerVersion = "v1";
    string SwaggerApiTitle => $"TimeWarp.Blazor API {SwaggerVersion}";
    string SwaggerEndPoint => $"/swagger/{SwaggerVersion}/swagger.json";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureDevelopmentServices(IServiceCollection services)
    {
      // use in-memory database
      ConfigureInMemoryDatabases(services);
      services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<CatalogSettings>()));

      // use real database
      //ConfigureProductionServices(services);
    }

    private void ConfigureInMemoryDatabases(IServiceCollection aServiceCollection)
    {
      // use in-memory database
      aServiceCollection.AddDbContext<CatalogContext>
      (
        aDbContextOptionsBuilder => aDbContextOptionsBuilder.UseInMemoryDatabase("Catalog")
      );

      // Add Identity DbContext
      //aServiceCollection.AddDbContext<AppIdentityDbContext>(options =>
      //    options.UseInMemoryDatabase("Identity"));

      ConfigureServices(aServiceCollection);
    }


    public void Configure
    (
      IApplicationBuilder aApplicationBuilder,
      IWebHostEnvironment aWebHostEnvironment
    )
    {
      // Enable middleware to serve generated Swagger as a JSON endpoint.
      aApplicationBuilder.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      aApplicationBuilder.UseSwaggerUI
      (
        aSwaggerUIOptions => aSwaggerUIOptions.SwaggerEndpoint(SwaggerEndPoint, SwaggerApiTitle)
      );

      aApplicationBuilder.UseResponseCompression();

      if (aWebHostEnvironment.IsDevelopment())
      {
        aApplicationBuilder.UseDeveloperExceptionPage();
        aApplicationBuilder.UseWebAssemblyDebugging();
      }

      aApplicationBuilder.UseRouting();
      aApplicationBuilder.UseEndpoints
      (
        aEndpointRouteBuilder =>
        {
          aEndpointRouteBuilder.MapControllers();
          aEndpointRouteBuilder.MapBlazorHub();
          aEndpointRouteBuilder.MapFallbackToPage("/_Host");
        }
      );
      aApplicationBuilder.UseStaticFiles();
      aApplicationBuilder.UseBlazorFrameworkFiles();
    }

    public void ConfigureServices(IServiceCollection aServiceCollection)
    {
      ConfigureApplicationCoreServices(aServiceCollection);
      aServiceCollection.AddAutoMapper(typeof(Startup).Assembly);

      aServiceCollection.AddRazorPages();
      aServiceCollection.AddServerSideBlazor();
      aServiceCollection.AddMvc()
        .AddFluentValidation
        (
          aFluentValidationMvcConfiguration =>
          {
            aFluentValidationMvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>();
            aFluentValidationMvcConfiguration.RegisterValidatorsFromAssemblyContaining<BaseRequest>();
          }
        );

      aServiceCollection.Configure<ApiBehaviorOptions>
      (
        aApiBehaviorOptions => aApiBehaviorOptions.SuppressInferBindingSourcesForParameters = true
      );

      aServiceCollection.AddResponseCompression
      (
        aResponseCompressionOptions =>
          aResponseCompressionOptions.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat
          (
            new[] { MediaTypeNames.Application.Octet }
          )
      );

      Client.Program.ConfigureServices(aServiceCollection);

      aServiceCollection.AddMediatR(typeof(Startup).Assembly);

      aServiceCollection.Scan
      (
        aTypeSourceSelector => aTypeSourceSelector
          .FromAssemblyOf<Startup>()
          .AddClasses()
          .AsSelf()
          .WithScopedLifetime()
      );
      ConfigureSwagger(aServiceCollection);
    }

    private void ConfigureApplicationCoreServices(IServiceCollection aServiceCollection)
    {
      aServiceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
    }

    private void ConfigureSwagger(IServiceCollection aServiceCollection)
    {
      // Register the Swagger generator, defining 1 or more Swagger documents
      aServiceCollection.AddSwaggerGen
        (
          aSwaggerGenOptions =>
          {
            aSwaggerGenOptions
            .SwaggerDoc
            (
              SwaggerVersion, 
              new OpenApiInfo { Title = SwaggerApiTitle, Version = SwaggerVersion }
            );
            aSwaggerGenOptions.EnableAnnotations();
            

            // Set the comments path for the Swagger JSON and UI from Server.
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            aSwaggerGenOptions.IncludeXmlComments(xmlPath);

            // Set the comments path for the Swagger JSON and UI from API.
            xmlFile = $"{typeof(BaseRequest).Assembly.GetName().Name}.xml";
            xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            aSwaggerGenOptions.IncludeXmlComments(xmlPath);

            aSwaggerGenOptions.AddFluentValidationRules();
          }
        );
    }
  }
}
