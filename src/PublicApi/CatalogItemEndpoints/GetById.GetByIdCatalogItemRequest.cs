namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    public class GetByIdCatalogItemRequest : BaseRequest 
    {
        public int CatalogItemId { get; set; }
    }
}
