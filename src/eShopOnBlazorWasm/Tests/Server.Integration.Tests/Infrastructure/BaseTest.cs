namespace eShopOnBlazorWasm.Server.Integration.Tests.Infrastructure
{
  using MediatR;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Microsoft.Extensions.DependencyInjection;
  using System;
  using System.Net.Http;
  using System.Text.Json;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Server;

  /// <summary>
  /// 
  /// </summary>
  /// <remarks>
  /// Based on Jimmy's SliceFixture
  /// https://github.com/jbogard/ContosoUniversityDotNetCore-Pages/blob/master/ContosoUniversity.IntegrationTests/SliceFixture.cs
  /// </remarks>
  public abstract class BaseTest
  {
    private readonly JsonSerializerOptions JsonSerializerOptions;
    private readonly IServiceScopeFactory ServiceScopeFactory;
    protected readonly HttpClient HttpClient;


    public BaseTest(WebApplicationFactory<Startup> aWebApplicationFactory, JsonSerializerOptions aJsonSerializerOptions)
    {
      ServiceScopeFactory = aWebApplicationFactory.Services.GetService<IServiceScopeFactory>();
      HttpClient = aWebApplicationFactory.CreateClient();
      JsonSerializerOptions = aJsonSerializerOptions;
    }

    protected async Task<T> ExecuteInScope<T>(Func<IServiceProvider, Task<T>> aAction)
    {
      using IServiceScope serviceScope = ServiceScopeFactory.CreateScope();
      return await aAction(serviceScope.ServiceProvider);
    }

    protected Task Send(IRequest aRequest)
    {
      return ExecuteInScope
      (
        aServiceProvider =>
        {
          IMediator mediator = aServiceProvider.GetService<IMediator>();

          return mediator.Send(aRequest);
        }
      );
    }

    protected Task<TResponse> Send<TResponse>(IRequest<TResponse> aRequest)
    {
      return ExecuteInScope
      (
        aServiceProvider =>
        {
          IMediator mediator = aServiceProvider.GetService<IMediator>();

          return mediator.Send(aRequest);
        }
      );
    }

    protected async Task<TResponse> GetJsonAsync<TResponse>(string aUri)
    {
      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(aUri);

      httpResponseMessage.EnsureSuccessStatusCode();

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      TResponse response = JsonSerializer.Deserialize<TResponse>(json, JsonSerializerOptions);

      return response;
    }

    protected async Task<TResponse> PostJsonAsync<TResponse>(string aUri, IRequest<TResponse> aRequest)
    {
      var httpContent = new StringContent(JsonSerializer.Serialize(aRequest));
      HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(aUri, httpContent);

      httpResponseMessage.EnsureSuccessStatusCode();

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      TResponse response = JsonSerializer.Deserialize<TResponse>(json, JsonSerializerOptions);

      return response;
    }

  }
}
