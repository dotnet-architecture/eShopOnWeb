namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System.Text.Json.Serialization;

  public class UpdateCatalogItemRequest : BaseRequest, IRequest<UpdateCatalogItemResponse>
  {
    public const string Route = "api/catalogItem";

    public int CatalogItemId { get; set; }

    [JsonIgnore]
    public string RouteFactory => $"{Route}/{CatalogItemId}?{nameof(Id)}={Id}";
  }
}
