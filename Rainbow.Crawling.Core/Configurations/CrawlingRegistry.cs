using Rainbow.Common.NLog;
using StructureMap;
using CommonLogging = Common.Logging;

namespace Rainbow.Crawling.Core.Configurations
{
    public class CrawlingRegistry : Registry
    {
        public CrawlingRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();
                     });
            For<CommonLogging.ILog>().Use(x => new NLogLoggerWrapper(NLog.LogManager.GetLogger(x.RootType.ToString()))); // For<CommonLogging.ILog>().Use('x => new NLogLoggerWrapper(NLog.LogManager.GetLogger(x.RootType.ToString())));
        }
    }
}