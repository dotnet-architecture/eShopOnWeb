using System;

namespace Microsoft.eShopWeb.Web.API.CatalogItemEndpoints
{
    public class DeleteCatalogItemResponse : BaseResponse
    {
        public DeleteCatalogItemResponse(Guid correlationId) : base(correlationId)
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}
