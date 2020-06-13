namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class DeleteCatalogItemEndpoint : BaseEndpoint<DeleteCatalogItemRequest, DeleteCatalogItemResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <remarks>
    /// Longer Description
    /// </remarks>
    /// <param name="aDeleteCatalogItemRequest"></param>
    /// <returns><see cref="DeleteCatalogItemResponse"/></returns>
    [HttpDelete(DeleteCatalogItemRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(DeleteCatalogItemResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process(DeleteCatalogItemRequest aDeleteCatalogItemRequest) => await Send(aDeleteCatalogItemRequest);
  }
}
