namespace eShopOnBlazorWasm.Features.Catalogs
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogTypesEndpoint : BaseEndpoint<GetCatalogTypesRequest, GetCatalogTypesResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <remarks>
    /// Longer Description
    /// `<see cref="GetCatalogTypesRequest.Days"/>`
    /// </remarks>
    /// <param name="aGetCatalogTypesRequest"></param>
    /// <returns><see cref="GetCatalogTypesResponse"/></returns>
    [HttpGet(GetCatalogTypesRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(GetCatalogTypesResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process(GetCatalogTypesRequest aGetCatalogTypesRequest) => await Send(aGetCatalogTypesRequest);
  }
}
