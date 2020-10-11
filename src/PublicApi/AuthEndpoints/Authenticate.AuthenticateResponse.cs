using System;

namespace Microsoft.eShopWeb.PublicApi.AuthEndpoints
{
    public class AuthenticateResponse : BaseResponse
    {
        public AuthenticateResponse(Guid correlationId) : base(correlationId)
        {
        }

        public AuthenticateResponse()
        {
        }
        public bool Result { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool IsLockedOut { get; set; } = false;
        public bool IsNotAllowed { get; set; } = false;
        public bool RequiresTwoFactor { get; set; } = false;
    }
}
