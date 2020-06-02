namespace eShopOnBlazorWasm.EndToEnd.Tests.Infrastructure
{
  using OpenQA.Selenium;
  using OpenQA.Selenium.Support.UI;
  using Shouldly;
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Shouldly assertions, but hooked into Selenium's WebDriverWait mechanism
  /// </summary>
  public class WaitAndAssert
  {
    public static IWebDriver WebDriver;
    private static readonly TimeSpan s_DefaultTimeout = TimeSpan.FromSeconds(10);

    //public static void Collection<T>(Func<IEnumerable<T>> actualValues, params Action<T>[] elementInspectors)
    //    => WaitAssertCore(() => Assert.Collection(actualValues(), elementInspectors));

    public static void False(Func<bool> aActual)
      => WaitAssertCore(() => aActual().ShouldBeFalse());

    public static void Single<T>(Func<IEnumerable<T>> aActualValues)
      => WaitAssertCore(() => aActualValues().ShouldHaveSingleItem());

    public static void True(Func<bool> aActual)
      => WaitAssertCore(() => aActual().ShouldBeTrue());

    public static void WaitAndAssertContains(string aExpectedSubstring, Func<string> aActualString)
                  => WaitAssertCore(() => aActualString().ShouldContain(aExpectedSubstring));

    public static void WaitAndAssertEmpty<T>(Func<IEnumerable<T>> aActualValues)
      => WaitAssertCore(() => aActualValues().ShouldBeEmpty());

    public static void WaitAndAssertEqual<T>(T aExpected, Func<T> aActual)
      => WaitAssertCore(() => aActual().ShouldBe(aExpected));

    public static void WaitAndAssertNotEmpty<T>(Func<IEnumerable<T>> aActualValues)
          => WaitAssertCore(() => aActualValues().ShouldNotBeEmpty());

    private static void WaitAssertCore(Action aAssertion, TimeSpan aTimeout = default)
    {
      if (aTimeout == default)
      {
        aTimeout = s_DefaultTimeout;
      }

      try
      {
        new WebDriverWait(WebDriver, aTimeout).Until(aWebDriver =>
        {
          try
          {
            aAssertion();
            return true;
          }
          catch
          {
            return false;
          }
        });
      }
      catch (WebDriverTimeoutException)
      {
        // Instead of reporting it as a timeout, report the exception
        aAssertion();
      }
    }
  }
}
