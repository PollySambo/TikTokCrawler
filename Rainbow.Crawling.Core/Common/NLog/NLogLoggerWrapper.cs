using System;
using Common.Logging;
using NLog;

namespace Rainbow.Common.NLog
{
    public class NLogLoggerWrapper : ILog
    {
        private readonly Logger RealLogger;
        private readonly ILog Logger;
        public NLogLoggerWrapper(Logger logger)
        {
            RealLogger = logger;
            Logger = new NLogLoggerWithAccessibleConstructor(RealLogger);
        }

        public void Trace(object message)
        {
            Logger.Trace(message);
        }

        public void Trace(object message, Exception exception)
        {
            Logger.Trace(message, exception);
        }

        public void TraceFormat(string format, params object[] args)
        {
            if (args != null && args.Length == 0)
            {
                Logger.Trace(format);
                return;
            }

            Logger.TraceFormat(format, args);
        }

        public void TraceFormat(string format, Exception exception, params object[] args)
        {
            Logger.TraceFormat(format, exception, args);
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.TraceFormat(formatProvider, format, args);
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Logger.TraceFormat(formatProvider, format, exception, args);
        }

        public void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Trace(formatMessageCallback);
        }

        public void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Trace(formatMessageCallback, exception);
        }

        public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Trace(formatProvider, formatMessageCallback);
        }

        public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Trace(formatProvider, formatMessageCallback);
        }

        public void Debug(object message)
        {
            Logger.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            Logger.Debug(message, exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            if (args != null && args.Length == 0)
            {
                Logger.Debug(format);
                return;
            }

            Logger.DebugFormat(format, args);
        }

        public void DebugFormat(string format, Exception exception, params object[] args)
        {
            Logger.DebugFormat(format, exception, args);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.DebugFormat(formatProvider, format, args);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Logger.DebugFormat(formatProvider, format, exception, args);
        }

        public void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Debug(formatMessageCallback);
        }

        public void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Debug(formatMessageCallback, exception);
        }

        public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Debug(formatProvider, formatMessageCallback);
        }

        public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Debug(formatProvider, formatMessageCallback, exception);
        }


        public void Info(object message)
        {
            Logger.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            Logger.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            if (args != null && args.Length == 0)
            {
                Logger.Info(format);
                return;
            }

            Logger.InfoFormat(format, args);
        }

        public void InfoFormat(string format, Exception exception, params object[] args)
        {
            Logger.InfoFormat(format, exception, args);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.InfoFormat(formatProvider, format, args);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Logger.InfoFormat(formatProvider, format, exception, args);
        }

        public void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Info(formatMessageCallback);
        }

        public void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Info(formatMessageCallback, exception);
        }

        public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Info(formatProvider, formatMessageCallback);
        }

        public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Info(formatProvider, formatMessageCallback, exception);
        }



        public void Warn(object message)
        {
            Logger.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            Logger.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            if (args != null && args.Length == 0)
            {
                Logger.Warn(format);
                return;
            }

            Logger.WarnFormat(format, args);
        }

        public void WarnFormat(string format, Exception exception, params object[] args)
        {
            Logger.WarnFormat(format, exception, args);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.WarnFormat(formatProvider, format, args);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Logger.WarnFormat(formatProvider, format, exception, args);
        }

        public void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Warn(formatMessageCallback);
        }

        public void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Warn(formatMessageCallback, exception);
        }

        public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Warn(formatProvider, formatMessageCallback);
        }

        public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Warn(formatProvider, formatMessageCallback, exception);
        }


        public void Error(object message)
        {
            Logger.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            Logger.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (args != null && args.Length == 0)
            {
                Logger.Error(format);
                return;
            }

            Logger.ErrorFormat(format, args);
        }

        public void ErrorFormat(string format, Exception exception, params object[] args)
        {
            Logger.ErrorFormat(format, exception, args);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.ErrorFormat(formatProvider, format, args);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Logger.ErrorFormat(formatProvider, format, exception, args);
        }

        public void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Error(formatMessageCallback);
        }

        public void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Error(formatMessageCallback, exception);
        }

        public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Error(formatProvider, formatMessageCallback);
        }

        public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Error(formatProvider, formatMessageCallback, exception);
        }

        public void Fatal(object message)
        {
            Logger.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            Logger.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (args != null && args.Length == 0)
            {
                Logger.Fatal(format);
                return;
            }

            Logger.FatalFormat(format, args);
        }

        public void FatalFormat(string format, Exception exception, params object[] args)
        {
            Logger.FatalFormat(format, exception, args);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.FatalFormat(formatProvider, format, args);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Logger.FatalFormat(formatProvider, format, exception, args);
        }

        public void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Fatal(formatMessageCallback);
        }

        public void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Fatal(formatMessageCallback, exception);
        }

        public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Logger.Fatal(formatProvider, formatMessageCallback);
        }

        public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            Logger.Fatal(formatProvider, formatMessageCallback, exception);
        }

        public bool IsTraceEnabled => Logger.IsTraceEnabled;

        public bool IsDebugEnabled => Logger.IsDebugEnabled;

        public bool IsInfoEnabled => Logger.IsInfoEnabled;

        public bool IsWarnEnabled => Logger.IsWarnEnabled;

        public IVariablesContext GlobalVariablesContext => Logger.GlobalVariablesContext;
        public IVariablesContext ThreadVariablesContext => Logger.ThreadVariablesContext;
        public INestedVariablesContext NestedThreadVariablesContext => Logger.NestedThreadVariablesContext;

        public bool IsErrorEnabled => Logger.IsErrorEnabled;

        public bool IsFatalEnabled => Logger.IsFatalEnabled;

    }
}
