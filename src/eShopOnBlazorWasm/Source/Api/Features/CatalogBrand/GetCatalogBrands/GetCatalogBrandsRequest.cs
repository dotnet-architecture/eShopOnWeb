namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;
  using System.Text.Json.Serialization;

  public class GetCatalogBrandsRequest : BaseApiRequest, IRequest<GetCatalogBrandsResponse>
  {
    public const string Route = "api/CatalogBrands";

    internal override string RouteFactory => $"{Route}?{nameof(RequestId)}={RequestId}";
  }
}
