namespace eShopOnBlazorWasm.Features.CatalogItems
{
  using eShopOnBlazorWasm.Features.Bases;
  using MediatR;

  public class CreateCatalogItemRequest : BaseApiRequest, IRequest<CreateCatalogItemResponse>
  {
    public const string Route = "api/CatalogItems";

    /// <summary>
    /// The Id of the CatalogBrand
    /// </summary>
    /// <example>1</example>
    public int CatalogBrandId { get; set; }

    /// <summary>
    /// The Id of the CatalogType
    /// </summary>
    /// <example>2</example>
    public int CatalogTypeId { get; set; }

    /// <summary>
    /// Description of the Item
    /// </summary>
    /// <example>Super cool thing.</example>
    public string Description { get; set; }

    /// <summary>
    /// Name of the Item
    /// </summary>
    /// <example>Super Cool Item</example>
    public string Name { get; set; }

    /// <summary>
    /// Uri of image displaying item
    /// </summary>
    /// <example>https://www.gravatar.com/avatar/fb214494d2a75080e8019f5fc961a1d9</example>
    public System.Uri PictureUri { get; set; }

    /// <summary>
    /// The Price of the Item
    /// </summary>
    /// <example>999.99</example>
    public decimal Price { get; set; }

    internal override string RouteFactory => $"{Route}?{nameof(CorrelationId)}={CorrelationId}";
  }
}
