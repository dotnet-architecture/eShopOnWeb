namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using eShopOnBlazorWasm.Features.Bases;

  public class CreateCatalogItemEndpoint : BaseEndpoint<CreateCatalogItemRequest, CreateCatalogItemResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <remarks>
    /// Longer Description
    /// </remarks>
    /// <param name="aCreateCatalogItemRequest"></param>
    /// <returns><see cref="CreateCatalogItemResponse"/></returns>
    [HttpPost(CreateCatalogItemRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(CreateCatalogItemResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process(CreateCatalogItemRequest aCreateCatalogItemRequest) => await Send(aCreateCatalogItemRequest);
  }
}
