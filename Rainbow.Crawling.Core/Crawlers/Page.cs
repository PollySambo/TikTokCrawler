using System;
using HtmlAgilityPack;
using OpenQA.Selenium.Remote;
using Rainbow.Crawling.Core.Browsers;

namespace Rainbow.Crawling.Core.Crawlers
{
    public abstract class Page
    {
        internal CrawlerBrowser Browser { get; set; }
        // internal CrawlerParameters Parameters { get; set; }
        internal abstract void Parse(HtmlNode html);
        /// <summary>
        /// A function that is executed after an item has been extracted. Here is where you can persist the item.
        /// </summary>
        /// <returns>True to indicate that processing should continue, or false to stop further processing of the page.</returns>
        internal Func<dynamic, bool> PostProcessItemFunc;

        /// <summary>
        /// A function that is executed before an item has been extracted (but once basic info has been established). Here is where you can decide whether to process the item or to skip it.
        /// </summary>
        /// <returns>True if the item should be processed/extracted, or false to skip it.</returns>
        internal Func<dynamic, bool> PreProcessItemFunc;

        internal HtmlNode GetFreshDocumentNode()
        {
            var document = new HtmlDocument();
            document.LoadHtml(Browser.Driver.PageSource);
            return document.DocumentNode;
        }

        internal HtmlNode GetFreshFrameNode(string locator)
        {
            var mainContentFrame = Browser.Session.FindFrame(locator);
            var mainFrameDriver = (RemoteWebDriver)mainContentFrame.Native;
            var document = new HtmlDocument();
            document.LoadHtml(mainFrameDriver.PageSource);
            return document.DocumentNode;
        }

        protected IDisposable Section(string message, params object[] args)
        {
            return new LogSection(Browser.Logger, message, args);
        }

        protected internal void Trace(string message, params object[] args)
        {
            LogSection.Current(Browser.Logger).Trace(message, args);
        }

        protected internal void Log(string message, params object[] args)
        {
            LogSection.Current(Browser.Logger).Log(message, args);
        }

        protected internal void Info(string message, params object[] args)
        {
            LogSection.Current(Browser.Logger).Info(message, args);
        }

        protected void Warn(string message, params object[] args)
        {
            LogSection.Current(Browser.Logger).Warn(message, args);
        }

        protected void Error(string message, params object[] args)
        {
            Error(null, message, args);
        }

        protected void Error(Exception ex, string message, params object[] args)
        {
            LogSection.Current(Browser.Logger).Error(ex, message, args);
        }
    }
}