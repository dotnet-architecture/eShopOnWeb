using Microsoft.AspNetCore.Mvc;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    public class DeleteCatalogItemRequest : BaseRequest 
    {
        //[FromRoute]
        public int CatalogItemId { get; set; }
    }
}
