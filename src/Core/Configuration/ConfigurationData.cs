using System.Configuration;

namespace Core.Configuration
{
    /// <summary>
    /// Provides access to configuration data.
    /// </summary>
    internal static class ConfigurationData
    {
        /// <summary>
        /// The log4 net configuration file path name
        /// </summary>
        private const string Log4NetConfigurationFilePathName = "Log4netConfigPath";

        /// <summary>
        /// Gets the log4 net configuration file path.
        /// </summary>
        /// <value>
        /// The log4 net configuration file path.
        /// </value>
        public static string Log4NetConfigurationFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings[ConfigurationData.Log4NetConfigurationFilePathName].ToString();
            }
        }
    }
}