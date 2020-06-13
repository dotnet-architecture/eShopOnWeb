namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System;
  using System.Collections.Specialized;

  public class FindCatalogItemRequest : BaseApiRequest, IRequest<FindCatalogItemResponse>
  {
    public const string Route = "api/CatalogItems/Find";

    public int? CatalogBrandId { get; set; }

    public int? CatalogTypeId { get; set; }

    internal override string RouteFactory
    {
      get
      {
        var queryParams = new NameValueCollection
        {
          [nameof(CatalogBrandId)] = Convert.ToString(CatalogBrandId),
          [nameof(CatalogTypeId)] = Convert.ToString(CatalogTypeId)
        };
        return $"{Route}?{queryParams}";
      }
    }
  }
}
