namespace eShopOnBlazorWasm.Features.Catalog
{
  using Microsoft.AspNetCore.Mvc;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class CreateCatalogItemEndpoint : BaseEndpoint<CreateCatalogItemRequest, CreateCatalogItemResponse>
  {
    [HttpPost(CreateCatalogItemRequest.Route)]
    public async Task<IActionResult> Process(CreateCatalogItemRequest aRequest) => await Send(aRequest);
  }
}
