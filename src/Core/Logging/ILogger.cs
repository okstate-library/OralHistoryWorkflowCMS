// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VAC.WebApp.Core.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void ErrorLogging(WellKnownErrorSeverity severity, string message, Exception exception);
    }
}
