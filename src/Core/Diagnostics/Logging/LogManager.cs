#region Imports

using System;
using Core.Configuration;

#endregion Imports

namespace Core.Diagnostics.Logging
{
    /// <summary>
    /// Manages the logging of messages.
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// Sets the configs.
        /// </summary>
        public static void SetConfigs(string filePath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            log4net.Config.XmlConfigurator.Configure(fileInfo);

            log4net.MDC.Set("machinename", Environment.MachineName.ToString());
        }

        #region Methods - Static Member

        #region Methods - Static Member - (logging)

        /// <summary>
        /// Creates a log entry on all configured loggers, based on the given
        /// message of the specified severity, originating from the
        /// given type.
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
        public static void Log(Type type, ErrorSeverity severity, object message)
        {
            // validate message text
            string messageText = (message != null) ? message.ToString() : string.Empty;

            // invoke overload
            LogManager.Log(type, severity, messageText);
        }

        /// <summary>
        /// Creates a log entry on all configured loggers, based on the given
        /// message of the specified severity, originating from the
        /// given type.
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
        public static void Log(Type type, ErrorSeverity severity, string message)
        {
            // invoke overload
            LogManager.Log(type, severity, message, null);
        }

        /// <summary>
        /// Creates a log entry on all configured loggers, based on the given
        /// message and/or exception of the specified severity, originating from the
        /// given type.
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
        public static void Log(Type type, ErrorSeverity severity, string message,
            Exception exception)
        {
            // invoke helper
            LogManager.CreateLogEntry(type, severity, message, exception);
        }

        #endregion Methods - Static Member - (logging)

        #region Methods - Static Member - (helpers)

        /// <summary>
        /// Creates a log entry on all configured loggers, based on the given
        /// message and/or exception of the specified severity, originating from the
        /// given type.
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
        private static void CreateLogEntry(Type type, ErrorSeverity severity, string message,
            Exception exception)
        {
            // retrieve collection of loggers
            LoggerCollection loggers = LogManager.GetLoggers();

            // iterate through loggers
            foreach (ILogger logger in loggers)
            {
                try
                {
                    // create log entry
                    logger.Log(type, severity, message, exception);
                }
                catch
                {
                    // sink exception
                }
            }
        }

        /// <summary>
        /// Retrieves the configured collection of loggers.
        /// </summary>
        /// <returns>
        /// The collection of configured loggers.
        /// </returns>
        private static LoggerCollection GetLoggers()
        {
            LoggerCollection loggers = new LoggerCollection();

            // create loggers
            loggers.Add(new Core.Diagnostics.Logging.Loggers.Log4NetLogger());

            return loggers;
        }

        #endregion Methods - Static Member - (helpers)

        #endregion Methods - Static Member
    }
}