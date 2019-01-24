using MaterialDesignThemes.Wpf;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using WpfApp.Domain;
using WpfApp.Properties;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        #region Public properties

        /// <summary>
        /// The snackbar
        /// </summary>
        public static Snackbar Snackbar;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            }).ContinueWith(t =>
            {
                MainSnackbar.MessageQueue.Enqueue(Settings.Default.WelComeApplicationTitle);

            }, TaskScheduler.FromCurrentSynchronizationContext());

            Title = Settings.Default.ApplicationTitle;

            ApplicationNameTextBlock.Text = Settings.Default.WelComeApplicationTitle;

            Loaded += MainWindow_Loaded;

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue, App.IsValidToProcess, "DemoItemsListBox");

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.IsValidToProcess)
            {
                //TODO: Login window
                LoginWindow loginWindow = new LoginWindow
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                loginWindow.ShowDialog();

                //App.BaseUserControl.UserModel = new Model.UserModel()
                //{
                //    UserId = 1,
                //    Name = "Patrick",
                //    UserType = 4,
                //    Username = "admin"
                //};


                if (Application.Current != null)
                {
                    GenerateUI(App.BaseUserControl.UserModel.UserType);
                }

                DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue, App.IsValidToProcess, "DemoItemsListBox2");
            }

        }

        /// <summary>
        /// Generates the UI.
        /// </summary>
        /// <param name="userType">Type of the user.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void GenerateUI(byte userType)
        {

            UserTypeModel current = App.BaseUserControl.Usertypes.Find(u => u.Id == userType);

            if (current.IsHorizontalMenu)
            {
                MenuToggleButton.Visibility = Visibility.Collapsed;

                DemoItemsListBox2.Visibility = Visibility.Visible;
                MainScrollViewer2.Visibility = Visibility.Visible;

                DemoItemsListBox.Visibility = Visibility.Visible;
                MainScrollViewer.Visibility = Visibility.Hidden;
            }
            else
            {
                MenuToggleButton.Visibility = Visibility.Visible;

                DemoItemsListBox.Visibility = Visibility.Visible;
                MainScrollViewer.Visibility = Visibility.Visible;

                DemoItemsListBox2.Visibility = Visibility.Hidden;
                MainScrollViewer2.Visibility = Visibility.Hidden;

            }
        }

        /// <summary>
        /// Handles the OnPreviewMouseLeftButtonUp event of the UIElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //MainContentControl.Visibility = Visibility.Visible;
            //SubContentControl.Visibility = Visibility.Hidden;

            DemoItem selectedItem = (DemoItem)((Selector)sender).SelectedItem;

            string title = Settings.Default.ApplicationTitle + " - " + selectedItem.Name;

            Title = title;
            ApplicationNameTextBlock.Text = title;

            //until we had a StaysOpen glag to Drawer, this will help with scroll bars

            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //MainContentControl.Visibility = Visibility.Hidden;
            ////SubContentControl.Visibility = Visibility.Visible;

            //MainContentControl.Content = null;
            //MainContentControl.Content = new Browse(MainSearchTextBox.Text.Trim());

            //MainSearchTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Handles the Click event of the LogoutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RequestNavigateEventArgs"/> instance containing the event data.</param>
        private void LogoutButton_Click(object sender, RequestNavigateEventArgs e)
        {
            //Application.Current.Shutdown();

            MainScrollViewer.Visibility = Visibility.Visible;
            MainScrollViewer2.Visibility = Visibility.Visible;

            App.BaseUserControl.UserModel = null;

            MenuToggleButton.IsChecked = false;

            MainWindow_Loaded(null, null);


        }

        #endregion
    }
}
