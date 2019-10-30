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
    public partial class ModifyRepository : UserControl
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
        public int RepositoryId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="collectionId"/> class.
        /// </summary>
        /// <param name="repositoryId">The user identifier.</param>
        public ModifyRepository(int repositoryId)
        {
            InitializeComponent();

            Loaded += ModifyRepositoryUserControl_Loaded;

            RepositoryId = repositoryId;

        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the UserUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ModifyRepositoryUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (RepositoryId > 0)
            {
                ViewRepository();

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
                        RepositoryModel = new RepositoryModel()
                        {
                            RepositoryName = this.RepositoryNameTextBox.Text
                        },

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Add,
                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifyRepository(requestModel);

                    ShowMessage(response);
                }
                else
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        RepositoryModel = new RepositoryModel()
                        {
                            Id = RepositoryId,
                            RepositoryName = this.RepositoryNameTextBox.Text,
                        },

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Edit,

                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifyRepository(requestModel);

                    ShowMessage(response);

                }
            }
            else
            {
                App.ShowMessage(false, "Fill all the fields.");
            }
        }

        #endregion

        #region Private Methods

        private bool FormValidation()
        {
            if (!string.IsNullOrEmpty(RepositoryNameTextBox.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Views the user.
        /// </summary>
        private void ViewRepository()
        {
            RequestModel requestModel = new RequestModel()
            {
                RepositoryId = RepositoryId
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetRepository(requestModel);

            if (response.IsOperationSuccess)
            {
                RepositoryModel repostory = response.Repository;

                RepositoryNameTextBox.Text = repostory.RepositoryName;

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
