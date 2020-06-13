namespace eShopOnBlazorWasm.Features.Bases
{
  using System;

  public abstract class BaseRequest
  {
    /// <summary>
    /// Unique Identifier
    /// </summary>
    public Guid RequestId { get; set; }

    public BaseRequest()
    {
      RequestId = Guid.NewGuid();
    }
  }
}
