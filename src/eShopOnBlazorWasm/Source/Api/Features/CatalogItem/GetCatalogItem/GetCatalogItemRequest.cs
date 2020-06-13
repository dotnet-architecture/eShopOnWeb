namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using MediatR;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogItemRequest : BaseApiRequest, IRequest<GetCatalogItemResponse>
  {
    public const string Route = "api/CatalogItems/{CatalogItemId}";

    /// <summary>
    /// The specific CatalogItemId to fetch
    /// </summary>
    /// <example>5</example>
    public int CatalogItemId { get; set; }

    internal override string RouteFactory =>
      $"{Route}?{nameof(CorrelationId)}={CorrelationId}"
      .Replace
      (
        $"{{{nameof(CatalogItemId)}}}",
        CatalogItemId.ToString(),
        System.StringComparison.OrdinalIgnoreCase
      );
  }
}