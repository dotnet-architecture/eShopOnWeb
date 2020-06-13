namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class UpdateCatalogItemEndpoint : BaseEndpoint<UpdateCatalogItemRequest, UpdateCatalogItemResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <remarks>
    /// Longer Description
    /// </remarks>
    /// <param name="aUpdateCatalogItemRequest"></param>
    /// <returns><see cref="UpdateCatalogItemResponse"/></returns>
    [HttpPut(UpdateCatalogItemRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(UpdateCatalogItemResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process(UpdateCatalogItemRequest aUpdateCatalogItemRequest) => await Send(aUpdateCatalogItemRequest);
  }
}
