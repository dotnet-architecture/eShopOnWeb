using Microsoft.AspNetCore.Mvc;

namespace Microsoft.eShopWeb.PublicApi.AuthEndpoints.Responses;

public class ExternalAuthResponse
{
    public ChallengeResult  challengeResult { get; set; }
}
