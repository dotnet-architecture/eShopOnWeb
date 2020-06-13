namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogItemsPaginatedEndpoint : BaseEndpoint<GetCatalogItemsPaginatedRequest, GetCatalogItemsPaginatedResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <remarks>
    /// Longer Description
    /// </remarks>
    /// <param name="aGetCatalogItemsPaginatedRequest"></param>
    /// <returns><see cref="GetCatalogItemsPaginatedResponse"/></returns>
    [HttpGet(GetCatalogItemsPaginatedRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(GetCatalogItemsPaginatedResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process(GetCatalogItemsPaginatedRequest aGetCatalogItemsPaginatedRequest) => await Send(aGetCatalogItemsPaginatedRequest);
  }
}
