using Microsoft.AspNetCore.Mvc;

namespace Microsoft.eShopWeb.Web.API.CatalogItemEndpoints
{
    public class DeleteCatalogItemRequest : BaseRequest 
    {
        //[FromRoute]
        public int CatalogItemId { get; set; }
    }
}
