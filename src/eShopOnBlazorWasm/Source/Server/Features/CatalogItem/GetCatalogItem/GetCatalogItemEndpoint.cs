namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogItemEndpoint : BaseEndpoint<GetCatalogItemRequest, GetCatalogItemResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <remarks>
    /// Longer Description
    /// </remarks>
    /// <param name="aGetCatalogItemRequest"></param>
    /// <returns><see cref="GetCatalogItemResponse"/></returns>
    [HttpGet(GetCatalogItemRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(GetCatalogItemResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Process(GetCatalogItemRequest aGetCatalogItemRequest) => await Send(aGetCatalogItemRequest);
  }
}
