#region Imports

using System;
using log4net;

#endregion Imports

[assembly: log4net.Config.XmlConfigurator()]
namespace Core.Diagnostics.Logging.Loggers
{
    /// <summary>
    /// Represents a logger for the log4net framework.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        public void Error(AggregateException exception, string message)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, string message)
        {
            throw new NotImplementedException();
        }
        #region Methods - Instance Member

        #region Methods - Instance Member - ILogger Members

        /// <summary>
        /// Creates a log entry, based on the given message and/or exception of the
        /// specified severity, originating from the given type.
        /// </summary>
        /// <param name="type">
        /// The type from which the log request originated.
        /// </param>
        /// <param name="severity">
        /// The severity of the log entry.
        /// </param>
        /// <param name="message">
        /// The message to be logged.
        /// </param>
        /// <param name="exception">
        /// The exception to be logged.
        /// </param>
        public void Log(Type type, ErrorSeverity severity, string message, Exception exception)
        {
            // retrieve the log4net logger
            ILog log = log4net.LogManager.GetLogger(type);

            #region // make log entry

            // check the severity; log accordingly, if log level is enabled
            switch (severity)
            {
                case ErrorSeverity.Debug:
                    if (log.IsDebugEnabled)
                    {
                        log.Debug(message, exception);
                    }

                    break;

                case ErrorSeverity.Information:
                    if (log.IsInfoEnabled)
                    {
                        log.Info(message, exception);
                    }

                    break;

                case ErrorSeverity.Warning:
                    if (log.IsWarnEnabled)
                    {
                        log.Warn(message, exception);
                    }

                    break;

                case ErrorSeverity.Error:
                    if (log.IsErrorEnabled)
                    {
                        log.Error(message, exception);
                    }

                    break;

                case ErrorSeverity.Fatal:
                    if (log.IsFatalEnabled)
                    {
                        log.Fatal(message, exception);
                    }

                    break;
            }

            #endregion // make log entry
        }

        #endregion Methods - Instance Member - ILogger Members

        #endregion Methods - Instance Member
    }
}