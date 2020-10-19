using Core.Enums;
using MaterialDesignThemes.Wpf;
using Model;
using Model.Transfer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Helper;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Transcription.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Transcription : UserControl
    {
        #region  Public Properties

        /// <summary>
        /// Gets or sets the transcription identifier.
        /// </summary>
        /// <value>
        /// The transcription identifier.
        /// </value>
        public int TranscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the transcription model.
        /// </summary>
        /// <value>
        /// The transcription model.
        /// </value>
        public TranscriptionModel TranscriptionModel { get; set; }

        /// <summary>
        /// Gets or sets the collections.
        /// </summary>
        /// <value>
        /// The collections.
        /// </value>
        public ObservableCollection<Collection> Collections { get; set; }

        /// <summary>
        /// Gets or sets the selected identifier.
        /// </summary>
        /// <value>
        /// The selected identifier.
        /// </value>
        public short SelectedCollectionId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TranscriptionUserControl" /> class.
        /// </summary>
        /// <param name="transcriptionId">The transcription identifier.</param>
        /// <param name="expander">The expander.</param>
        public Transcription(int transcriptionId, WellKnownExpander expander)
        {
            InitializeComponent();

            DataContext = new TranscriptionUiModel();

            CollectionComboBox.ItemsSource = App.BaseUserControl.ObservableCollection;

            TranscriptionId = transcriptionId;

            if (TranscriptionId > 0)
            {
                ViewTranscription();

                switch (expander)
                {
                    case WellKnownExpander.General:
                        //Expander_Expanded(GeneralExpander, null);
                        break;
                    case WellKnownExpander.Transcript:
                        //Expander_Expanded(TranscriptExpander, null);
                        break;
                    case WellKnownExpander.Media:
                        // Expander_Expanded(MediaExpander, null);
                        break;
                    case WellKnownExpander.Metadata:
                        //Expander_Expanded(MetadataExpander, null);
                        break;
                    case WellKnownExpander.Supplemental:
                        //Expander_Expanded(SupplementalExpander, null);
                        break;
                    default:
                        break;
                }

            }

            ControlsVisibility();

            Loaded += (s, args) =>
            {
                PopulateIntializeView();
            };

        }

        #endregion

        #region Events

        private void DeleteConfirmaiton_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter.Equals("true"))
            {
                TranscriptionModel transcriptionModel = new TranscriptionModel()
                {
                    Id = TranscriptionId,
                };

                RequestModel requestModel = new RequestModel()
                {
                    TranscriptionModel = transcriptionModel,
                };

                ResponseModel response = App.BaseUserControl.InternalService.DeleteTranscription(requestModel);

                if (response.IsOperationSuccess)
                {
                    App.ShowMessageDeleteRecord(true, string.Empty);
                }
                else
                {
                    App.ShowMessage(false, response.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the TranscriptionDetailsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void TranscriptionDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            //isAnySaveButtonClicked = true;

            TranscriptionModel transcriptionModel = new TranscriptionModel
            {
                TranscriberAssigned = TranscriberAssignedFilteredComboBox.Text,
                AuditCheckCompleted = AuditCheckCompletedFilteredComboBox.Text,

                FirstEditCompleted = FirstEditCompletedFilteredComboBox.Text,
                SecondEditCompleted = SecondEditCompletedFilteredComboBox.Text,
                ThirdEditCompleted = ThirdEditCompletedFilteredComboBox.Text,

                DraftSentDate = DraftSentDatePicker.SelectedDate != null ?
                DraftSentDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                EditWithCorrectionCompleted = EditCorrectionCompletedFilteredComboBox.Text,
                FinalEditCompleted = FinalEditCompletedFilteredComboBox.Text,

                TranscriptStatus = TranscriptStatusCompleteCheckBox.IsChecked.Value,

                SentOut = SentOutYesCheckBox.IsChecked.Value,

                MetadataDraft = MetadataDraftTextBox.Text,

                IsPriority = IsPriorityYesCheckBox.IsChecked.Value,
                ReasonForPriority = ReasonofPriorityTextBox.Text,

                FinalSentDate = FinalSentDatePicker.SelectedDate != null ?
                FinalSentDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                TranscriptLocation = TranscriptionLocationTextBox.Text,
                TranscriptNote = TranscriberNoteTextBox.Text,
            };

            UpdateTranscription(transcriptionModel,
                WellKnownTranscriptionModificationType.Transcript);

        }

        /// <summary>
        /// Handles the Click event of the MediaDetailsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void MediaDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            TranscriptionModel transcriptionModel = new TranscriptionModel
            {
                IsAudioFormat = MediaAudioCheckBox.IsChecked.Value ? true : false,
                IsVideoFormat = MediaVideoCheckBox.IsChecked.Value ? true : false,

                IsBornDigital = BornDigitalYesCheckBox.IsChecked.Value,

                OriginalMediumType = AudiocassetteCheckBox.IsChecked.Value ? (byte)OriginalMediumType.AudioCassette : (byte)OriginalMediumType.Other
            };

            transcriptionModel.OriginalMediumType = MiniDVCheckBox.IsChecked.Value ? (byte)OriginalMediumType.MiniDV : (byte)OriginalMediumType.Other;
            transcriptionModel.OriginalMediumType = OtherCheckBox.IsChecked.Value ? (byte)OriginalMediumType.Other : transcriptionModel.OriginalMediumType;

            transcriptionModel.OriginalMedium = OtherDigitalFormatTextBox.Text;

            transcriptionModel.IsConvertToDigital = ConvertedYesCheckBox.IsChecked.Value;

            transcriptionModel.ConvertToDigitalDate = DateDigitalConvertedDateDatePicker.SelectedDate != null ?
                DateDigitalConvertedDateDatePicker.SelectedDate.Value.Date
                : (DateTime?)null;

            transcriptionModel.IsAccessMediaStatus = AccessMediaStatusCompleteCheckBox.IsChecked.Value;

            transcriptionModel.MasterFileLocation = MasterFileLocationTextBox.Text;
            transcriptionModel.TechnicalSpecification = TechnicalSpecificationTextBox.Text;
            transcriptionModel.AccessFileLocation = AccessFileLocationTextBox.Text;

            UpdateTranscription(transcriptionModel, WellKnownTranscriptionModificationType.Media);
        }

        /// <summary>
        /// Handles the Click event of the MetadataButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MetadataButton_Click(object sender, EventArgs e)
        {
            string interviewerName = InterviewerFilteredComboBox.Text +
                    (!string.IsNullOrEmpty(InterviewerFilteredComboBox1.Text) ? "; " + InterviewerFilteredComboBox1.Text : string.Empty) +
                   (!string.IsNullOrEmpty(InterviewerFilteredComboBox2.Text) ? "; " + InterviewerFilteredComboBox2.Text : string.Empty);

            TranscriptionModel transcriptionModel = new TranscriptionModel
            {

                Title = TitleTextBox.Text,
                Interviewee = Interviewee2TextBox.Text,
                Interviewer = interviewerName,

                InterviewDate = InterviewDate2DateDatePicker.SelectedDate != null ? ((DateTime)InterviewDate2DateDatePicker.SelectedDate).ToShortDateString() : string.Empty,
                InterviewDate1 = InterviewDate2DateDatePicker1.SelectedDate != null ? ((DateTime)InterviewDate2DateDatePicker1.SelectedDate).ToShortDateString() : string.Empty,
                InterviewDate2 = InterviewDate2DateDatePicker2.SelectedDate != null ? ((DateTime)InterviewDate2DateDatePicker2.SelectedDate).ToShortDateString() : string.Empty,

                //ConvertToDigitalDate = (DateTime)DateDigitalConverted2DateDatePicker.SelectedDate,
                ConvertToDigitalDate = DateDigitalConverted2DateDatePicker.SelectedDate != null ?
                DateDigitalConverted2DateDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                Subject = Subject2TextBox.Text,
                Keywords = Keywords2TextBox.Text,
                Description = Description2TextBox.Text,

                Rights = RightsTextBox.Text,
                ScopeAndContents = ScopeAndContentTextBox.Text,

                Format = Format2TextBox.Text,
                Language = LabguageTextBox.Text,

                Type = TypeTextBox.Text,
                Identifier = "",

                Publisher = PublisherTextBox.Text,
                Transcript = TranscriptTextBox.Text,

                RelationIsPartOf = RelationIsPartofTextBox.Text,
                FileName = FileNameTextBox.Text,

                CoverageSpatial = CoverageSpatialTextBox.Text,
                CoverageTemporal = CoverageTemporalTextBox.Text,

                CollectionId = short.Parse(((TranscriptionUiModel)DataContext).SelectedCollection),
                SubseriesId = int.Parse(((TranscriptionUiModel)DataContext).SelectedSeries),
            };

            UpdateTranscription(transcriptionModel, WellKnownTranscriptionModificationType.Metadata);


            PopulateFilterTextBox();

        }

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// Handles the Click event of the SupplementDetailsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SupplementDetailsButton_Click(object sender, EventArgs e)
        {
            bool isANewAudioEquipment = !string.IsNullOrEmpty(AudioEquipmentUsedFilteredComboBox.Text) &&
                   !BaseUserControl.AudioEquipments.Contains(AudioEquipmentUsedFilteredComboBox.Text);

            bool isANewVideoEquipment = !string.IsNullOrEmpty(VideoEquipmentUsedFilteredComboBox.Text) &&
                !BaseUserControl.VideoEquipments.Contains(VideoEquipmentUsedFilteredComboBox.Text);

            TranscriptionModel transcriptionModel = new TranscriptionModel
            {
                IsOnline = OnContentDmYesCheckBox.IsChecked.Value,
                IsRosetta = InRosettaYesCheckBox.IsChecked.Value,
                ReleaseForm = ReleaseFormYesCheckBox.IsChecked.Value,

                IsRestriction = RestrictionsYesCheckBox.IsChecked.Value,
                IsDarkArchive = DarkArchiveYesCheckBox.IsChecked.Value,

                RestrictionNote = RestrictionNoteTextBox.Text,
                DarkArchiveNote = DarkArchiveNoteTextBox.Text,

                IsAudioFormat = RecordingFormatAudioCheckBox.IsChecked.Value ? true : false,
                IsVideoFormat = RecordingFormatVideoCheckBox.IsChecked.Value ? true : false,

                AudioEquipmentUsed = AudioEquipmentUsedFilteredComboBox.Text,
                VideoEquipmentUsed = VideoEquipmentUsedFilteredComboBox.Text,

                EquipmentNumber = EquipmentNumberTextBox.Text,
                InterviewerDescription = InterviewerDescriptionTextBox.Text,
                InterviewerKeywords = InterviewerKeywordsTextBox.Text,
                InterviewerSubjects = InterviewerSubjectsTextBox.Text,

                Place = PlaceTextBox.Text,
                InterviewerNote = InterviewerNoteTextBox.Text,
                GeneralNote = GeneralNoteTextBox.Text,

                IsANewAudioEquipment = isANewAudioEquipment,
                IsANewVideoEquipment = isANewVideoEquipment,
            };

            UpdateTranscription(transcriptionModel, WellKnownTranscriptionModificationType.Supplement);

            if (isANewAudioEquipment || isANewVideoEquipment)
            {
                PopulateFilterTextBox();
            }
        }

        /// <summary>
        /// Handles the Check event of the TranscriptStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void TranscriptStatus_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                TranscriptStatusInProgressCheckBox,
                TranscriptStatusCompleteCheckBox);

        }

        /// <summary>
        /// Handles the Check event of the BornDigital control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BornDigital_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                BornDigitalYesCheckBox,
                BornDigitalNoCheckBox);
        }

        /// <summary>
        /// Handles the Check event of the Converted control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Converted_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                ConvertedYesCheckBox,
                ConvertedNoCheckBox);
        }

        /// <summary>
        /// Handles the Check event of the AccessMediaStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void AccessMediaStatus_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                AccessMediaStatusInProgressCheckBox,
                AccessMediaStatusCompleteCheckBox);
        }

        /// <summary>
        /// Called when [content dm check].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnContentDm_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                OnContentDmYesCheckBox,
                OnContentDmNoCheckBox);
        }

        /// <summary>
        /// Called when [sent out check].
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SentOut_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                SentOutYesCheckBox,
                SentOutNoCheckBox);
        }

        /// <summary>
        /// Called when [is priority check].
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void IsPriority_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                IsPriorityYesCheckBox,
                IsPriorityNoCheckBox);
        }

        /// <summary>
        /// Handles the Check event of the InRosetta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void InRosetta_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                InRosettaYesCheckBox,
                InRosettaNoCheckBox);
        }

        /// <summary>
        /// Handles the Check event of the ReleaseForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ReleaseForm_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                ReleaseFormYesCheckBox,
                ReleaseFormNoCheckBox);
        }

        /// <summary>
        /// Handles the Check event of the Restrictions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Restrictions_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                RestrictionsYesCheckBox,
                RestrictionsNoCheckBox);
        }

        /// <summary>
        /// Handles the Check event of the Dark Archive control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DarkArchive_Check(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;

            UIHelper.SetMutualExclusivity((CheckBox)sender,
                DarkArchiveYesCheckBox,
                DarkArchiveNoCheckBox);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Views the transcription.
        /// </summary>
        private void ViewTranscription()
        {
            RequestModel requestModel = new RequestModel()
            {
                TranscriptionId = TranscriptionId,
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetTranscription(requestModel);

            if (response.IsOperationSuccess)
            {
                TranscriptionModel transcriptionModel = response.Transcription;

                TitleLabel.Content = !string.IsNullOrEmpty(transcriptionModel.Title) && transcriptionModel.Title.Trim() != "" ? transcriptionModel.Title : "Title - n/a";

                // Tab 1 Details
                Title2Label.Text = transcriptionModel.Title;
                IntervieweeTextBox.Text = transcriptionModel.Interviewee;
                InterviewerTextBox.Text = transcriptionModel.Interviewer;
                ProjectCodeTextBox.Text = transcriptionModel.ProjectCode;
                ProjectCodeTextBox2.Text = transcriptionModel.ProjectCode;

                InterviewDateTextBox.Text = transcriptionModel.InterviewDate;
                InterviewDateTextBox1.Text = transcriptionModel.InterviewDate1;
                InterviewDateTextBox2.Text = transcriptionModel.InterviewDate2;

                if (transcriptionModel.IsAudioFormat && transcriptionModel.IsVideoFormat)
                {
                    FormatTextBox.Text = "Audio/Video";
                }
                else if (transcriptionModel.IsAudioFormat)
                {
                    FormatTextBox.Text = "Audio";
                }
                else if (transcriptionModel.IsVideoFormat)
                {
                    FormatTextBox.Text = "Video";
                }

                double completePercentage = GetObjectCompletePercentage(transcriptionModel);
                TranscriptionProgressProgressBar.Value = completePercentage;
                TranscriptionProgressLabel.Content = completePercentage + " %";

                CurrentStatusLabel.Content = transcriptionModel.TranscriptStatus ? "Complete" : "In Progress";
                TranscriptionStatusTextBox.Text = transcriptionModel.TranscriptStatus ? "Complete" : "In Progress";

                AccessMediaStatusTextBox.Text = transcriptionModel.IsAccessMediaStatus ? "Yes" : "No";

                OnContentDmTextBox.Text = transcriptionModel.IsOnline ? "Yes" : "No";

                InRosettaTextBox.Text = transcriptionModel.IsRosetta ? "Yes" : "No";

                ReleaseFormTextBox.Text = transcriptionModel.ReleaseForm ? "Yes" : "No";

                RestrictionsTextBox.Text = transcriptionModel.IsRestriction ? "Yes" : "No";

                CollectionNameTextBox.Text = transcriptionModel.CollectionName;
                SubseriesTextBox.Text = transcriptionModel.SubseriesName;

                SubjectTextBox.Text = transcriptionModel.Subject;
                KeywordsTextBox.Text = transcriptionModel.Keywords;
                DescriptionTextBox.Text = transcriptionModel.Description;

                // Tab 2 details

                TranscriberAssignedFilteredComboBox.Text = transcriptionModel.TranscriberAssigned == null ? "" : transcriptionModel.TranscriberAssigned;
                AuditCheckCompletedFilteredComboBox.Text = transcriptionModel.AuditCheckCompleted == null ? "" : transcriptionModel.AuditCheckCompleted;
                FirstEditCompletedFilteredComboBox.Text = transcriptionModel.FirstEditCompleted == null ? "" : transcriptionModel.FirstEditCompleted;
                SecondEditCompletedFilteredComboBox.Text = transcriptionModel.SecondEditCompleted == null ? "" : transcriptionModel.SecondEditCompleted;
                ThirdEditCompletedFilteredComboBox.Text = transcriptionModel.ThirdEditCompleted == null ? "" : transcriptionModel.ThirdEditCompleted;

                DraftSentDatePicker.Text = transcriptionModel.DraftSentDate != null ?
                    ((DateTime)transcriptionModel.DraftSentDate).ToShortDateString() : string.Empty;

                EditCorrectionCompletedFilteredComboBox.Text = transcriptionModel.EditWithCorrectionCompleted == null ? "" : transcriptionModel.EditWithCorrectionCompleted;
                FinalEditCompletedFilteredComboBox.Text = transcriptionModel.FinalEditCompleted == null ? "" : transcriptionModel.FinalEditCompleted;

                MetadataDraftTextBox.Text = transcriptionModel.MetadataDraft;

                IsPriorityYesCheckBox.IsChecked = transcriptionModel.IsPriority;
                IsPriorityNoCheckBox.IsChecked = !transcriptionModel.IsPriority;

                ReasonofPriorityTextBox.Text = transcriptionModel.ReasonForPriority;

                FinalSentDatePicker.Text = transcriptionModel.FinalSentDate != null ?
                    ((DateTime)transcriptionModel.FinalSentDate).ToShortDateString() : string.Empty;

                TranscriptStatusCompleteCheckBox.IsChecked = transcriptionModel.TranscriptStatus;
                TranscriptStatusInProgressCheckBox.IsChecked = !transcriptionModel.TranscriptStatus;

                SentOutYesCheckBox.IsChecked = transcriptionModel.SentOut;
                SentOutNoCheckBox.IsChecked = !transcriptionModel.SentOut;

                TranscriptionLocationTextBox.Text = transcriptionModel.TranscriptLocation;

                TranscriberNoteTextBox.Text = transcriptionModel.TranscriptNote;

                //// Tab 3 details

                MediaAudioCheckBox.IsChecked = transcriptionModel.IsAudioFormat;
                MediaVideoCheckBox.IsChecked = transcriptionModel.IsVideoFormat;

                BornDigitalYesCheckBox.IsChecked = transcriptionModel.IsBornDigital;
                BornDigitalNoCheckBox.IsChecked = !transcriptionModel.IsBornDigital;

                AudiocassetteCheckBox.IsChecked = (transcriptionModel.OriginalMediumType == (byte)OriginalMediumType.AudioCassette ? true : false);
                MiniDVCheckBox.IsChecked = (transcriptionModel.OriginalMediumType == (byte)OriginalMediumType.MiniDV ? true : false);
                OtherCheckBox.IsChecked = (transcriptionModel.OriginalMediumType == (byte)OriginalMediumType.Other ? true : false);
                OtherDigitalFormatTextBox.Text = transcriptionModel.OriginalMedium;

                ConvertedYesCheckBox.IsChecked = transcriptionModel.IsConvertToDigital;
                ConvertedNoCheckBox.IsChecked = !transcriptionModel.IsConvertToDigital;

                DateDigitalConvertedDateDatePicker.Text = transcriptionModel.ConvertToDigitalDate != null ?
                    ((DateTime)transcriptionModel.ConvertToDigitalDate).ToShortDateString() : string.Empty;

                AccessMediaStatusCompleteCheckBox.IsChecked = transcriptionModel.IsAccessMediaStatus;
                AccessMediaStatusInProgressCheckBox.IsChecked = !transcriptionModel.IsAccessMediaStatus;

                TechnicalSpecificationTextBox.Text = transcriptionModel.TechnicalSpecification;
                MasterFileLocationTextBox.Text = transcriptionModel.MasterFileLocation;
                AccessFileLocationTextBox.Text = transcriptionModel.AccessFileLocation;

                //// Tab 4 
                TitleTextBox.Text = transcriptionModel.Title;
                Interviewee2TextBox.Text = transcriptionModel.Interviewee;

                string[] interviews = transcriptionModel.Interviewer.Split(';');

                InterviewerFilteredComboBox.Text = interviews[0];

                InterviewerFilteredComboBox1.Text = interviews.Length > 1 ? interviews[1].Trim() : string.Empty;
                InterviewerFilteredComboBox2.Text = interviews.Length > 2 ? interviews[2].Trim() : string.Empty;

                InterviewDate2DateDatePicker.Text = transcriptionModel.InterviewDate != null ?
                    transcriptionModel.InterviewDate : string.Empty;

                InterviewDate2DateDatePicker1.Text = transcriptionModel.InterviewDate1 != null ?
                 transcriptionModel.InterviewDate1 : string.Empty;

                InterviewDate2DateDatePicker2.Text = transcriptionModel.InterviewDate2 != null ?
                 transcriptionModel.InterviewDate2 : string.Empty;

                DateDigitalConverted2DateDatePicker.Text = transcriptionModel.ConvertToDigitalDate != null ?
                    ((DateTime)transcriptionModel.ConvertToDigitalDate).ToShortDateString() : string.Empty;

                Subject2TextBox.Text = transcriptionModel.Subject;
                Keywords2TextBox.Text = transcriptionModel.Keywords;
                Description2TextBox.Text = transcriptionModel.Description;

                ScopeAndContentTextBox.Text = transcriptionModel.ScopeAndContents;
                Format2TextBox.Text = transcriptionModel.Format;
                TypeTextBox.Text = transcriptionModel.Type;
                PublisherTextBox.Text = transcriptionModel.Publisher;
                //textBoxCollectionName.Text = transcriptionModel.CollectionName;
                RelationIsPartofTextBox.Text = transcriptionModel.RelationIsPartOf;
                CoverageSpatialTextBox.Text = transcriptionModel.CoverageSpatial;
                CoverageTemporalTextBox.Text = transcriptionModel.CoverageTemporal;
                RightsTextBox.Text = transcriptionModel.Rights;
                LabguageTextBox.Text = transcriptionModel.Language;

                TranscriptTextBox.Text = transcriptionModel.Transcript;

                FileNameTextBox.Text = transcriptionModel.FileName;

                ((TranscriptionUiModel)DataContext).SelectedCollection = transcriptionModel.CollectionId.ToString();
                ((TranscriptionUiModel)DataContext).SelectedSeries = transcriptionModel.SubseriesId.ToString();

                //// Tab 5 

                OnContentDmYesCheckBox.IsChecked = transcriptionModel.IsOnline;
                OnContentDmNoCheckBox.IsChecked = !transcriptionModel.IsOnline;

                InRosettaYesCheckBox.IsChecked = transcriptionModel.IsRosetta;
                InRosettaNoCheckBox.IsChecked = !transcriptionModel.IsRosetta;

                ReleaseFormYesCheckBox.IsChecked = transcriptionModel.ReleaseForm;
                ReleaseFormNoCheckBox.IsChecked = !transcriptionModel.ReleaseForm;

                RestrictionsYesCheckBox.IsChecked = transcriptionModel.IsRestriction;
                RestrictionsNoCheckBox.IsChecked = !transcriptionModel.IsRestriction;

                RestrictionNoteTextBox.Text = transcriptionModel.RestrictionNote;

                DarkArchiveYesCheckBox.IsChecked = transcriptionModel.IsDarkArchive;
                DarkArchiveNoCheckBox.IsChecked = !transcriptionModel.IsDarkArchive;

                DarkArchiveNoteTextBox.Text = transcriptionModel.DarkArchiveNote;

                RecordingFormatAudioCheckBox.IsChecked = transcriptionModel.IsAudioFormat;
                RecordingFormatVideoCheckBox.IsChecked = transcriptionModel.IsVideoFormat;

                AudioEquipmentUsedFilteredComboBox.Text = transcriptionModel.AudioEquipmentUsed;
                VideoEquipmentUsedFilteredComboBox.Text = transcriptionModel.VideoEquipmentUsed;

                EquipmentNumberTextBox.Text = transcriptionModel.EquipmentNumber;
                InterviewerDescriptionTextBox.Text = transcriptionModel.InterviewerDescription;
                InterviewerKeywordsTextBox.Text = transcriptionModel.InterviewerKeywords;
                InterviewerSubjectsTextBox.Text = transcriptionModel.InterviewerSubjects;

                PlaceTextBox.Text = transcriptionModel.Place;
                InterviewerNoteTextBox.Text = transcriptionModel.InterviewerNote;
                GeneralNoteTextBox.Text = transcriptionModel.GeneralNote;
            }
        }

        /// <summary>
        /// Updates the transcription.
        /// </summary>
        /// <param name="transcriptionModel">The transcription model.</param>
        /// <param name="modificationType">Type of the modification.</param>
        private void UpdateTranscription(TranscriptionModel transcriptionModel, WellKnownTranscriptionModificationType modificationType)
        {
            transcriptionModel.Id = TranscriptionId;
            transcriptionModel.UpdatedBy = App.BaseUserControl.UserModel.UserId;
            transcriptionModel.UpdatedDate = DateTime.Now;

            RequestModel requestModel = new RequestModel()
            {
                TranscriptionModel = transcriptionModel,
                WellKnownTranscriptionModificationType = modificationType,
                WellKnownModificationType = WellKnownModificationType.Edit,
            };

            ResponseModel response = App.BaseUserControl.InternalService.ModifyTranscription(requestModel);

            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, string.Empty);
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

        }

        /// <summary>
        /// Gets the object complete percentage.
        /// </summary>
        /// <param name="transcriptionModel">The transcription model.</param>
        /// <returns></returns>
        private double GetObjectCompletePercentage(TranscriptionModel transcriptionModel)
        {
            double totalProperties = 0, filledProperties = 0;

            foreach (PropertyInfo prop in transcriptionModel.GetType().GetProperties())
            {
                totalProperties++;

                object obj = prop.GetValue(transcriptionModel, null);

                if (obj != null)
                {
                    filledProperties++;
                }
            }

            double percentage = filledProperties / totalProperties * 100;

            return Math.Round(percentage, 0);
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
                    DarkArchieveAndButtonVisibility(Visibility.Collapsed,
                        Visibility.Collapsed);

                    DeleteButtonVisibility(Visibility.Collapsed);
                    break;
                case WellKnownUserType.Staff:
                    DarkArchieveAndButtonVisibility(Visibility.Collapsed,
                        Visibility.Visible);

                    DeleteButtonVisibility(Visibility.Collapsed);

                    break;
                case WellKnownUserType.AdminUser:

                    DarkArchieveAndButtonVisibility(Visibility.Visible,
                        Visibility.Visible);

                    DeleteButtonVisibility(Visibility.Visible);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Restrictionses the and button visibility.
        /// </summary>
        /// <param name="isButtonDisplay">if set to <c>true</c> [is button display].</param>
        /// <param name="isDarkArcieveDisplay">if set to <c>true</c> [is restrictions display].</param>
        private void DarkArchieveAndButtonVisibility(Visibility isDarkArcieveDisplay, Visibility isButtonDisplay)
        {
            DarkArchiveLabel.Visibility = isDarkArcieveDisplay;
            DarkArchiveStackPanel.Visibility = isDarkArcieveDisplay;
            DarkArchiveBorder1.Visibility = isDarkArcieveDisplay;
            DarkArchiveBorder2.Visibility = isDarkArcieveDisplay;
            DarkArchiveNoteLabel.Visibility = isDarkArcieveDisplay;
            DarkArchiveNoteTextBox.Visibility = isDarkArcieveDisplay;

            TranscriptionDetailsButton.Visibility = isButtonDisplay;
            MediaDetailsButto.Visibility = isButtonDisplay;
            MetadataButton.Visibility = isButtonDisplay;
            SupplementDetailsButton.Visibility = isButtonDisplay;
        }

        /// <summary>
        /// Deletes the button visibility.
        /// </summary>
        /// <param name="isDarkArcieveDisplay">The is dark arcieve display.</param>
        private void DeleteButtonVisibility(Visibility isDarkArcieveDisplay)
        {
            DeleteButtonStackPanel.Visibility = isDarkArcieveDisplay;
        }

        /// <summary>
        /// Populates the intialize view.
        /// </summary>
        private void PopulateIntializeView()
        {
            // interviews
            IEnumerable interviews = ListHelper.GetPredefinedUser(WellKnownPredefinedUserType.Interviewer);

            PopulateFilterListBox(InterviewerFilteredComboBox, interviews);
            PopulateFilterListBox(InterviewerFilteredComboBox1, interviews);
            PopulateFilterListBox(InterviewerFilteredComboBox2, interviews);

            // Student list filter list box 

            IEnumerable students = ListHelper.GetPredefinedUser(WellKnownPredefinedUserType.Student);

            PopulateFilterListBox(TranscriberAssignedFilteredComboBox, students);
            PopulateFilterListBox(AuditCheckCompletedFilteredComboBox, students);
            PopulateFilterListBox(FirstEditCompletedFilteredComboBox, students);
            PopulateFilterListBox(SecondEditCompletedFilteredComboBox, students);
            PopulateFilterListBox(ThirdEditCompletedFilteredComboBox, students);
            PopulateFilterListBox(EditCorrectionCompletedFilteredComboBox, students);
            PopulateFilterListBox(FinalEditCompletedFilteredComboBox, students);

            // Audio and Video list
            PopulateFilterListBox(AudioEquipmentUsedFilteredComboBox, BaseUserControl.AudioEquipments);
            PopulateFilterListBox(VideoEquipmentUsedFilteredComboBox, BaseUserControl.VideoEquipments);
        }

        /// <summary>
        /// Populates the filter ListBox.
        /// </summary>
        /// <param name="filteredComboBox">The filtered ComboBox.</param>
        private void PopulateFilterListBox(FilteredComboBox filteredComboBox, IEnumerable collection)
        {
            filteredComboBox.IsEditable = true;
            filteredComboBox.IsTextSearchEnabled = false;
            filteredComboBox.ItemsSource = collection;
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
