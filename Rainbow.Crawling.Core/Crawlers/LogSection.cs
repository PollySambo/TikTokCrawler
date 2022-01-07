using System;
using Common.Logging;

namespace Rainbow.Crawling.Core.Crawlers
{
     public class LogSection : IDisposable
    {
        private readonly ILog _logger;

        private static LogSection _current;
        private readonly LogSection _parent;

        private LogSection(ILog log)
        {
            _logger = log; //ObjectFactory.GetInstance<ILog>();

            if (_current != null)
            {
                _parent = _current;
            }

            _current = this;
        }

        public LogSection(ILog log, string message, params object[] args)
            : this(log)
        {
            Log(message, args);
        }

        public static Func<ILog, LogSection> Current = (log) => _current ?? (_current = new LogSection(log)); //new LogSection());

        private string Tabs => null == _parent ? string.Empty : _parent.Tabs + "\t";

        #region IDisposable Members

        public void Dispose()
        {
            if (_parent != null)
            {
                _current = _parent;
            }
        }

        #endregion

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(Tabs + string.Format(message, args));
        }

        public void Info(string message, params object[] args)
        {
            _logger.Info(Tabs + string.Format(message, args));
        }

        public void Log(string message, params object[] args)
        {
            _logger.Debug(Tabs + string.Format(message, args));
        }

        public void Error(Exception exception, string message, object[] args)
        {
            _logger.Error(Tabs + string.Format(message, args), exception);
        }

        public void Error(string message, object[] args)
        {
            _logger.Error(Tabs + string.Format(message, args));
        }

        public void Error(string message)
        {
            _logger.Error(Tabs + message);
        }

        public void Warn(string message, object[] args)
        {
            _logger.Warn(Tabs + string.Format(message, args));
        }

    }
}
