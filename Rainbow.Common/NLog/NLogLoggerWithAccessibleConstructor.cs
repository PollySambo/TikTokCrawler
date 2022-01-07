using Common.Logging.NLog;
using NLog;

namespace Rainbow.Common.NLog
{
    public class NLogLoggerWithAccessibleConstructor : NLogLogger
    {
        protected internal NLogLoggerWithAccessibleConstructor(Logger logger) : base(logger)
        {
        }
    }
}
