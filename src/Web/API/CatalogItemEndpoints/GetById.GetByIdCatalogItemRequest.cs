namespace Microsoft.eShopWeb.Web.API.CatalogItemEndpoints
{
    public class GetByIdCatalogItemRequest : BaseRequest 
    {
        public int CatalogItemId { get; set; }
    }
}
