using System;

namespace Microsoft.eShopWeb.Web.API.CatalogItemEndpoints
{
    public class CreateCatalogItemResponse : BaseResponse
    {
        public CreateCatalogItemResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CatalogItemDto CatalogItem { get; set; }
    }
}
