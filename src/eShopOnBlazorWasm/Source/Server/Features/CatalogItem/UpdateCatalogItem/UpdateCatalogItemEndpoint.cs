namespace eShopOnBlazorWasm.Features.Catalog
{
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class UpdateCatalogItemEndpoint : BaseEndpoint<UpdateCatalogItemRequest, UpdateCatalogItemResponse>
  {
    [HttpPut(UpdateCatalogItemRequest.Route)]
    public async Task<IActionResult> Process(UpdateCatalogItemRequest aRequest) => await Send(aRequest);
  }
}
