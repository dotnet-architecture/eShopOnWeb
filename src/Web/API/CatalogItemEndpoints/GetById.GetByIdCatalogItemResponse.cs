using System;

namespace Microsoft.eShopWeb.Web.API.CatalogItemEndpoints
{
    public class GetByIdCatalogItemResponse : BaseResponse
    {
        public GetByIdCatalogItemResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CatalogItemDto CatalogItem { get; set; }
    }
}
