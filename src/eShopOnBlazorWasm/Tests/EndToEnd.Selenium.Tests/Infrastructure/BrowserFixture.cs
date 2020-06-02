namespace eShopOnBlazorWasm.EndToEnd.Tests.Infrastructure
{
  using OpenQA.Selenium;
  using OpenQA.Selenium.Chrome;
  using OpenQA.Selenium.Remote;
  using System;

  public class BrowserFixture : IDisposable
  {
    public ILogs Logs { get; }

    public IWebDriver WebDriver { get; }

    public BrowserFixture()
    {
      var chromeOptions = new ChromeOptions();

      // Comment this out if you want to watch or interact with the browser (e.g., for debugging or fun)
      // chromeOptions.AddArgument("--headless");

      // Log errors
      chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);

      // On Windows/Linux, we don't need to set opts.BinaryLocation
      // But for Travis and Mac builds we do
      string binaryLocation = Environment.GetEnvironmentVariable("TEST_CHROME_BINARY");
      if (!string.IsNullOrEmpty(binaryLocation))
      {
        chromeOptions.BinaryLocation = binaryLocation;
        Console.WriteLine($"Set {nameof(ChromeOptions)}.{nameof(chromeOptions.BinaryLocation)} to {binaryLocation}");
      }

      try
      {
        var driver = new RemoteWebDriver(chromeOptions);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        WebDriver = driver;
        WaitAndAssert.WebDriver = driver;
        Logs = new RemoteLogs(driver);
      }
      catch (WebDriverException ex)
      {
        string message =
            "Failed to connect to the web driver. Please see the readme and follow the instructions to install selenium." +
            "Remember to start the web driver with `selenium-standalone start` before running the end-to-end tests.";
        throw new InvalidOperationException(message, ex);
      }
    }

    public void Dispose() => WebDriver.Dispose();
  }
}
