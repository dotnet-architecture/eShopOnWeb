namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System.Text.Json.Serialization;

  public class GetCatalogTypesRequest : BaseRequest, IRequest<GetCatalogTypesResponse>
  {
    public const string Route = "api/catalogTypes";

    [JsonIgnore]
    public string RouteFactory => $"{Route}?{nameof(Id)}={Id}";
  }
}
