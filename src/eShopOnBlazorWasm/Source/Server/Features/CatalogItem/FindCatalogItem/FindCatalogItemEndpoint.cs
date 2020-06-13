namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class FindCatalogItemEndpoint : BaseEndpoint<FindCatalogItemRequest, FindCatalogItemResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <remarks>
    /// Longer Description
    /// </remarks>
    /// <param name="aFindCatalogItemRequest"></param>
    /// <returns><see cref="FindCatalogItemResponse"/></returns>
    [HttpGet(FindCatalogItemRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(FindCatalogItemResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process(FindCatalogItemRequest aFindCatalogItemRequest) => await Send(aFindCatalogItemRequest);
  }
}
