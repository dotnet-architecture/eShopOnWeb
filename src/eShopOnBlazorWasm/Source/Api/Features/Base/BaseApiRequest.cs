namespace eShopOnBlazorWasm.Features.Bases
{
  using System.Text.Json.Serialization;

  public abstract class BaseApiRequest: BaseRequest
  {

    [JsonIgnore]
    internal abstract string RouteFactory { get; }
  }
}
