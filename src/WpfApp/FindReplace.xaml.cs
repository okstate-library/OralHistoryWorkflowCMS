using Core.Enums;
using Model;
using Model.Transfer;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for FindReplace.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class FindReplace : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FindReplace" /> class.
        /// </summary>
        public FindReplace()
        {
            InitializeComponent();

            DataContext = new FindandReplaceModel();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the FindAndReplaceButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void FindAndReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormFindAndReplaceValidation())
            {
                WellKnownFindAndReplaceType field = WellKnownFindAndReplaceType.None;

                RequestModel requestModel = new RequestModel()
                {
                    FindReplaceModel = new FindReplaceModel()
                    {
                        Field = field,
                        FindWord = FindTextBox.Text,
                        ReplaceWord = ReplaceTextBox.Text
                    },

                };

                ResponseModel response = App.BaseUserControl.InternalService.FindReplace(requestModel);

                if (response.IsOperationSuccess)
                {
                    App.ShowMessage(true, response.ErrorMessage + " record(s) updated.");
                }
                else
                {
                    App.ShowMessage(false, response.ErrorMessage);
                }

                ClearData();
            }

        }
        
        /// <summary>
        /// Handles the Click event of the FindAndReplaceFieldButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void FindAndReplaceFieldButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormFindAndReplaceFieldValidation())
            {                
                WellKnownFindAndReplaceType field = WellKnownFindAndReplaceType.None;

                if (!string.IsNullOrEmpty(FieldsComboBox.SelectedValue.ToString()))
                {
                    field = (WellKnownFindAndReplaceType)Enum.Parse(typeof(WellKnownFindAndReplaceType), FieldsComboBox.SelectedValue.ToString(), true);
                }

                RequestModel requestModel = new RequestModel()
                {
                    FindReplaceModel = new FindReplaceModel()
                    {
                        Field = field,
                        FindWord = FindFieldWordTextBox.Text,
                        ReplaceWord = ReplaceFieldWordTextBox.Text
                    },
                };

                ResponseModel response = App.BaseUserControl.InternalService.FindReplace(requestModel);

                if (response.IsOperationSuccess)
                {
                    App.ShowMessage(true, response.ErrorMessage + " record(s) updated.");
                }
                else
                {
                    App.ShowMessage(false, response.ErrorMessage);
                }

                ClearData();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clears the data.
        /// </summary>
        private void ClearData()
        {
            FindTextBox.Text = string.Empty;
            ReplaceTextBox.Text = string.Empty;

            FindFieldWordTextBox.Text = string.Empty;
            ReplaceFieldWordTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Forms the find and replace validation.
        /// </summary>
        /// <returns></returns>
        private bool FormFindAndReplaceValidation()
        {
            if (!string.IsNullOrEmpty(FindTextBox.Text) &&
                      !string.IsNullOrEmpty(ReplaceTextBox.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Forms the find and replace field validation.
        /// </summary>
        /// <returns></returns>
        private bool FormFindAndReplaceFieldValidation()
        {
            if (!string.IsNullOrEmpty(FindFieldWordTextBox.Text) &&
                      !string.IsNullOrEmpty(ReplaceFieldWordTextBox.Text) &&
                      !string.IsNullOrEmpty(FieldsComboBox.SelectedValue.ToString()))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
