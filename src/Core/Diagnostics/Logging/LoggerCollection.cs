#region Imports

#endregion Imports

namespace Core.Diagnostics.Logging
{
    /// <summary>
    /// Represents a collection of loggers.
    /// </summary>
    public sealed class LoggerCollection : System.Collections.ObjectModel.Collection<ILogger>
    {
    }
}