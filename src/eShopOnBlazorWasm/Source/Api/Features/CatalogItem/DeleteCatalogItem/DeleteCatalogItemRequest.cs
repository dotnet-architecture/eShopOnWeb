namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using MediatR;
  using System.Text.Json.Serialization;
  using eShopOnBlazorWasm.Features.Bases;

  public class DeleteCatalogItemRequest : BaseApiRequest, IRequest<DeleteCatalogItemResponse>
  {
    public const string Route = "api/catalogItem/{CatalogItemId}";

    /// <summary>
    /// The Id of CatalogItem to Delete
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