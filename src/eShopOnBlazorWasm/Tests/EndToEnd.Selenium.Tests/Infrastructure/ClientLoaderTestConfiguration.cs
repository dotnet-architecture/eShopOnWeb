namespace eShopOnBlazorWasm.EndToEnd.Tests.Infrastructure
{
  using System;
  using eShopOnBlazorWasm.Features.ClientLoaders;

  public class TestClientLoaderConfiguration : IClientLoaderConfiguration
  {
    public TimeSpan DelayTimeSpan => TimeSpan.FromMilliseconds(10);
  }
}
