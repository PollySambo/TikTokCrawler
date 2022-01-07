using Coypu;

namespace Rainbow.Crawling.Core.Browsers
{
    public class FlexibleAppHostBrowserSession : BrowserSession
    {
        public FlexibleAppHostBrowserSession()
            : base()
        {

        }

        public FlexibleAppHostBrowserSession(SessionConfiguration sessionConfiguration)
            : base(sessionConfiguration)
        {
        }

        public void ChangeAppHost(string appHost)
        {
            SessionConfiguration.AppHost = appHost;
        }
    }
}