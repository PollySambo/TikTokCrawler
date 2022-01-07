using System;
using Common.Logging;
using Coypu;
using Coypu.Drivers;
using OpenQA.Selenium.Chrome;
using Rainbow.Crawling.Core.Browsers.Chrome;

namespace Rainbow.Crawling.Core.Browsers
{
    public interface IBrowserFactory
    {
        CrawlerBrowser Open(BrowserOptions options);
    }

    public class BrowserFactory : IBrowserFactory
    {
        private readonly ILog logger;
        //
        // private readonly ISHA1Hasher sha1Hasher;
        // private readonly IMd5Hasher md5Hasher;
        private FlexibleAppHostBrowserSession browserSession;

        public BrowserFactory()
        {
            this.logger = logger;
            // // this.documentStore = documentStore;
            // this.sha1Hasher = sha1Hasher;
            // this.md5Hasher = md5Hasher;
        }

        public CrawlerBrowser Open(BrowserOptions options)
        {
            Type driver;
            Browser browser;
            switch (options.BrowserType)
            {
                case BrowserType.Chrome:
                    driver = typeof(ChromeWebDriver);
                    browser = Browser.Chrome;
                    break;
                case BrowserType.ChromeHeadless:
                    driver = typeof(ChromeHeadlessWebDriver);
                    browser = Browser.Chrome;
                    break;
                default:
                    throw new Exception($"Browser Type {options.BrowserType} is not being handled");
            }

            var sessionConfig = new SessionConfiguration
            {
                Driver = driver,
                AppHost = options.BaseUrl,
                SSL = options.SSL, 
                Browser = browser
            };

            if (options.Port.HasValue)
                sessionConfig.Port = options.Port.Value;

            browserSession = new FlexibleAppHostBrowserSession(sessionConfig);
            // The timeout that is passed here as parameter is the page load timeout. 
            // The timeout that is passed in the SessionConfiguration is the wait timeout so I
            // had to get the native browser and explicitly assign the timeout

            switch (options.BrowserType)
            {
                case BrowserType.Chrome:
                case BrowserType.ChromeHeadless:
                    if(!(browserSession.Driver.Native is ChromeDriver chromeDriver))
                        throw new Exception("Unable to explicitly set timeout for Chrome Driver!");
                    chromeDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(options.NavigationTimeoutInSeconds);
                    break;
                default:
                    throw new Exception($"Browser Type {options.BrowserType} is not being handled");
            }

            return new CrawlerBrowser(browserSession, sessionConfig, options.Host, options.BaseUrl, logger, 0, options.BrowserType);
        }
    }
}