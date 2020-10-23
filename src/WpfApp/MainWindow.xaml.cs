using Core.Enums;
using MaterialDesignThemes.Wpf;
using Model;
using Model.Transfer;
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

            Title = Settings.Default.ApplicationTitle;

            ApplicationNameTextBlock.Text = Settings.Default.WelComeApplicationTitle;

            Loaded += MainWindow_Loaded;

            VersionLabel.Content = "Version " + Properties.Settings.Default.Version;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            TranscriptionQueueTextBlock.Text = App.BaseUserControl.TranscrptionQueueRecordCount.ToString();
            AllRecordsTextBlock.Text = App.BaseUserControl.BrowseRecordCount.ToString();

            ButtonColorChange(HomeButton);
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

                //App.BaseUserControl.UserModel = new UserModel()
                //{
                //    UserId = 1,
                //    Name = "Patrick",
                //    UserType = 4,
                //    Username = "admin"
                //};

                SetLabels();

                InitialControlSetup();
            }
            else
            {
                App.ShowMessage(false, " No internet access.\n Please check the connection.");
            }

        }

        /// <summary>
        /// Handles the OnPreviewMouseLeftButtonUp event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void Button_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string toolTip = "";

            if (sender.GetType().Equals(typeof(Button)))
            {
                toolTip = ((Button)sender).ToolTip.ToString();
            }
            else
            {
                toolTip = ((TextBlock)sender).ToolTip.ToString();
            }

            ApplicationPageTextBlock.Text = "- " + toolTip;

            //MenuToggleButton.IsChecked = false;

            if (toolTip.Equals("Home"))
            {
                InitialControlSetup();

                MainGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MainGrid.Visibility = Visibility.Collapsed;
            }

            if (toolTip.Equals("Search"))
            {
                this.SearchTextBox.Text = string.Empty;
            }

            ButtonBackgroundColorRest(HomeButton);
            ButtonBackgroundColorRest(InterviewButton);
            ButtonBackgroundColorRest(TranscriptionQueueButton);
            ButtonBackgroundColorRest(BrowseButton);
            ButtonBackgroundColorRest(FindnReplaceButton);
            ButtonBackgroundColorRest(ReportsButton);
            ButtonBackgroundColorRest(SettingButton);
            ButtonBackgroundColorRest(ImportButton);

            switch (toolTip)
            {
                case "Home":
                    ButtonColorChange(HomeButton);
                    break;
                case "Add Interview":
                    ButtonColorChange(InterviewButton);
                    break;
                case "Transcription Queue":
                    ButtonColorChange(TranscriptionQueueButton);
                    break;
                case "Browse":
                    ButtonColorChange(BrowseButton);
                    break;
                case "Find and Replace":
                    ButtonColorChange(FindnReplaceButton);
                    break;
                case "Controlled vocabulary":
                    break;
                case "Reports":
                    ButtonColorChange(ReportsButton);
                    break;
                case "Settings":
                    ButtonColorChange(SettingButton);
                    break;
                case "Import records":
                    ButtonColorChange(ImportButton);
                    break;

                default:
                    break;

            }
        }

        /// <summary>
        /// Handles the Click event of the LogoutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RequestNavigateEventArgs" /> instance containing the event data.</param>
        private void LogoutButton_Click(object sender, RequestNavigateEventArgs e)
        {
            MainGrid.Visibility = Visibility.Visible;

            App.BaseUserControl.UserModel = null;

            //MenuToggleButton.IsChecked = false;

            MainWindow_Loaded(null, null);
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the LatestTranscriptionsListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        void LatestTranscriptionsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement clicked = e.OriginalSource as UIElement;

            TranscriptionModel itemTranscriptionModel = ((FrameworkElement)e.OriginalSource).DataContext as TranscriptionModel;

            //MainGrid.Visibility = Visibility.Hidden;

            //if (itemTranscriptionModel != null)
            //{
            //    object parameter = itemTranscriptionModel.Id;

            //    BrowseButton.Command?.Execute(parameter);
            //}

            Window window = new Window
            {
                Title = itemTranscriptionModel.Title,
                Content = new Transcription(itemTranscriptionModel.Id, Helper.WellKnownExpander.General),
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Height = SystemParameters.PrimaryScreenHeight * .85,
                Width = SystemParameters.PrimaryScreenWidth * .85,
            };

            window.ShowDialog();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the labels.
        /// </summary>
        private void SetLabels()
        {
            if (App.Current != null)
            {
                UsernameLabel.Content = App.BaseUserControl.UserModel.Name;
                UsertypeLabel.Content = App.BaseUserControl.UserModel.UserTypeName;

                SetUserRoleAuthorizations(App.BaseUserControl.UserModel.UserType);
            }
        }

        /// <summary>
        /// Sets the user role authorizations.
        /// </summary>
        /// <param name="userType">Type of the user.</param>
        private void SetUserRoleAuthorizations(byte userType)
        {
            UserTypeModel current = App.BaseUserControl.Usertypes.Find(u => u.Id == userType);

            HorizontalDemoItemsList.Visibility = Visibility.Visible;

            AuthorizationModel model = new Domain.AuthorizationModel(App.IsValidToProcess);

            HomeButton.Visibility = Visibility.Collapsed;
            InterviewButton.Visibility = Visibility.Collapsed;
            TranscriptionQueueButton.Visibility = Visibility.Collapsed;
            BrowseButton.Visibility = Visibility.Collapsed;
            FindnReplaceButton.Visibility = Visibility.Collapsed;
            ReportsButton.Visibility = Visibility.Collapsed;
            SettingButton.Visibility = Visibility.Collapsed;
            ImportButton.Visibility = Visibility.Collapsed;

            foreach (DemoItem item in model.DemoItems)
            {
                switch (item.Name)
                {
                    case "Home":
                        HomeButton.Visibility = Visibility.Visible;
                        break;
                    case "Interview":
                        InterviewButton.Visibility = Visibility.Visible;
                        break;
                    case "Transcription Queue":
                        TranscriptionQueueButton.Visibility = Visibility.Visible;
                        break;
                    case "Browse":
                        BrowseButton.Visibility = Visibility.Visible;
                        break;
                    case "Find and Replace":
                        FindnReplaceButton.Visibility = Visibility.Visible;
                        break;
                    case "Controlled vocabulary":
                        break;
                    case "Reports":
                        ReportsButton.Visibility = Visibility.Visible;
                        break;
                    case "Setting":
                        SettingButton.Visibility = Visibility.Visible;
                        break;
                    case "Import":
                        ImportButton.Visibility = Visibility.Visible;
                        break;

                    default:
                        break;
                }

            }

        }

        /// <summary>
        /// Initials the control setup.
        /// </summary>
        private void InitialControlSetup()
        {
            if (App.BaseUserControl.UserModel != null && (WellKnownUserType)App.BaseUserControl.UserModel.UserType != WellKnownUserType.GuestUser)
            {
                RequestModel request = new RequestModel()
                {
                    IsAdminUser = App.BaseUserControl.UserModel.CurrentUserType == WellKnownUserType.AdminUser ? true : false,
                    UserModel = App.BaseUserControl.UserModel,
                };

                ResponseModel response = App.BaseUserControl.InternalService.GetPostInitializeMainForm(request);

                if (response.IsOperationSuccess)
                {
                    if (response.Transcriptions.Count > 0)
                    {
                        LatestTranscriptions.ItemsSource = response.Transcriptions;
                        LatestTranscriptions.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        LatestTranscriptions.Visibility = Visibility.Collapsed;
                    }

                    TranscriptionQueueTextBlock.Text = response.MainFormModel.TranscrptionQueueRecordCount.ToString();
                    AllRecordsTextBlock.Text = response.MainFormModel.BrowseRecordCount.ToString();
                }
            }
            else
            {
                LatestTranscriptions.ItemsSource = "No data found";
                LatestTranscriptions.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Buttons the color change.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        private void ButtonColorChange(Button btn)
        {
            btn.Background = new SolidColorBrush(Colors.Green);
        }

        /// <summary>
        /// Buttons the background color rest.
        /// </summary>
        /// <param name="btn">The BTN.</param>
        private void ButtonBackgroundColorRest(Button btn)
        {
            btn.ClearValue(Button.BackgroundProperty);
        }

        #endregion
    }
}
