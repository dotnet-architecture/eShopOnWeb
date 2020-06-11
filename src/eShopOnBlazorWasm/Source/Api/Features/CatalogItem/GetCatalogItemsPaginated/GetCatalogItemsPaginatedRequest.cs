namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System.Text.Json.Serialization;
  public class GetCatalogItemsPaginatedRequest : BaseApiRequest, IRequest<GetCatalogItemsPaginatedResponse>
  {
    public const string Route = "api/CatalogItems";

    /// <summary>
    /// Page Size
    /// </summary>
    /// <example>25</example>
    public int PageSize { get; set; }

    /// <summary>
    /// Page Index
    /// </summary>
    /// <example>2</example>
    public int PageIndex { get; set; }

    public int? CatalogBrandId { get; set; }

    public int? CatalogTypeId { get; set; }

    internal override string RouteFactory => 
      $"{Route}?{nameof(PageSize)}={PageSize}&{nameof(PageIndex)}={PageIndex}&{nameof(Id)}={Id}";
  }
}
