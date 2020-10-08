namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    public class ListPagedCatalogItemRequest : BaseRequest 
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int? CatalogBrandId { get; set; }
        public int? CatalogTypeId { get; set; }
    }
}
