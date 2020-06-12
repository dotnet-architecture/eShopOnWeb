namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System.Text.Json.Serialization;
  public class FindCatalogItemRequest : BaseRequest, IRequest<FindCatalogItemResponse>
  {
    public const string Route = "api/catalogItem/{CatalogItemId}";

    public int CatalogItemId { get; set; }

    [JsonIgnore]
    public string RouteFactory =>
      $"{Route}?{nameof(RequestId)}={RequestId}"
      .Replace
      (
        $"{{{nameof(CatalogItemId)}}}",
        CatalogItemId.ToString(),
        System.StringComparison.OrdinalIgnoreCase
      );
  }
}
