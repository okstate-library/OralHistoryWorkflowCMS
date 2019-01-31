using Model;
using Model.Transfer;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class LoginWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWindow"/> class.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the LoginButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            RequestModel requestModel = new RequestModel()
            {
                UserModel = new UserModel
                {
                    Username = UsernameTextBox.Text.Trim(),
                    Password = PasswordTextBox.SecurePassword,
                },
            };

            ResponseModel response = App.BaseUserControl.InternalService.UserSignIn(requestModel);

            if (response.IsOperationSuccess)
            {
                App.BaseUserControl.UserModel = response.UserModel;

                Close();
            }
            else
            {
                ErrorMessageTextBlock.Text = "Invalid credentials.";
            }
        }

        /// <summary>
        /// Handles the Click event of the GuestUserButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void GuestUserButton_Click(object sender, RoutedEventArgs e)
        {
            App.BaseUserControl.UserModel = new UserModel()
            {
                UserId = 5,
                Name = "Guest user",
                UserType = 1,
                Username = "guest"
            };

            Close();
        }

        /// <summary>
        /// Handles the Click event of the CancleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}
