namespace eShopOnBlazorWasm.Features.Catalog
{
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class FindCatalogItemEndpoint : BaseEndpoint<FindCatalogItemRequest, FindCatalogItemResponse>
  {
    [HttpGet(FindCatalogItemRequest.Route)]
    public async Task<IActionResult> Process(FindCatalogItemRequest aRequest) => await Send(aRequest);
  }
}
