# Testing Convention

We are using Fixie which allows for a highly configurable convention. See `TestingConvention.cs` if you want to adjust it.


## Test naming conventions

The `namespace` is the name of Class being tested
The Class name is the Method/Action/Request being tested
The Method name is the Result expected stating any conditions

Example:

  Test Name:	CounterState.IncrementCounterAction_Should.Decrement_Count_Given_NegativeAmount
  File Name: `CounterState_IncrementCounterAction_Tests.cs`

  * Namespace: CounterState
  * Class: IncrementCounterAction_Should
  * Method: Decrement_Count_Given_NegativeAmount

The Filename uses the above `<Namespace>_<Class-Verb>_Tests.cs`

