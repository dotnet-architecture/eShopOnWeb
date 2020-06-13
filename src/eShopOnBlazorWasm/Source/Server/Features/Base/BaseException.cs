namespace eShopOnBlazorWasm.Features.Bases
{
  using System;

  public class BaseException : Exception
  {
    public BaseException() { }

    public BaseException(string aMessage) : base(aMessage) { }
  }
}
