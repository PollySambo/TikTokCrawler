namespace Rainbow.Crawling.Core.Browsers
{
    public enum BrowserType
    {
        //current chrome version supported: 83.0.4103.61
        ChromeHeadless, 
        Chrome 
    }

    public class BrowserOptions
    {
        public BrowserOptions(string host, string baseUrl, int numberOfParallelDownloads, BrowserType browserType, int elementWaitTimeoutInSeconds, int navigationTimeoutInSeconds=60, bool ssl = false, int? port = null)
        {
            Host = host;
            BaseUrl = baseUrl;
            NumberOfParallelDownloads = numberOfParallelDownloads;
            NavigationTimeoutInSeconds = navigationTimeoutInSeconds;
            SSL = ssl;
            Port = port;
            BrowserType = browserType;
            ElementWaitTimeoutInSeconds = elementWaitTimeoutInSeconds;
        }

        public string Host { get; private set; }
        public string BaseUrl { get; private set; }
        public int NumberOfParallelDownloads { get; private set; }
        public int NavigationTimeoutInSeconds { get; private set; }
        public int ElementWaitTimeoutInSeconds { get; private set; }
        public bool SSL { get; private set; }
        public int? Port { get; private set; }
        public BrowserType BrowserType { get; private set; } 
    }
}
