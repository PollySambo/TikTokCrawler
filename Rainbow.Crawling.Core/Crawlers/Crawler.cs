using System;
using System.Threading;
using Common.Logging;
using OpenQA.Selenium;
using Rainbow.Crawling.Core.Browsers;
using Rainbow.Crawling.Core.Common;
using StructureMap;

namespace Rainbow.Crawling.Core.Crawlers
{
    public abstract class Crawler 
    {
        protected readonly ILog Logger;
        protected readonly IBrowserFactory BrowserFactory;

        protected Crawler( ILog logger, IBrowserFactory browserFactory)
        {
            BrowserFactory = browserFactory;
            Logger = logger;
        }

        protected virtual void LaunchBrowser()
        {
            try
            {
                Browser = BrowserFactory.Open(new BrowserOptions(Host, BaseUrl, 0, BrowserType.Chrome, 0, 0, UseSsl));
            }
            catch (WebDriverException exception)
            {
                if (exception.Message.StartsWith("Unable to bind to locking port"))
                {
                    var randomNumberOfSeconds = new Random().Next(60, 300);
                    Logger.TraceFormat("Failed because of lock. Waiting {0} seconds before trying again...", randomNumberOfSeconds);

                    // try again...
                    Thread.Sleep(randomNumberOfSeconds * 1000);

                    Logger.TraceFormat("Retrying to open the browser...");
                    Browser = BrowserFactory.Open(new BrowserOptions(Host, BaseUrl, 0, BrowserType.Chrome,
                        0,
                        0, UseSsl, new Random().Next(7000, 7100)));
                }
                else throw;
            }

            // Browser.Parameters = parameters;
            // Browser.DownloadManager = DecorateDownloadManager(Browser.DownloadManager);
        }

        protected CrawlerBrowser Browser { get; set; }
        public  BrowserType CrawlerBrowserType { get; private set; }
        public abstract string Name { get; }
        public abstract string Host { get; }

        /// <summary>
        ///     Relative URLs will use this as a base.
        /// </summary>
        public abstract string BaseUrl { get; }
        protected virtual bool UseSsl { get; set; }

    }
}
