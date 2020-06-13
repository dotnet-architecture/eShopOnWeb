namespace eShopOnBlazorWasm.Features.Bases
{
  using System;

  public abstract class BaseResponse
  {
    /// <summary>
    /// Used to correlate request and response
    /// </summary>
    public Guid RequestId { get; set; }

    /// <summary>
    /// Used to correlate request and response
    /// </summary>
    public Guid ResponseId { get; }

    public BaseResponse(Guid aRequestId) : this()
    {
      RequestId = aRequestId;
    }

    public BaseResponse()
    {
      ResponseId = Guid.NewGuid();
    }
  }
}
