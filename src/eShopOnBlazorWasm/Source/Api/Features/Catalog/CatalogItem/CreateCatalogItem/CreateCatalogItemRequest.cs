namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System.Text.Json.Serialization;

  public class CreateCatalogItemRequest : BaseRequest, IRequest<CreateCatalogItemResponse>
  {
    public const string Route = "api/catalogItem";

    [JsonIgnore]
    public string RouteFactory => $"{Route}?{nameof(Id)}={Id}";
  }
}
