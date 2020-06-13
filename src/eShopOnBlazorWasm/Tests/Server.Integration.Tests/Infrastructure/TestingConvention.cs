namespace eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure
{
  using Fixie;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.Extensions.DependencyInjection;
  using System;
  using System.Reflection;
  using System.Text.Json;
  using eShopOnBlazorWasm.Server;

  [NotTest]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class NotTest : Attribute { }
  [NotTest]
  public class TestingConvention : Discovery, Execution
  {
    const string TestPostfix = "Tests";
    private readonly IServiceScopeFactory ServiceScopeFactory;

    public TestingConvention()
    {
      var testServices = new ServiceCollection();
      ConfigureTestServices(testServices);
      ServiceProvider serviceProvider = testServices.BuildServiceProvider();
      ServiceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();

      Classes.Where(aType => aType.IsPublic && !aType.Has<NotTest>());
      Methods.Where(aMethodInfo => aMethodInfo.Name != nameof(Setup));
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
      aServiceCollection.AddSingleton(new WebApplicationFactory<Startup>());
      aServiceCollection.AddSingleton(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
      aServiceCollection.Scan
      (
        aTypeSourceSelector => aTypeSourceSelector        // Start with all non abstract types in this assembly
          .FromAssemblyOf<TestingConvention>()
          .AddClasses(action: (aClasses) => aClasses.Where(aType => aType.IsPublic && !aType.Has<NotTest>()))
          .AsSelf()
          .WithScopedLifetime()
      );
    }
  }
}
