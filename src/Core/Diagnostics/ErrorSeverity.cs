#region Imports

using System;

#endregion Imports

namespace Core.Diagnostics
{
    /// <summary>
    /// Represents the severity of an error.
    /// </summary>
    [Serializable]
    public enum ErrorSeverity
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