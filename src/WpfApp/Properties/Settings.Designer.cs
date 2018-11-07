﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp.Properties
{    
    /// <summary>
    /// Access the static settings defines in app.config file
    /// </summary>
    /// <seealso cref="System.Configuration.ApplicationSettingsBase" />
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.8.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
    {

        /// <summary>
        /// The default instance
        /// </summary>
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>
        /// The default.
        /// </value>
        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <value>
        /// The setting.
        /// </value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Setting
        {
            get
            {
                return ((string)(this["Setting"]));
            }
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Version
        {
            get
            {
                return ((string)(this["Version"]));
            }
        }

        /// <summary>
        /// Gets the wel come application title.
        /// </summary>
        /// <value>
        /// The wel come application title.
        /// </value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WelComeApplicationTitle
        {
            get
            {
                return "Welcome to " + ((string)(this["ApplicationTitle"]));
            }
        }

        /// <summary>
        /// Gets the application title.
        /// </summary>
        /// <value>
        /// The application title.
        /// </value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ApplicationTitle
        {
            get
            {
                return ((string)(this["ApplicationTitle"]));
            }
        }
        
        /// <summary>
        /// Gets the external API.
        /// </summary>
        /// <value>
        /// The external API.
        /// </value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ExternalApi
        {
            get
            {
                return ((string)(this["ExternalApi"]));
            }
        }

        /// <summary>
        /// Gets the log4net configuration path.
        /// </summary>
        /// <value>
        /// The log4net configuration path.
        /// </value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Log4netConfigPath
        {
            get
            {
                return ((string)(this["Log4netConfigPath"]));
            }
        }


    }
}