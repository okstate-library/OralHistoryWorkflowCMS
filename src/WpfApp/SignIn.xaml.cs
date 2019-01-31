using Model;
using Model.Transfer;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : UserControl
    {
        #region Public properties

        public Boolean IsClose { get; internal set; }

        #endregion

        #region Constructor

        public SignIn()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {

            RequestModel requestModel = new RequestModel()
            {

                UserModel = new UserModel
                {
                    Username = UsernameTextBox.Text.Trim(),
                    Password = (PasswordTextBox.SecurePassword)
                },
            };

            ResponseModel response = App.BaseUserControl.InternalService.UserSignIn(requestModel);

            if (response.IsOperationSuccess)
            {
                App.BaseUserControl.UserModel = response.UserModel;

                IsClose = true;
            }
            else
            {
                SigninMessageLabel.Content = "Invalid credentials.";
            }

        }

        private void ButtonCancle_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}
