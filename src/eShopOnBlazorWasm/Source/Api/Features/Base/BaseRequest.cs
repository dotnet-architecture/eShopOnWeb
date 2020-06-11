namespace eShopOnBlazorWasm.Features.Bases
{
  using System;

  public abstract class BaseRequest
  {
    /// <summary>
    /// Unique Identifier
    /// </summary>
    public Guid Id { get; set; }

    public BaseRequest()
    {
      Id = Guid.NewGuid();
    }
  }
}
