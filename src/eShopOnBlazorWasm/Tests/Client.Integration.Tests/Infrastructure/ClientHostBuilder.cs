namespace eShopOnBlazorWasm.Client.Integration.Tests.Infrastructure
{
  using Microsoft.Extensions.DependencyInjection;
  using System;

  [NotTest]
  public class ClientHostBuilder
  {
    /// <summary>
    /// Gets the service collection.
    /// </summary>
    public IServiceCollection Services { get; }

    public static ClientHostBuilder CreateDefault(string[] aArgumentArray = default)
    {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
      aArgumentArray ??= Array.Empty<string>();
#pragma warning restore IDE0059 // Unnecessary assignment of a value
      var builder = new ClientHostBuilder();

      return builder;
    }

    public ClientHost Build()
    {
      // Intentionally overwrite configuration with the one we're creating.
      //Services.AddSingleton<IConfiguration>(Configuration);

      // A Blazor application always runs in a scope. Since we want to make it possible for the user
      // to configure services inside *that scope* inside their startup code, we create *both* the
      // service provider and the scope here.
      //var services = _createServiceProvider();
      //var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

      //return new ClientHost(services, scope, Configuration, RootComponents.ToArray());
      ServiceProvider serviceProvider = Services.BuildServiceProvider();
      return new ClientHost(serviceProvider);
    }

    public ClientHostBuilder()
    {

      // Private right now because we don't have much reason to expose it. This can be exposed
      // in the future if we want to give people a choice between CreateDefault and something
      // less opinionated.
      //Configuration = new WebAssemblyHostConfiguration();
      //RootComponents = new RootComponentMappingCollection();
      Services = new ServiceCollection();
      //Logging = new LoggingBuilder(Services);

      // Retrieve required attributes from JSRuntimeInvoker
      //InitializeNavigationManager(jsRuntimeInvoker);
      //InitializeDefaultServices();

      //var hostEnvironment = InitializeEnvironment(jsRuntimeInvoker);
      //HostEnvironment = hostEnvironment;

      //_createServiceProvider = () =>
      //{
      //  return Services.BuildServiceProvider(validateScopes: WebAssemblyHostEnvironmentExtensions.IsDevelopment(hostEnvironment));
      //};
    }


  }
}
