namespace eShopOnBlazorWasm.Features.CatalogBrands
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class GetCatalogBrandsEndpoint : BaseEndpoint<GetCatalogBrandsRequest, GetCatalogBrandsResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <remarks>
    /// Longer Description
    /// `<see cref="GetCatalogBrandsRequest"/>`
    /// </remarks>
    /// <param name="aGetCatalogBrandsRequest"></param>
    /// <returns><see cref="GetCatalogBrandsResponse"/></returns>
    [HttpGet(GetCatalogBrandsRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(GetCatalogBrandsResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process(GetCatalogBrandsRequest aGetCatalogBrandsRequest) => await Send(aGetCatalogBrandsRequest);
  }
}
