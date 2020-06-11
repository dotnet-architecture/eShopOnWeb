namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System.Text.Json.Serialization;

  public class GetCatalogBrandsRequest : BaseRequest, IRequest<GetCatalogBrandsResponse>
  {
    public const string Route = "api/catalogBrand";

    [JsonIgnore]
    public string RouteFactory => $"{Route}?{nameof(Id)}={Id}";
  }
}
