using Model;
using Model.Transfer;
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
        /// Initializes a new instance of the <see cref="FindReplace"/> class.
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
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void FindAndReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            RequestModel requestModel = new RequestModel()
            {

                FindReplaceModel = new FindReplaceModel()
                {
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
