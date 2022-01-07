using Common.Logging;
using Rainbow.Crawling.Core.Browsers;

namespace Rainbow.Crawling.Core.Crawlers.TikTok
{
    public class TikTokCrawler : Crawler
    {
        public TikTokCrawler(ILog logger, IBrowserFactory browserFactory) : base(logger, browserFactory)
        {
            
        }


        public void Crawl()
        {
            Browser = BrowserFactory.Open(new BrowserOptions(Host, BaseUrl, 0, BrowserType.Chrome, 0, 0, UseSsl));
            Browser.Visit($"{BaseUrl}");

            if (!Browser.Driver.Url.StartsWith("https://ww2.")) return;
        }


        public override string Name => "Tik Tok";
        public override string Host => "https://www.tiktok.com";
        public override string BaseUrl => "https://www.tiktok.com";
    }
}
