using Core.Enums;
using Model;
using Model.Transfer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Helper;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Reports : UserControl
    {
        #region Properties

        public List<TranscriptionModel> Transcriptions { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Interview" /> class.
        /// </summary>
        public Reports()
        {
            InitializeComponent();

            Loaded += ReportUserControl_Loaded;

        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the InterviewUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ReportUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearAll();

            PopulateIntializeView();
        }

        /// <summary>
        /// Handles the Click event of the ResetButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        /// <summary>
        /// Handles the Click event of the InterviewSubmitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ReportSubmitButton_Click(object sender, RoutedEventArgs e)
        {

            RequestModel requestModel = new RequestModel()
            {

                ReportModel = new ReportModel()
                {
                    BeginDate = BeginDateDatePicker.SelectedDate != null ? (DateTime)BeginDateDatePicker.SelectedDate : (DateTime?)null,
                    EndDate = EndDateDatePicker.SelectedDate != null ? (DateTime)EndDateDatePicker.SelectedDate : (DateTime?)null,

                    IsBornDigitally = (bool)BornDigitalMediaCheckBox.IsChecked,
                    IsConvertedDigital = (bool)ConvertedMediaCheckBox.IsChecked,

                    Interviewer = InterviewerFilteredComboBox.Text,

                    IsOnline = (bool)OnlineStatusCheckBox.IsChecked,
                    IsOffline = (bool)OfflineStatusCheckBox.IsChecked,

                    Location = PlaceTextBox.Text,
                }

            };

            ResponseModel response = App.BaseUserControl.InternalService.GetReport(requestModel);

            TranscriptionQueueTextBlock.Text = response.Transcriptions.Count.ToString() + " Record(s) found.";

            if (response.Transcriptions.Count == 0)
            {
                ExportButton.Visibility = Visibility.Hidden;
            }
            else
            {
                ExportButton.Visibility = Visibility.Visible;
                Transcriptions = response.Transcriptions;
            }


        }

        /// <summary>
        /// Handles the Click event of the ExportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            ExportHelper helper = new ExportHelper();

            helper.Export(Transcriptions);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clears all.
        /// </summary>
        private void ClearAll()
        {
            BeginDateDatePicker.SelectedDate = null;
            EndDateDatePicker.SelectedDate = null;

            BornDigitalMediaCheckBox.IsChecked = false;
            ConvertedMediaCheckBox.IsChecked = false;
                       
            OnlineStatusCheckBox.IsChecked = false;
            OfflineStatusCheckBox.IsChecked = false;

            InterviewerFilteredComboBox.Text = string.Empty;
            PlaceTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Populates the intialize view.
        /// </summary>
        private void PopulateIntializeView()
        {
            InterviewerFilteredComboBox.IsEditable = true;
            InterviewerFilteredComboBox.IsTextSearchEnabled = true;
            InterviewerFilteredComboBox.ItemsSource = ListHelper.GetPredefinedUser(WellKnownPredefinedUserType.Interviewer);
        }

        #endregion
    }
}
