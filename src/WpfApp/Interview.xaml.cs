using Core.Enums;
using Model;
using Model.Transfer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Domain;
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
        /// Initializes a new instance of the <see cref="Interview" /> class.
        /// </summary>
        public Interview()
        {
            InitializeComponent();

            DataContext = new InterviewModel();

            Loaded += InterviewUserControl_Loaded;

        }

        #endregion

        #region Events

        private void ProjectCodeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = ((TextBox)sender);
            
            if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
            {
                RequestModel requestModel = new RequestModel()
                {
                    TranscriptionModel = new TranscriptionModel()
                    {
                        ProjectCode = textBox.Text
                    },
                };

                ResponseModel response = App.BaseUserControl.InternalService.GetUniqueProjectCode(requestModel);

                if (!response.IsOperationSuccess)
                {
                    App.ShowMessage(false, response.ErrorMessage);

                    ProjectCodeTextBox.Focusable = true;
                    Keyboard.Focus(ProjectCodeTextBox);
          
                }
            }
          
        }

        /// <summary>
        /// Handles the Loaded event of the InterviewUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void InterviewUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ControlsVisibility();

            ClearAll();

            PopulateIntializeView();

        }

        /// <summary>
        /// Handles the Click event of the InterviewSubmitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void InterviewSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {
                bool isANewInterviewer = !BaseUserControl.Interviewers.Contains(InterviewerFilteredComboBox.Text);

                bool isANewAudioEquipment = !string.IsNullOrEmpty(AudioEquipmentUsedFilteredComboBox.Text) &&
                    !BaseUserControl.AudioEquipments.Contains(AudioEquipmentUsedFilteredComboBox.Text);

                bool isANewVideoEquipment = !string.IsNullOrEmpty(VideoEquipmentUsedFilteredComboBox.Text) && 
                    !BaseUserControl.VideoEquipments.Contains(VideoEquipmentUsedFilteredComboBox.Text);

                Collection collectionModel = (Collection)CollectionComboBox.SelectedValue;

                KeyValuePair<int, string> subseryModel = (KeyValuePair<int, string>)SubseriesComboBox.SelectedValue;

                RequestModel requestModel = new RequestModel()
                {
                    TranscriptionModel = new TranscriptionModel()
                    {
                        Title = " ",
                        CollectionId = (short)collectionModel.Id,
                        CreatedBy = App.BaseUserControl.UserModel.UserId,
                        CreatedDate = DateTime.Today,
                        Description = DescriptionTextBox.Text,
                        AudioEquipmentUsed = AudioEquipmentUsedFilteredComboBox.Text,
                        VideoEquipmentUsed = VideoEquipmentUsedFilteredComboBox.Text,
                        //EquipmentUsed = EquipmentUsedComboBox.Text, //TODO 
                        InterviewDate = (DateTime)InterviewDateDateDatePicker.SelectedDate,
                        Interviewee = IntervieweeTextBox.Text,
                        Interviewer = InterviewerFilteredComboBox.Text,
                        InterviewerNote = NoteTextBox.Text,
                        IsAudioFormat = (bool)MediaAudioCheckBox.IsChecked,
                        IsRestriction = (bool)RestrictionYesCheckBox.IsChecked,
                        IsVideoFormat = (bool)MediaVideoCheckBox.IsChecked,
                        Keywords = KeywordsTextBox.Text,
                        LegalNote = RestrictionNoteTextBox.Text,
                        Place = PlaceTextBox.Text,
                        ReleaseForm = (bool)ReleaseFromYesCheckBox.IsChecked,
                        Subject = SubjectTextBox.Text,
                        SubseriesId = subseryModel.Key,
                        ProjectCode = ProjectCodeTextBox.Text,
                        TranscriberAssigned = TranscriberAssignedTextBox.Text,
                        EquipmentNumber = string.Empty,
                        MetadataDraft = string.Empty,
                        UpdatedBy = App.BaseUserControl.UserModel.UserId,
                        UpdatedDate = DateTime.Today,

                        IsANewInterviewer = isANewInterviewer,
                        IsANewAudioEquipment = isANewAudioEquipment,
                        IsANewVideoEquipment = isANewVideoEquipment,
                    },

                    WellKnownModificationType = Core.Enums.WellKnownModificationType.Add,

                };

                ResponseModel response = App.BaseUserControl.InternalService.ModifyTranscription(requestModel);

                if (response.IsOperationSuccess)
                {
                    App.ShowMessage(true, string.Empty);

                    ClearAll();

                    if (isANewInterviewer || isANewAudioEquipment || isANewVideoEquipment)
                    {
                        PopulateFilterTextBox();
                    }

                }
                else
                {
                    App.ShowMessage(false, response.ErrorMessage);
                }
            }

        }

        /// <summary>
        /// Handles the Check event of the ReleaseFrom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ReleaseFrom_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                ReleaseFromYesCheckBox,
                ReleaseFromYesCheckBox);
        }

        /// <summary>
        /// Handles the Check event of the Restriction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Restriction_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                RestrictionYesCheckBox,
                RestrictionNoCheckBox);
        }
             
        #endregion

        #region Methods

        /// <summary>
        /// Clears all.
        /// </summary>
        private void ClearAll()
        {
            DescriptionTextBox.Text = string.Empty;
            InterviewDateDateDatePicker.SelectedDate = null;
            IntervieweeTextBox.Text = string.Empty;
            InterviewerFilteredComboBox.Text = string.Empty;
            NoteTextBox.Text = string.Empty;
            MediaAudioCheckBox.IsChecked = false;
            RestrictionYesCheckBox.IsChecked = false;
            MediaVideoCheckBox.IsChecked = false;
            KeywordsTextBox.Text = string.Empty;
            RestrictionNoteTextBox.Text = string.Empty;
            PlaceTextBox.Text = string.Empty;
            ReleaseFromYesCheckBox.IsChecked = false;
            SubjectTextBox.Text = string.Empty;
            ProjectCodeTextBox.Text = string.Empty;
            TranscriberAssignedTextBox.Text = string.Empty;

            //EquipmentUsedComboBox.SelectedValue = null; //TODO 
            CollectionComboBox.SelectedValue = null;
            SubseriesComboBox.SelectedValue = null;
        }

        /// <summary>
        /// Controlses the visibility.
        /// </summary>
        private void ControlsVisibility()
        {
            WellKnownUserType selUserType = (WellKnownUserType)App.BaseUserControl.UserModel.UserType;

            switch (selUserType)
            {
                case WellKnownUserType.GuestUser:
                case WellKnownUserType.Student:
                case WellKnownUserType.Staff:
                    RestrictionsBorder1.Visibility = Visibility.Collapsed;
                    RestrictionsLabel.Visibility = Visibility.Collapsed;
                    RestrictionsStackPanel.Visibility = Visibility.Collapsed;
                    RestrictionsBorder2.Visibility = Visibility.Collapsed;
                    RestrictionNoteTextBox.Visibility = Visibility.Collapsed;
                    RestrictionNoteLabel.Visibility = Visibility.Collapsed;
                    break;
                case WellKnownUserType.AdminUser:
                    RestrictionsBorder1.Visibility = Visibility.Visible;
                    RestrictionsLabel.Visibility = Visibility.Visible;
                    RestrictionsStackPanel.Visibility = Visibility.Visible;
                    RestrictionsBorder2.Visibility = Visibility.Visible;
                    RestrictionNoteTextBox.Visibility = Visibility.Visible;
                    RestrictionNoteLabel.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

        }

        private bool FormValidation()
        {
            if (CollectionComboBox.SelectedValue != null &&
                SubseriesComboBox.SelectedValue != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Populates the intialize view.
        /// </summary>
        private void PopulateIntializeView()
        {
            InterviewerFilteredComboBox.IsEditable = true;
            InterviewerFilteredComboBox.IsTextSearchEnabled = false;
            InterviewerFilteredComboBox.ItemsSource = BaseUserControl.Interviewers;
            
            AudioEquipmentUsedFilteredComboBox.IsEditable = true;
            AudioEquipmentUsedFilteredComboBox.IsTextSearchEnabled = false;
            AudioEquipmentUsedFilteredComboBox.ItemsSource = BaseUserControl.AudioEquipments;
            
            VideoEquipmentUsedFilteredComboBox.IsEditable = true;
            VideoEquipmentUsedFilteredComboBox.IsTextSearchEnabled = false;
            VideoEquipmentUsedFilteredComboBox.ItemsSource = BaseUserControl.VideoEquipments;
        }

        /// <summary>
        /// Populates the filter text box.
        /// </summary>
        private void PopulateFilterTextBox()
        {
            App.BaseUserControl.InitializeComponent(false);

            PopulateIntializeView();
        }

        #endregion
    }
}

