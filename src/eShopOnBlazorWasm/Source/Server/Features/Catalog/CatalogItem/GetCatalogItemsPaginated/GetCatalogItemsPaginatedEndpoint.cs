namespace eShopOnBlazorWasm.Features.Catalog
{
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogItemsPaginatedEndpoint : BaseEndpoint<GetCatalogItemsPaginatedRequest, GetCatalogItemsPaginatedResponse>
  {
    [HttpGet(GetCatalogItemsPaginatedRequest.Route)]
    public async Task<IActionResult> Process(GetCatalogItemsPaginatedRequest aRequest) => await Send(aRequest);
  }
}
