using Model;
using Model.Transfer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Helper;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ModifySubseries : UserControl
    {
        /// <summary>
        /// Gets or sets the collections.
        /// </summary>
        /// <value>
        /// The collections.
        /// </value>
        public ObservableCollection<Collection> Collections { get; set; }

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether this instance is add new record.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is add new record; otherwise, <c>false</c>.
        /// </value>
        public bool IsAddNewRecord { get; set; }

        /// <summary>
        /// Gets or sets the selected identifier.
        /// </summary>
        /// <value>
        /// The selected identifier.
        /// </value>
        public short SelectedId { get; set; }
        
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int SubseriesId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifySubseries" /> class.
        /// </summary>
        public ModifySubseries()
        {
            InitializeComponent();

            Loaded += ModifySubseriesUserControl_Loaded;

            Collections = App.BaseUserControl.ObservableCollection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="subseriesId">The user identifier.</param>
        public ModifySubseries(int subseriesId)
        {
            InitializeComponent();

            Loaded += ModifySubseriesUserControl_Loaded;

            SubseriesId = subseriesId;

            Collections = App.BaseUserControl.ObservableCollection;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the UserUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ModifySubseriesUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (SubseriesId > 0)
            {
                ViewSubseries();

                IsAddNewRecord = false;
            }
            else
            {
                IsAddNewRecord = true;
            }


            DataContext = this;
        }

        /// <summary>
        /// Handles the Click event of the SaveUserDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void SaveUserDetails_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {

                short collectionId = short.Parse(CollectionComboBox.SelectedValue.ToString());

                if (IsAddNewRecord)
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        SubseryModel = new SubseryModel()
                        {
                            CollectionId = collectionId,
                            SubseryName = SubseriesNameTextBox.Text,
                        },

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Add,

                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifySubseries(requestModel);

                    ShowMessage(response);
                }
                else
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        SubseryModel = new SubseryModel()
                        {
                            Id = SubseriesId,
                            CollectionId = collectionId,
                            SubseryName = SubseriesNameTextBox.Text,
                        },

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Edit,

                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifySubseries(requestModel);

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
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            RequestModel requestModel = new RequestModel()
            {
                UserModel = new UserModel()
                {
                    UserId = SubseriesId
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
        /// Forms the validation.
        /// </summary>
        /// <returns></returns>
        private bool FormValidation()
        {
            if (!string.IsNullOrEmpty(SubseriesNameTextBox.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Views the subseries.
        /// </summary>
        private void ViewSubseries()
        {
            RequestModel requestModel = new RequestModel()
            {
                SubseriesId = SubseriesId,
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetSubseies(requestModel);

            if (response.IsOperationSuccess)
            {
                SubseryModel subseries = response.SubseriesModel;

                //WellKnownUserType userType = (WellKnownUserType)user.UserType;

                //UserTypeComboBox.SelectedIndex = user.UserType - 2;
                //NameTextBox.Text = user.Name;
                SubseriesNameTextBox.Text = subseries.SubseryName;


                //ComboBoxItem ComboItem = (ComboBoxItem)CollectionComboBox.SelectedItem;
                //CollectionComboBox.SelectedItem = 5;

                SelectedId = subseries.CollectionId;
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="response">The response.</param>
        private void ShowMessage(ResponseModel response)
        {
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
    }
}
