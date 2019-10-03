using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using Xunit;

namespace SeleniumTests
{
    public class ChromeDriverTests
    {
        private ChromeDriver _driver;
        private const string _url = "https://localhost:44315/";

        public ChromeDriverTests()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("headless");
            options.AddArguments("no-sandbox");
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                options);
        }

        [Fact]
        public void HomePageHasCatalogItem()
        {
            NavigateToHomeAndClickFirstCatalogItemButton();

            Assert.Equal($"{_url}Basket", _driver.Url);
        }

        [Fact]
        public void AddItemToCartIncrementsCartIconCount()
        {
            NavigateToHomeAndClickFirstCatalogItemButton();

            var cartIcon = _driver.FindElement(By.ClassName("esh-basketstatus-badge"));

            int itemCount = int.Parse(cartIcon.Text);
            Assert.Equal(1, itemCount);
        }

        private void NavigateToHomeAndClickFirstCatalogItemButton()
        {
            _driver.Navigate().GoToUrl(_url);
            var button = _driver.FindElement(By.ClassName("esh-catalog-button"));
            button.Click();
        }
    }
}



/*
 * 
 *                 //var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                //((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
                //var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                //var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
                //clickableElement.Click();
*/
