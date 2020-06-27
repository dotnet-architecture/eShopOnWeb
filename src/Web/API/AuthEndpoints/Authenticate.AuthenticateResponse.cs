using System;

namespace Microsoft.eShopWeb.Web.API.AuthEndpoints
{
    public class AuthenticateResponse : BaseResponse
    {
        public AuthenticateResponse(Guid correlationId) : base(correlationId)
        {
        }

        public AuthenticateResponse()
        {
        }

        public bool Result { get; set; }
        public string Token { get; set; }
    }
}
