#region imports

using System;
using log4net;

#endregion

namespace VAC.WebApp.Core.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        protected LoggerBase()
        {
           
        }

        /// <summary>
        /// Abstract property which must be overridden by the derived classes.
        /// The logger prefix is used to create the logger instance.
        /// </summary>
        protected abstract Type LogPrefix
        {
            get;
        }
                   
        public void ErrorLogging(WellKnownErrorSeverity severity, string message, Exception exception)
        {

            // retrieve the log4net logger
            ILog log = log4net.LogManager.GetLogger(this.LogPrefix);

            // check the severity; log accordingly, if log level is enabled
            switch (severity)
            {
                case WellKnownErrorSeverity.Debug:
                    if (log.IsDebugEnabled)
                    {
                        log.Debug(message, exception);
                    }

                    break;
                case WellKnownErrorSeverity.Information:
                    if (log.IsInfoEnabled)
                    {
                        log.Info(message, exception);
                    }

                    break;
                case WellKnownErrorSeverity.Warning:
                    if (log.IsWarnEnabled)
                    {
                        log.Warn(message, exception);
                    }

                    break;
                case WellKnownErrorSeverity.Error:
                    if (log.IsErrorEnabled)
                    {
                        log.Error(message, exception);
                    }

                    break;
                case WellKnownErrorSeverity.Fatal:
                    if (log.IsFatalEnabled)
                    {
                        log.Fatal(message, exception);
                    }

                    break;
            }

        }
    }
}
