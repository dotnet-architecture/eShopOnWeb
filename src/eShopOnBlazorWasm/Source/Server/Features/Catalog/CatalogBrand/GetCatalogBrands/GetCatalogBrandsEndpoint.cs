namespace eShopOnBlazorWasm.Features.Catalog
{
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogBrandsEndpoint : BaseEndpoint<GetCatalogBrandsRequest, GetCatalogBrandsResponse>
  {
    [HttpGet(GetCatalogBrandsRequest.Route)]
    public async Task<IActionResult> Process(GetCatalogBrandsRequest aRequest) => await Send(aRequest);
  }
}
