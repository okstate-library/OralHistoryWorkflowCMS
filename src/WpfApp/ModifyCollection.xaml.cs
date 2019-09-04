using Model;
using Model.Transfer;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ModifyCollection : UserControl
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
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>
        /// The collection identifier.
        /// </value>
        public int CollectionId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="collectionId"/> class.
        /// </summary>
        /// <param name="collectionId">The user identifier.</param>
        public ModifyCollection(int collectionId)
        {
            InitializeComponent();

            Loaded += ModifyCollectionUserControl_Loaded;

            CollectionId = collectionId;

        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the UserUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ModifyCollectionUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (CollectionId > 0)
            {
                ViewCollection();

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
        private void SaveCollectionDetails_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {

                if (IsAddNewRecord)
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        CollectionModel = new CollectionModel()
                        {
                            CollectionName = this.CollectionNameTextBox.Text
                        },

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Add,
                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifyCollection(requestModel);

                    ShowMessage(response);
                }
                else
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        CollectionModel = new CollectionModel()
                        {
                            Id = CollectionId,
                            CollectionName = this.CollectionNameTextBox.Text,
                        },

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Edit,

                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifyCollection(requestModel);

                    ShowMessage(response);

                }
            }
            else
            {
                App.ShowMessage(false, "Fill all the fields.");
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
                UserModel = new UserModel()
                {
                    UserId = CollectionId
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

        private bool FormValidation()
        {

            if (!string.IsNullOrEmpty(CollectionNameTextBox.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Views the user.
        /// </summary>
        private void ViewCollection()
        {
            RequestModel requestModel = new RequestModel()
            {
                CollectionId = CollectionId

            };

            ResponseModel response = App.BaseUserControl.InternalService.GetCollection(requestModel);

            if (response.IsOperationSuccess)
            {
                CollectionModel collection = response.Collection;

                CollectionNameTextBox.Text = collection.CollectionName;

            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

        }

        private void ShowMessage(ResponseModel response)
        {
            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, string.Empty);

                App.BaseUserControl.InitializeComponent(false);
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        #endregion
    }
}
