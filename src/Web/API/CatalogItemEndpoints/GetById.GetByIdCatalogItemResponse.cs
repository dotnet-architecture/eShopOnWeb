using System;

namespace Microsoft.eShopWeb.Web.API.CatalogItemEndpoints
{
    public class GetByIdCatalogItemResponse : BaseResponse
    {
        public GetByIdCatalogItemResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdCatalogItemResponse()
        {
        }

        public CatalogItemDto CatalogItem { get; set; }
    }
}
