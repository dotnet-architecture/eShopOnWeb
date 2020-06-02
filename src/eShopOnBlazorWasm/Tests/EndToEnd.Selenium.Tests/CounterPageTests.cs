namespace eShopOnBlazorWasm.EndToEnd.Tests
{
  using OpenQA.Selenium;
  using Shouldly;
  using eShopOnBlazorWasm.EndToEnd.Tests.Infrastructure;
  using static Infrastructure.WaitAndAssert;

  public class CounterPageTests : BaseTest
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="aWebDriver"></param>
    /// <param name="aServerFixture">
    /// Is a dependency as the server needs to be running
    /// but is not referenced otherwise thus the injected item is NOT stored
    /// </param>
    public CounterPageTests(IWebDriver aWebDriver, ServerFixture aServerFixture)
      : base(aWebDriver, aServerFixture)
    {
      aServerFixture.Environment = AspNetEnvironment.Development;
      aServerFixture.CreateHostBuilderDelegate = Server.Program.CreateHostBuilder;

      Navigate("/", aReload: true);
      WaitUntilLoaded();
    }

    public void HasCounterPage()
    {
      // Navigate to "Counter"
      WebDriver.FindElement(By.LinkText("Counter")).Click();

      WaitAndAssertEqual
      (
        aExpected: "Counter Page",
        aActual: () => WebDriver.FindElement(By.TagName("h1")).Text
      );

      // Observe the initial value is 3
      IWebElement countDisplayElement1 = WebDriver.FindElement(By.CssSelector("[data-qa='Counter1'] p"));
      countDisplayElement1.Text.ShouldBe("Current count: 3");

      IWebElement countDisplayElement2 = WebDriver.FindElement(By.CssSelector("[data-qa='Counter2'] p"));
      countDisplayElement2.Text.ShouldBe("Current count: 3");

      // Click the button; see it increment by 5
      IWebElement button1 = WebDriver.FindElement(By.CssSelector("[data-qa='Counter1'] button"));
      IWebElement button2 = WebDriver.FindElement(By.CssSelector("[data-qa='Counter2'] button"));
      button1.Click();
      WaitAndAssertEqual("Current count: 8", () => countDisplayElement1.Text);
      WaitAndAssertEqual("Current count: 8", () => countDisplayElement2.Text);
      button2.Click();
      WaitAndAssertEqual("Current count: 13", () => countDisplayElement1.Text);
      WaitAndAssertEqual("Current count: 13", () => countDisplayElement2.Text);
      button1.Click();
      WaitAndAssertEqual("Current count: 18", () => countDisplayElement1.Text);
      WaitAndAssertEqual("Current count: 18", () => countDisplayElement2.Text);
    }
  }
}
