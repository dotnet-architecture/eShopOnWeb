using System;

namespace Microsoft.eShopWeb.PublicApi;

/// <summary>
/// Base class used by API responses
/// </summary>
public abstract class BaseResponse : BaseMessage
{
    /// <summary>
    /// links the request to a response using this id
    /// </summary>
    /// <param name="correlationId"></param>
    public BaseResponse(Guid correlationId) : base()
    {
        base._correlationId = correlationId;
    }


    public BaseResponse()
    {
    }


}
