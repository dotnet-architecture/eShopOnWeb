namespace eShopOnBlazorWasm.Client.Integration.Tests.Infrastructure
{
  using BlazorState;
  using Fixie;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.Extensions.DependencyInjection;
  using System;
  using System.Net.Http;
  using System.Reflection;
  using System.Text.Json;
  using eShopOnBlazorWasm.Features.ClientLoaders;


  [NotTest]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class NotTest : Attribute { }

  [NotTest]
  public class TestingConvention : Discovery, Execution, IDisposable
  {
    private readonly IServiceScopeFactory ServiceScopeFactory;
    private HttpClient ServerHttpClient;
    private WebApplicationFactory<Server.Startup> ServerWebApplicationFactory;

    public TestingConvention()
    {
      var testServices = new ServiceCollection();
      ConfigureTestServices(testServices);
      ServiceProvider serviceProvider = testServices.BuildServiceProvider();
      ServiceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();

      Classes.Where(aType => aType.IsPublic && !aType.Has<NotTest>());
      Methods.Where(aMethodInfo => aMethodInfo.Name != nameof(Setup) && !aMethodInfo.IsSpecialName);
    }

    public void Execute(TestClass aTestClass)
    {
      aTestClass.RunCases
      (
        aCase =>
        {
          using IServiceScope serviceScope = ServiceScopeFactory.CreateScope();
          object instance = serviceScope.ServiceProvider.GetService(aTestClass.Type);
          Setup(instance);
          aCase.Execute(instance);
          instance.Dispose();
        }
       );
    }

    private static void Setup(object aInstance)
    {
      MethodInfo methodInfo = aInstance.GetType().GetMethod(nameof(Setup));
      methodInfo?.Execute(aInstance);
    }

    private void ConfigureTestServices(ServiceCollection aServiceCollection)
    {
      ServerWebApplicationFactory = new WebApplicationFactory<Server.Startup>();
      ServerHttpClient = ServerWebApplicationFactory.CreateClient();

      ConfigureWebAssemblyHost(aServiceCollection);

      aServiceCollection.AddSingleton(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });


      aServiceCollection.Scan
      (
        aTypeSourceSelector => aTypeSourceSelector
          .FromAssemblyOf<TestingConvention>()
          .AddClasses(action: (aClasses) => aClasses.Where(aType => aType.IsPublic && !aType.Has<NotTest>()))
          .AsSelf()
          .WithScopedLifetime()
      );
    }

    private void ConfigureWebAssemblyHost(ServiceCollection aServiceCollection)
    {
      //var webAssemblyHostBuilder = WebAssemblyHostBuilder.CreateDefault();
      //ConfigureServices(webAssemblyHostBuilder.Services);

      //WebAssemblyHost webAssemblyHost = webAssemblyHostBuilder.Build();
      //aServiceCollection.AddSingleton(webAssemblyHost);

      var clientHostBuilder = ClientHostBuilder.CreateDefault();
      ConfigureServices(clientHostBuilder.Services);

      ClientHost clientHost = clientHostBuilder.Build();
      aServiceCollection.AddSingleton(clientHost);

    }

    private void ConfigureServices(IServiceCollection aServiceCollection)
    {
      // Need an HttpClient to talk to the Server side configured before calling AddBlazorState.
      aServiceCollection.AddSingleton(ServerHttpClient);
      aServiceCollection.AddBlazorState
      (
        aOptions => aOptions.Assemblies =
        new Assembly[] { typeof(eShopOnBlazorWasm.Client.Program).GetTypeInfo().Assembly }
      );

      aServiceCollection.AddSingleton
      (
        new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }
      );

      aServiceCollection.AddSingleton<IClientLoaderConfiguration, ClientLoaderTestConfiguration>();
    }

    private bool DisposedValue;

    protected virtual void Dispose(bool aIsDisposing)
    {
      if (!DisposedValue)
      {
        if (aIsDisposing)
        {
          ServerWebApplicationFactory.Dispose();
        }

        DisposedValue = true;
      }
    }

    public void Dispose() => Dispose(true);
  }
}
