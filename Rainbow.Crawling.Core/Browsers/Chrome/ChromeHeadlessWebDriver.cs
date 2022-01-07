using System;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Rainbow.Crawling.Core.Browsers.Chrome
{
    public class ChromeHeadlessWebDriver : SeleniumWebDriver
    {
        public ChromeHeadlessWebDriver(Browser browser) : base(CreateChromeWebDriver(), browser)
        {
        }

        private static IWebDriver CreateChromeWebDriver()
        {
            var options = new ChromeOptions(); 
            options.AddArguments("--headless");
            options.AddArguments("--no-sandbox");
            var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(240));
            return driver;

        }
    }

    public class ChromeWebDriver : SeleniumWebDriver
    {
        public ChromeWebDriver(Browser browser) : base(CreateChromeWebDriver(), browser)
        {
        }

        private static IWebDriver CreateChromeWebDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");
            options.BinaryLocation("C:\\Program Files\\Google\\Chrome\\Application");
            var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(240));
            return driver;

        }
    }
}