#region imports

using System;

#endregion

namespace VAC.WebApp.Core.Logging
{
    /// <summary>
    /// Defines the well known error severity types.
    /// </summary>
    [Serializable]
    public enum WellKnownErrorSeverity
    {
        /// <summary>
        /// Very low severity, of information relevant for debugging purposes.
        /// </summary>
        Debug,

        /// <summary>
        /// Low severity, of only informational relevance.
        /// </summary>
        Information,

        /// <summary>
        /// Low-mid severity, indicating a warning of an impending error.
        /// </summary>
        Warning,

        /// <summary>
        /// Mid severity, indicating the occurrence of an error.
        /// </summary>
        Error,

        /// <summary>
        /// High severity, indicating the occurrence of an error fatal to the functioning
        /// of the application.
        /// </summary>
        Fatal
    }
}
