using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Common.Logging;
using Coypu;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Rainbow.Crawling.Core.Crawlers;
using Cookie = OpenQA.Selenium.Cookie;

namespace Rainbow.Crawling.Core.Browsers
{
    public class CrawlerBrowser : IDisposable
    {
        public readonly ILog Logger;
        private readonly string _baseUrl;
        private readonly Random random = new Random(DateTime.Now.Millisecond);

        public CrawlerBrowser(FlexibleAppHostBrowserSession session, Options browserConfig, string host, string baseUrl, ILog logger,  int numberOfParallelDownloads, BrowserType browserType)
        {
            BrowserConfig = browserConfig;
            Session = session;
            _baseUrl = baseUrl;
            Logger = logger;

            var userAgent = GetUserAgent();
            var language = GetLanguage();

            // DownloadManager = new DefaultDownloadManager(host, _baseUrl, CookieValue, documentStore, sha1Hasher, logger, userAgent, language, numberOfParallelDownloads, md5Hasher);
            //
            // Statistics = new BrowserStatistics();

            BrowserType = browserType;
        }

        public FlexibleAppHostBrowserSession Session { get; private set; }
        // public CrawlerParameters Parameters { get; set; }
        // public CrawlSession CrawlSession { get; set; }
        public Options BrowserConfig { get; private set; }
        // public BrowserStatistics Statistics { get; private set; }
        public BrowserType BrowserType { get; private set; }

        public string GetUserAgent()
        {
            return Session.ExecuteScript("return navigator.userAgent;").ToString();
        }

        public string GetLanguage()
        {
            return Session.ExecuteScript("return navigator.language;") + ",en;q=0.5";
        }

        /// <summary>
        /// The IDownloadManager that will download files.
        /// </summary>
        // public IDownloadManager DownloadManager { get; set; }

        // public void WaitForAllPendingDownloadsToComplete()
        // {
        //     var activeWaitHandles = DownloadManager.GetActiveWaitHandles();
        //     if (activeWaitHandles.Length <= 0) return;
        //     Logger.Trace($"Waiting for {activeWaitHandles.Length} download(s) to complete...");
        //     WaitHandle.WaitAll(activeWaitHandles);
        // }

        public ICookieJar Cookies => Driver.Manage().Cookies;


        private List<Cookie> supplementaryCookies = new List<Cookie>();

        public string CookieValue
        {
            get
            {
                var mainCookies = Cookies.AllCookies;
                var supplementaryCookiesNotInMainCookies = supplementaryCookies.Where(sup => !mainCookies.Select(cookie => cookie.Name).Contains(sup.Name));
                return string.Join(";", mainCookies.Union(supplementaryCookiesNotInMainCookies).Select(x => $"{x.Name}={x.Value}"));
            }
        }

        public RemoteWebDriver Driver => ((RemoteWebDriver)(Session.Native));

        #region IDisposable Members




        public void Dispose()
        {
            //TODO: This should clear out temp Firefox profile files as it calls driver.Quit() but for some reason the files build up over time
            // See http://code.google.com/p/selenium/issues/detail?id=1934

            // lets try calling quit ourselves
            Driver?.Quit();

            Session?.Dispose();

            // Logger.Info(Statistics.ToString());
        }

        #endregion

        public string PageSource => Driver.PageSource;

        public string PageText
        {
            get
            {
                var document = new HtmlDocument();
                document.LoadHtml(Driver.PageSource);
                return document.DocumentNode.InnerText;
            }
        }

        public bool WaitingEnabled { get; set; }


        // public DownloadInfo Download(string url, bool saveToDocStore = true)
        // {
        //     return Download(url, null, saveToDocStore);
        // }

        // public DownloadInfo Download(string url, string postData, bool saveToDocStore = true)
        // {
        //     return Download(url, postData, null, saveToDocStore);
        // }

        // public DownloadInfo Download(string url, string postData, Regex fileNameRegex, bool saveToDocStore = true, HttpWebRequest request = null)
        // {
        //     Wait();
        //
        //     // Always refresh download manager cookies and see if this improves number of failed downloads
        //     //if (string.IsNullOrWhiteSpace(DownloadManager.Cookie))
        //     // {
        //     RefreshDownloadManagerCookies();
        //     // }
        //
        //     Statistics.FilesDownloaded++;
        //     // we can't assume that passing null will have the same effect as invoking the overload without postData
        //     return postData == null ? DownloadManager.Download(url, Driver.Url, fileNameRegex, saveToDocStore, request) : DownloadManager.Download(url, Driver.Url, postData, fileNameRegex, null, saveToDocStore, request);
        // }

        public SelectFrom Select(string option)
        {
            return Session.Select(option);
        }

        public TPage ClickButton<TPage>(string locator, Options options = null) where TPage : Page, new()
        {
            Wait();
            Session.ClickButton(locator, options);

            return GetPage<TPage>();
        }

        public void ClickButton(string locator, Options options = null)
        {
            Wait();
            Session.ClickButton(locator, options);
        }

        public void ClickLink(string locator, Options options = null)
        {
            Wait();
            Session.ClickLink(locator, options);
        }

        public TPage ClickLink<TPage>(string locator, Options options = null) where TPage : Page, new()
        {
            Wait();
            Session.ClickLink(locator, options);

            return GetPage<TPage>();
        }

        //public FillInWith FillIn(Element element, Options options = null)
        //{
        //    return Session.FillIn(element, options);
        //}

        public FillInWith FillIn(string locator, Options options = null)
        {
            return Session.FillIn(locator, options);
        }

        public TPage Visit<TPage>(string url)
            where TPage : Page, new()
        {
            Wait();
            Visit(url);
            return GetPage<TPage>();
        }

        public void Visit(string url)
        {
            //Session.Driver.Visit(url.Replace("&amp;", "&"), null);
            Session.Visit(url.Replace("&amp;", "&"));
        }


        public TPage VisitWithSilentTimeout<TPage>(string url, int milliseconds)
            where TPage : Page, new()
        {
            Wait();
            var originalTimeout = BrowserConfig.Timeout;
            BrowserConfig.Timeout = TimeSpan.FromMilliseconds(milliseconds);
            try
            {
                Visit(url);
            }
            finally
            {
                BrowserConfig.Timeout = originalTimeout;
            }

            return GetPage<TPage>();
        }

        public TPage Visit<TPage, TViewModel>(string url, Func<TViewModel, bool> preProcessItemFunc, Func<TViewModel, bool> postProcessItemFunc)
            where TPage : Page, new()
        {
            Wait();
            Visit(url);

            return GetPage<TPage>(x => preProcessItemFunc(x), x => postProcessItemFunc(x));
        }


        public void Choose(string locator, Options options = null)
        {
            Wait();
            Session.Choose(locator, options);
        }

        public TPage Choose<TPage>(string locator, Options options = null) where TPage : Page, new()
        {
            Wait();
            Session.Choose(locator, options);

            return GetPage<TPage>();
        }


        public TPage GetPage<TPage>() where TPage : Page, new()
        {
            return GetPage<TPage>(null, null);
        }

        public TPage GetPage<TPage>(string html) where TPage : Page, new()
        {
            return GetPage<TPage>(null, null, html);
        }

        public TPage GetPageFromPageSource<TPage>() where TPage : Page, new()
        {
            return GetPage<TPage>(null, null, Driver.PageSource);
        }

        public TPage GetPage<TPage>(Func<dynamic, bool> preProcessItemFunc, Func<dynamic, bool> postProcessItemFunc, string html = null) where TPage : Page, new()
        {
            // Statistics.PagesVisited++;

            var page = new TPage { Browser = this };
            var document = new HtmlDocument();
            document.LoadHtml(html ?? Driver.PageSource);
            page.PreProcessItemFunc = preProcessItemFunc;
            page.PostProcessItemFunc = postProcessItemFunc;
            page.Parse(document.DocumentNode);

            return page;
        }

        public HtmlDocument GetHtmlDocument()
        {
            var document = new HtmlDocument();
            document.LoadHtml(Driver.PageSource);
            return document;
        }

        /// <summary>
        /// </summary>
        // internal void RefreshDownloadManagerCookies()
        // {
        //     DownloadManager.Cookie = CookieValue;
        // }



        public string GetWindowSource(string windowHandle)
        {
            var currentHandle = Driver.CurrentWindowHandle;
            try
            {
                Driver.SwitchTo().Window(windowHandle);
                return Driver.PageSource;
            }
            finally
            {
                Driver.SwitchTo().Window(currentHandle);
            }
        }
        

        internal void Wait()
        {
            if (WaitingEnabled)
                Thread.Sleep(random.Next(100, 800));
        }

        public void InjectJQuery()
        {
            // Ensuring the jquery was pulled from the server in time. 
            // The count here is to make sure that we can't get JQuery because FF got stuck
            var count = 0;
            Session.ExecuteScript("var jq_1 = document.createElement('script'); jq_1.type = 'text/javascript'; jq_1.src = '//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'; document.getElementsByTagName('body')[0].appendChild(jq_1);");

            //RJ 18/05/2018 - commenting this out because it causes a too much recursion jquery exception
            // - while (Session.ExecuteScript("return window.jQuery;") == null && count < 2500)
            while ((bool)Session.ExecuteScript("return (typeof jQuery != 'undefined');") == false && count < 2500)
            {
                Thread.Sleep(100);
                ++count;
            }
        }

        public bool JQueryExists()
        {
            return (bool) Session.ExecuteScript("return (typeof jQuery != 'undefined');");
        }

        public bool HasDialog()
        {
            try
            {
                return Driver.SwitchTo() != null && Driver.SwitchTo().Alert() != null;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public string GetDialogText()
        {
            return Driver.SwitchTo().Alert().Text;
        }

        public string TryGetDialogText()
        {
            try
            {
                return GetDialogText();
            }
            catch (NoAlertPresentException)
            {
                return null;
            }
        }

        public void StoreSupplementaryCookies(string cookies)
        {
            if (cookies == null)
                throw new ArgumentNullException("cookies");
            //e.g. JSESSIONID=3772A75AFDF874A332488A90FE830888.node1; CP=null*
            supplementaryCookies = cookies.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s =>
              {
                  var parts = s.Split('=');
                  return new Cookie(parts[0], parts[1]);
              }).ToList();
        }
    }
}