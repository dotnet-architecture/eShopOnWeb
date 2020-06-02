namespace CounterState
{
  using AnyClone;
  using Shouldly;
  using eShopOnBlazorWasm.Features.Counters;
  using eShopOnBlazorWasm.Client.Integration.Tests.Infrastructure;

  public class Clone_Should : BaseTest
  {
    private CounterState CounterState { get; set; }

    public Clone_Should(ClientHost aWebAssemblyHost) : base(aWebAssemblyHost)
    {
      CounterState = Store.GetState<CounterState>();
    }

    public void Clone()
    {
      //Arrange
      CounterState.Initialize(aCount: 15);

      //Act
      var clone = CounterState.Clone() as CounterState;

      //Assert
      CounterState.ShouldNotBeSameAs(clone);
      CounterState.Count.ShouldBe(clone.Count);
      CounterState.Guid.ShouldNotBe(clone.Guid);
    }
  }
}
