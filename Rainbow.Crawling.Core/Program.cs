using System;
using System.Collections.Generic;
using Common.Logging;
using Rainbow.Crawling.Core.Browsers;
using Rainbow.Crawling.Core.Common;
using Rainbow.Crawling.Core.Crawlers;
using Rainbow.Crawling.Core.Crawlers.TikTok;

namespace Rainbow.Crawling.Core
{
    public class Program
    {
        private static ILog _logger;
        private static IBrowserFactory _browserFactory;
        private Crawler crawler;

        
        public Program()
        {
           
        }

        private Program(ILog logger, IBrowserFactory browserFactory)
        {

            // Logger = logger;
            // BrowserFactory = browserFactory;

            Run();
        }


        private static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            try
            {
                IoC.Initialize();

                _logger = ObjectFactory.GetInstance<ILog>();
                _browserFactory = ObjectFactory.GetInstance<IBrowserFactory>();

                var tikTokCrawler = new TikTokCrawler(_logger,  _browserFactory);

                tikTokCrawler.Crawl();

                

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
