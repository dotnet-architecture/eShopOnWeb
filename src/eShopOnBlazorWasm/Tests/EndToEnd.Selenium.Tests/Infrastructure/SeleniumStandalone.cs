namespace eShopOnBlazorWasm.EndToEnd.Tests.Infrastructure
{
  using System;
  using System.Diagnostics;
  using System.Net.Http;

  public class SeleniumStandAlone : IDisposable
  {
    private const string SeleniumRequestUri = "http://localhost:4444/wd/hub";

    public Process Process { get; }

    public SeleniumStandAlone()
    {
      Process = new Process()
      {
        StartInfo = new ProcessStartInfo
        {
          FileName = "selenium-standalone",
          Arguments = "start",
          UseShellExecute = true
        }
      };
      Process.Start();
      WaitForSelenium().Wait();
    }

    public void Dispose() => Process?.Close();

    internal async System.Threading.Tasks.Task WaitForSelenium()
    {
      using var httpClient = new HttpClient();

      try
      {
        HttpResponseMessage response = await httpClient.GetAsync(SeleniumRequestUri);
        response.EnsureSuccessStatusCode();
      }
      catch (Exception)
      {
        Console.WriteLine("First connect attempt failed.");
        HttpResponseMessage secondResponse = await httpClient.GetAsync(SeleniumRequestUri);
        secondResponse.EnsureSuccessStatusCode();
      }
    }
  }
}
