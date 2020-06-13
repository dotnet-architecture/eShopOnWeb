namespace eShopOnBlazorWasm.Client.Integration.Tests.Infrastructure
{
  using BlazorState;
  using MediatR;
  using Microsoft.Extensions.DependencyInjection;
  using System.Net.Http;
  using System.Threading.Tasks;

  /// <summary>
  /// 
  /// </summary>
  /// <remarks>
  /// Based on Jimmy's SliceFixture
  /// https://github.com/jbogard/ContosoUniversityDotNetCore-Pages/blob/master/ContosoUniversity.IntegrationTests/SliceFixture.cs
  /// </remarks>
  public abstract class BaseTest
  {
    private readonly IServiceScopeFactory ServiceScopeFactory;
    private readonly IServiceScope ServiceScope;
    private readonly IMediator Mediator;
    protected readonly IStore Store;
    protected readonly HttpClient HttpClient;

    public BaseTest(ClientHost aWebAssemblyHost)
    {
      ServiceScopeFactory = aWebAssemblyHost.ServiceProvider.GetService<IServiceScopeFactory>();
      ServiceScope = ServiceScopeFactory.CreateScope();
      Mediator = ServiceScope.ServiceProvider.GetService<IMediator>();
      Store = ServiceScope.ServiceProvider.GetService<IStore>();
    }

    protected Task<TResponse> Send<TResponse>(IRequest<TResponse> aRequest) => Send(aRequest);

    protected async Task Send(IRequest aRequest) => await Mediator.Send(aRequest);

  }
}


