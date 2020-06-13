namespace eShopOnBlazorWasm.Features.ClientLoaders
{
  using Microsoft.Extensions.Logging;
  using Microsoft.JSInterop;
  using System.Threading.Tasks;

  public class ClientLoader
  {
    private readonly IClientLoaderConfiguration ClientLoaderConfiguration;

    private readonly IJSRuntime JSRuntime;

    private readonly ILogger Logger;

    public ClientLoader
    (
      ILogger<ClientLoader> aLogger,
      IJSRuntime aJSRuntime,
      IClientLoaderConfiguration aClientLoaderConfiguration
    )
    {
      Logger = aLogger;
      Logger.LogDebug($"{GetType().Name}: constructor");
      JSRuntime = aJSRuntime;
      ClientLoaderConfiguration = aClientLoaderConfiguration;
    }

    public async Task InitAsync()
    {
      //await Task.Delay(TimeSpan.FromSeconds(10));
      await Task.Delay(ClientLoaderConfiguration.DelayTimeSpan);
      await LoadClient();
    }

    public async Task LoadClient()
    {
      const string LoadClientInteropName = "TimeWarp.loadClient";
      Logger.LogDebug(LoadClientInteropName);
      await JSRuntime.InvokeAsync<object>(LoadClientInteropName);
    }
  }
}
