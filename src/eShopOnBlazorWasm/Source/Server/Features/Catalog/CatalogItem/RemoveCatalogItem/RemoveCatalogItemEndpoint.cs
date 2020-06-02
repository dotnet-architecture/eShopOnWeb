namespace eShopOnBlazorWasm.Features.Catalog
{
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class RemoveCatalogItemEndpoint : BaseEndpoint<RemoveCatalogItemRequest, RemoveCatalogItemResponse>
  {
    [HttpGet(RemoveCatalogItemRequest.Route)]
    public async Task<IActionResult> Process(RemoveCatalogItemRequest aRequest) => await Send(aRequest);
  }
}
