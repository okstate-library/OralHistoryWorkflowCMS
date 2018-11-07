#region Imports

using System;

#endregion Imports

namespace Core.Diagnostics.ExceptionManagement
{
    /// <summary>
    /// Represents a exception handling approach.
    /// </summary>
    [Serializable]
    public enum ExceptionHandlingApproach
    {
        /// <summary>
        /// Re-throw the caught exception.
        /// </summary>
        Rethrow,

        /// <summary>
        /// Sink the caught exception.
        /// </summary>
        Sink,

        /// <summary>
        /// Wrap (and throw) the caught exception.
        /// </summary>
        Wrap,

        /// <summary>
        /// Swapping the caught exception with another exception.
        /// </summary>
        Swap
    }
}