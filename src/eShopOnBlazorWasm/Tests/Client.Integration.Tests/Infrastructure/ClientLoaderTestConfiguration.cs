namespace eShopOnBlazorWasm.Client.Integration.Tests.Infrastructure
{
  using System;
  using eShopOnBlazorWasm.Features.ClientLoaders;

  [NotTest]
  public class ClientLoaderTestConfiguration : IClientLoaderConfiguration
  {
    public TimeSpan DelayTimeSpan => TimeSpan.FromMilliseconds(10);
  }
}
