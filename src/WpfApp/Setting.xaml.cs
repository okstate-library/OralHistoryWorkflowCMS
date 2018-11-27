using MaterialDesignThemes.Wpf;
using Model.Transfer;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Setting : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        public Setting()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the OnDialogClosing event of the Confirmaiton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="DialogClosingEventArgs"/> instance containing the event data.</param>
        private void Confirmaiton_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter.Equals("true"))
            {
                ClearDatabase();
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Clears the database.
        /// </summary>
        private void ClearDatabase()
        {
            ResponseModel response = App.BaseUserControl.InternalService.ResetDatabase(null);

            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, "You successfully deleted \n records from database.");
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        #endregion
    }
}
