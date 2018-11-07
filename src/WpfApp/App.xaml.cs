using Core.Diagnostics.Logging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfApp.Domain;
using WpfApp.Helper;
using WpfApp.Properties;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Application" />
    public partial class App : Application
    {
        /// <summary>
        /// Gets or sets the base user control.
        /// </summary>
        /// <value>
        /// The base user control.
        /// </value>
        public static BaseUserControl BaseUserControl { get; set; }
        
        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        /// <value>
        /// My property.
        /// </value>
        public BaseUserControl MyProperty { get; set; }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            //LogManager.SetConfigs(Settings.Default.WelComeApplicationTitle);

            base.OnStartup(e);

            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            BaseUserControl = new BaseUserControl();

            BaseUserControl.InitializeComponent();
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="sucess">if set to <c>true</c> [sucess].</param>
        /// <param name="message">The message.</param>
        public static void ShowMessage(bool sucess, string message)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                PackIcon = { Kind = sucess ? PackIconKind.CheckCircleOutline : PackIconKind.CloseCircleOutline },
                Message = { Text = sucess ? "Record submit" : message }
            };

            DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        /// <summary>
        /// Applications the dispatcher unhandled exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ShowUnhandledException(e);
        }

        /// <summary>
        /// Shows the unhandled exception.
        /// </summary>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void ShowUnhandledException(DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            var errorMessage = string.Format("An application error occurred.\nPlease check whether your data is correct and repeat the action." +
                " If this error occurs again there seems to be a more" +
                " serious malfunction in the application, and you better close it.\n\nError: {0}\n\nDo you want to continue?\n(if" +
                " you click Yes you will continue with your work, if you click No the application will close)",

            e.Exception.Message + (e.Exception.InnerException != null ? "\n" +
            e.Exception.InnerException.Message : null));
                                    
            if (MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error) == MessageBoxResult.No)
            {
                if (MessageBox.Show("WARNING: The application will close. Any changes will not be saved!\nDo you really want to close it?", 
                    "Close the application!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
