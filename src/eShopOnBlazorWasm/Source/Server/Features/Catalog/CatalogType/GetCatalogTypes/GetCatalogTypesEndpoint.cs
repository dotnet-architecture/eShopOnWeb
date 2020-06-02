namespace eShopOnBlazorWasm.Features.Catalog
{
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogTypesEndpoint : BaseEndpoint<GetCatalogTypesRequest, GetCatalogTypesResponse>
  {
    [HttpGet(GetCatalogTypesRequest.Route)]
    public async Task<IActionResult> Process(GetCatalogTypesRequest aRequest) => await Send(aRequest);
  }
}
