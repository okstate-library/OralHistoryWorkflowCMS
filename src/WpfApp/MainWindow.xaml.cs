using MaterialDesignThemes.Wpf;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
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
        /// <summary>
        /// The snackbar
        /// </summary>
        public static Snackbar Snackbar;


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

            this.Title = Settings.Default.ApplicationTitle;

            ApplicationNameTextBlock.Text = Settings.Default.WelComeApplicationTitle;

            Loaded += MainWindow_Loaded;

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue, App.IsValidToProcess);
        }

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
                LoginWindow win1 = new LoginWindow
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                win1.ShowDialog();

                //App.BaseUserControl.UserModel = new Model.UserModel()
                //{
                //    UserId = 1,
                //    Name = "Patrick",
                //    UserType = 4,
                //    Username = "admin"
                //};

                DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue, App.IsValidToProcess);
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

            this.Title = title;
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
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //MainContentControl.Visibility = Visibility.Hidden;
            //SubContentControl.Visibility = Visibility.Visible;

            //MainContentControl.Content = null;
            //SubContentControl.Content = new Browse(MainSearchTextBox.Text.Trim());

            //MainSearchTextBox.Text = string.Empty;
        }

        private void LogoutButton_Click(object sender, RequestNavigateEventArgs e)
        {
            App.BaseUserControl.UserModel = null;

            MenuToggleButton.IsChecked = false;

            MainWindow_Loaded(null, null);
        }
    }
}
