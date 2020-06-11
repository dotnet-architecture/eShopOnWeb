namespace eShopOnBlazorWasm.Features.Catalog
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System.Text.Json.Serialization;
  public class GetCatalogItemsPaginatedRequest : BaseRequest, IRequest<GetCatalogItemsPaginatedResponse>
  {
    public const string Route = "api/catalogItem";

    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    [JsonIgnore]
    public string RouteFactory => 
      $"{Route}?{nameof(PageSize)}={PageSize}&{nameof(PageIndex)}={PageIndex}&{nameof(Id)}={Id}";
  }
}
