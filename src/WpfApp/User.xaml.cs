using Core.Enums;
using Model;
using Model.Transfer;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class User : UserControl
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether this instance is add new record.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is add new record; otherwise, <c>false</c>.
        /// </value>
        public bool IsAddNewRecord { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public User(int userId)
        {
            InitializeComponent();

            Loaded += UserUserControl_Loaded;

            UserId = userId;

        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the UserUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserId > 0)
            {
                ViewUser();
                IsAddNewRecord = false;
            }
            else
            {
                IsAddNewRecord = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveUserDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveUserDetails_Click(object sender, RoutedEventArgs e)
        {

            ComboBoxItem ComboItem = (ComboBoxItem)UserTypeComboBox.SelectedItem;

            WellKnownUserType userType = (WellKnownUserType)Enum.Parse(typeof(WellKnownUserType), ComboItem.Name);

            if (IsAddNewRecord)
            {
                RequestModel requestModel = new RequestModel()
                {
                    UserModel = new UserModel()
                    {
                        UserId = UserId,
                        Name = NameTextBox.Text,
                        Username = UsernameTextBox.Text,
                        UserType = (byte)userType,
                        Password = PasswordTextBox.SecurePassword,
                    },

                    WellKnownModificationType = Core.Enums.WellKnownModificationType.Add,

                };

                ResponseModel response = App.BaseUserControl.InternalService.ModifyUser(requestModel);

                if (response.IsOperationSuccess)
                {
                    App.ShowMessage(true, string.Empty);
                }
                else
                {
                    App.ShowMessage(false, response.ErrorMessage);
                }
            }
            else
            {
                RequestModel requestModel = new RequestModel()
                {
                    UserModel = new UserModel()
                    {
                        UserId = UserId,
                        Name = NameTextBox.Text,
                        Username = UsernameTextBox.Text,
                        UserType = (byte)userType,
                    },

                    WellKnownModificationType = Core.Enums.WellKnownModificationType.Edit,

                };

                ResponseModel response = App.BaseUserControl.InternalService.ModifyUser(requestModel);

                if (response.IsOperationSuccess)
                {
                    App.ShowMessage(true, string.Empty);
                }
                else
                {
                    App.ShowMessage(false, response.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ResetPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            RequestModel requestModel = new RequestModel()
            {
                UserModel = new Model.UserModel()
                {
                    UserId = UserId
                },

                WellKnownModificationType = Core.Enums.WellKnownModificationType.Reset,
            };

            ResponseModel response = App.BaseUserControl.InternalService.ModifyUser(requestModel);

            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, string.Empty);
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Views the user.
        /// </summary>
        private void ViewUser()
        {
            RequestModel requestModel = new RequestModel()
            {
                UserModel = new UserModel()
                {
                    UserId = UserId
                },
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetUser(requestModel);

            if (response.IsOperationSuccess)
            {
                UserModel user = response.UserModel;

                WellKnownUserType userType = (WellKnownUserType)user.UserType;

                UserTypeComboBox.SelectedIndex = user.UserType - 2;
                NameTextBox.Text = user.Name;
                UsernameTextBox.Text = user.Username;

            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

        }
        #endregion
    }
}
