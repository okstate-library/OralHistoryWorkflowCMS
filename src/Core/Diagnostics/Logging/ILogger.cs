
#region Imports

using System;

#endregion Imports

namespace Core.Diagnostics.Logging
{
    /// <summary>
    /// Defines the functionality of a logger.
    /// </summary>
    public interface ILogger
    {
        #region Methods - Instance Member

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
        void Log(Type type, ErrorSeverity severity, string message, Exception exception);

        #endregion Methods - Instance Member
    }
}