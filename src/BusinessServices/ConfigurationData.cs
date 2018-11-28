namespace BusinessServices
{
    /// <summary>
    /// Provides access to configuration data.
    /// </summary>
    internal static class ConfigurationData
    {
        /// <summary>
        /// The is database transaction needed
        /// </summary>
        private const string IsDBTransactionNeededConfigName = "IsDBTransactionNeeded";

        /// <summary>
        /// Represents a value indicating whether operational performance must be logged,
        /// by default.
        /// </summary>
        private const bool MustLogOperationalPerformanceByDefault = true;


        /// <summary>
        /// Gets a value indicating whether this instance is database transaction needed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is database transaction needed; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDBTransactionNeeded
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether operational performance must be logged.
        /// </summary>
        /// <value>
        /// <c>true</c> if operational performance must be logged; 
        /// otherwise, <c>false</c>.
        /// </value>
        public static bool MustLogOperationalPerformance
        {
            get
            {
                return ConfigurationData.MustLogOperationalPerformanceByDefault;
            }
        }

    }
}
