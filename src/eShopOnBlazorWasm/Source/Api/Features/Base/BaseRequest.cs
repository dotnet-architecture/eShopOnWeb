namespace eShopOnBlazorWasm.Features.Bases
{
  using System;

  public abstract class BaseRequest
  {
    /// <summary>
    /// Unique Identifier used by logging
    /// </summary>
    public Guid CorrelationId { get; set; }

    public BaseRequest()
    {
      CorrelationId = Guid.NewGuid();
    }
  }
}
