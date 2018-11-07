using Core.Enums;
using Model;
using Model.Transfer;
using System;
using System.Windows;
using System.Windows.Controls;

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
                App.ShowMessage(true, string.Empty);
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

            ClearData();
        }

        /// <summary>
        /// Handles the Click event of the FindAndReplaceFieldButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FindAndReplaceFieldButton_Click(object sender, RoutedEventArgs e)
        {
            WellKnownFindAndReplaceType field = WellKnownFindAndReplaceType.None;

            if (!string.IsNullOrEmpty(FieldComboBox.SelectedValue.ToString()))
            {
                field = (WellKnownFindAndReplaceType)Enum.Parse(typeof(WellKnownFindAndReplaceType),
                    ((ComboBoxItem)FieldComboBox.SelectedValue).Content.ToString(), true);
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
                App.ShowMessage(true, string.Empty);
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

            ClearData();
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
        }

        #endregion
    }
}
