using Model;
using Model.Transfer;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Helper;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Interview.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Interview : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Interview"/> class.
        /// </summary>
        public Interview()
        {
            InitializeComponent();

            SubseriesComboBox.ItemsSource = App.BaseUserControl.Subseries;

            CollectionComboBox.ItemsSource = App.BaseUserControl.Collecions;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the InterviewSubmitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void InterviewSubmitButton_Click(object sender, RoutedEventArgs e)
        {

            CollectionModel collectionModel = (CollectionModel)CollectionComboBox.SelectedValue;

            SubseryModel subseryModel = (SubseryModel)SubseriesComboBox.SelectedValue;

            RequestModel requestModel = new RequestModel()
            {

                TranscriptionModel = new TranscriptionModel()
                {
                    CollectionId = (short)collectionModel.Id,
                    CreatedBy = App.BaseUserControl.UserModel.UserId,
                    CreatedDate = DateTime.Today,
                    Description = DescriptionTextBox.Text,
                    EquipmentUsed = EquipmentUsedTextBox.Text,
                    InterviewDate = (DateTime)InterviewDateDateDatePicker.SelectedDate,
                    Interviewee = IntervieweeTextBox.Text,
                    Interviewer = InterviewerTextBox.Text,
                    InterviewerNote = NoteTextBox.Text,
                    IsAudioFormat = (bool)MediaAudioCheckBox.IsChecked,
                    IsRestriction = (bool)RestrictionYesCheckBox.IsChecked,
                    IsVideoFormat = (bool)MediaVideoCheckBox.IsChecked,
                    Keywords = KeywordsTextBox.Text,
                    LegalNote = LegalNoteTextBox.Text,
                    Place = PlaceTextBox.Text,
                    ReleaseForm = (bool)ReleaseFromYesCheckBox.IsChecked,
                    Subject = SubjectTextBox.Text,
                    SubseriesId = (int)subseryModel.Id,
                    Title = TitleTextBox.Text,
                    ProjectCode = ProjectCodeTextBox.Text,
                    TranscriberAssigned = TranscriberAssignedTextBox.Text,

                    UpdatedBy = App.BaseUserControl.UserModel.UserId,
                    UpdatedDate = DateTime.Today,
                },

                WellKnownModificationType = Core.Enums.WellKnownModificationType.Add,

            };

            ResponseModel response = App.BaseUserControl.InternalService.ModifyTranscription(requestModel);

            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, string.Empty);

                ClearData();
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        private void ReleaseFrom_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                ReleaseFromYesCheckBox,
                ReleaseFromYesCheckBox);
        }

        private void Restriction_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                RestrictionYesCheckBox,
                RestrictionNoCheckBox);
        }


        /// <summary>
        /// Handles the SelectionChanged event of the CollectionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void CollectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SubseriesComboBox.ItemsSource = null;

            //if (e.AddedItems[0] != null)
            //{
            //    CollectionModel collection = (CollectionModel)e.AddedItems[0];

            //    SubseriesComboBox.ItemsSource = App.BaseUserControl.Subseries.Select(s => s.CollectionId == collection.Id);
            //}

        }

        #endregion

        /// <summary>
        /// Clears the data.
        /// </summary>
        private void ClearData()
        {
            TitleTextBox.Text = string.Empty;
            IntervieweeTextBox.Text = string.Empty;
            InterviewerTextBox.Text = string.Empty;
            PlaceTextBox.Text = string.Empty;
            SubjectTextBox.Text = string.Empty;
            KeywordsTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
            TranscriberAssignedTextBox.Text = string.Empty;
            ProjectCodeTextBox.Text = string.Empty;
            LegalNoteTextBox.Text = string.Empty;
            EquipmentUsedTextBox.Text = string.Empty;
            NoteTextBox.Text = string.Empty;
        }

    }
}
