﻿using Core.Enums;
using Model;
using Model.Transfer;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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

            TranscriptionId = transcriptionId;

            if (TranscriptionId > 0)
            {
                ViewTranscription();

                switch (expander)
                {
                    case WellKnownExpander.General:
                        Expander_Expanded(GeneralExpander, null);
                        break;
                    case WellKnownExpander.Transcript:
                        Expander_Expanded(TranscriptExpander, null);
                        break;
                    case WellKnownExpander.Media:
                        Expander_Expanded(MediaExpander, null);
                        break;
                    case WellKnownExpander.Metadata:
                        Expander_Expanded(MetadataExpander, null);
                        break;
                    case WellKnownExpander.Supplimental:
                        Expander_Expanded(SupplementalExpander, null);
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
                TranscriberAssigned = TranscriberAssignedTextBox.Text,
                TranscriberCompleted = TranscriptCompletedDateDatePicker.SelectedDate != null ?
                TranscriptCompletedDateDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                AuditCheckCompleted = AuditCheckCompletedTextBox.Text,
                AuditCheckCompletedDate = AuditCheckCompletedTDateDatePicker.SelectedDate != null ?
                AuditCheckCompletedTDateDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                FirstEditCompleted = FirstEditCompletedTextBox.Text,
                FirstEditCompletedDate = FirstEditCompletedTDateDatePicker.SelectedDate != null ?
                FirstEditCompletedTDateDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                SecondEditCompleted = SecondEditCompletedTextBox.Text,
                SecondEditCompletedDate = SecondEditCompletedTDateDatePicker.SelectedDate != null ?
                SecondEditCompletedTDateDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                DraftSentDate = DraftSentDatePicker.SelectedDate != null ?
                DraftSentDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                EditWithCorrectionCompleted = EditCorrectionCompletedTextBox.Text,
                EditWithCorrectionDate = EditCorrectionCompletedTDateDatePicker.SelectedDate != null ?
                EditCorrectionCompletedTDateDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                TranscriptStatus = TranscriptStatusCompleteCheckBox.IsChecked.Value,// TranscriptStatusToggleButton.IsChecked.Value,

                SentOut = SentOutYesCheckBox.IsChecked.Value,

                MetadataDraft = MetadataDraftTextBox.Text,

                FinalSentDate = FinalSentDatePicker.SelectedDate != null ?
                FinalSentDatePicker.SelectedDate.Value.Date
                : (DateTime?)null,

                TranscriberLocation = TranscriberLocationTextBox.Text,
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
            bool isANewInterviewer = !BaseUserControl.Interviewers.Contains(InterviewerFilteredComboBox.Text);

            TranscriptionModel transcriptionModel = new TranscriptionModel
            {

                Title = TitleTextBox.Text,
                Interviewee = Interviewee2TextBox.Text,
                Interviewer = InterviewerFilteredComboBox.Text,

                InterviewDate = (DateTime)InterviewDate2DateDatePicker.SelectedDate,
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
                Identifier = IdentifierTextBox.Text,

                Publisher = PublisherTextBox.Text,
                Transcript = TranscriptTextBox.Text,

                RelationIsPartOf = RelationIsPartofTextBox.Text,
                FileName = FileNameTextBox.Text,

                CoverageSpatial = CoverageSpatialTextBox.Text,
                CoverageTemporal = CoverageTemporalTextBox.Text,

                 IsANewInterviewer = isANewInterviewer,
            };

            UpdateTranscription(transcriptionModel, WellKnownTranscriptionModificationType.Metadata);

            if (isANewInterviewer)
            {
                PopulateFilterTextBox();
            }
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
                IsInContentDm = OnContentDmYesCheckBox.IsChecked.Value,
                IsRosetta = InRosettaYesCheckBox.IsChecked.Value,
                ReleaseForm = ReleaseFormYesCheckBox.IsChecked.Value,
                IsRestriction = RestrictionsYesCheckBox.IsChecked.Value,

                LegalNote = RestrictionNoteTextBox.Text,

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
        /// Handles the Expanded event of the Expander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expander = (Expander)sender;

            if (expander.Name == "GeneralExpander")
            {
                ExpanderVisibility(true, false, false, false, false);
            }
            else if (expander.Name == "TranscriptExpander")
            {
                ExpanderVisibility(false, true, false, false, false);
            }
            else if (expander.Name == "MetadataExpander")
            {
                ExpanderVisibility(false, false, true, false, false);
            }
            else if (expander.Name == "MediaExpander")
            {
                ExpanderVisibility(false, false, false, true, false);
            }
            else if (expander.Name == "SupplementalExpander")
            {
                ExpanderVisibility(false, false, false, false, true);
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

        #endregion

        #region Private methods

        /// <summary>
        /// Expanders the visibility.
        /// </summary>
        /// <param name="general">if set to <c>true</c> [general].</param>
        /// <param name="transcript">if set to <c>true</c> [transcript].</param>
        /// <param name="media">if set to <c>true</c> [media].</param>
        /// <param name="metadata">if set to <c>true</c> [metadata].</param>
        /// <param name="supplimental">if set to <c>true</c> [supplimental].</param>
        private void ExpanderVisibility(bool general, bool transcript, bool media, bool metadata, bool supplimental)
        {
            GeneralExpander.IsExpanded = general;
            TranscriptExpander.IsExpanded = transcript;
            MetadataExpander.IsExpanded = media;
            MediaExpander.IsExpanded = metadata;
            SupplementalExpander.IsExpanded = supplimental;
        }

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

                TitleLabel.Content = transcriptionModel.Title;

                // Tab 1 Details
                Title2Label.Text = transcriptionModel.Title;
                IntervieweeTextBox.Text = transcriptionModel.Interviewee;
                InterviewerTextBox.Text = transcriptionModel.Interviewer;
                ProjectCodeTextBox.Text = transcriptionModel.ProjectCode;
                InterviewDateTextBox.Text = transcriptionModel.InterviewDate.ToShortDateString();

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

                OnContentDmTextBox.Text = transcriptionModel.IsInContentDm ? "Yes" : "No";

                InRosettaTextBox.Text = transcriptionModel.IsRosetta ? "Yes" : "No";

                ReleaseFormTextBox.Text = transcriptionModel.ReleaseForm ? "Yes" : "No";

                RestrictionsTextBox.Text = transcriptionModel.IsRestriction ? "Yes" : "No";

                CollectionNameTextBox.Text = transcriptionModel.CollectionName;
                SubseriesTextBox.Text = transcriptionModel.SubseriesName;

                SubjectTextBox.Text = transcriptionModel.Subject;
                KeywordsTextBox.Text = transcriptionModel.Keywords;
                DescriptionTextBox.Text = transcriptionModel.Description;

                // Tab 2 details

                TranscriberAssignedTextBox.Text = transcriptionModel.TranscriberAssigned;
                TranscriptCompletedDateDatePicker.Text = transcriptionModel.TranscriberCompleted != null ?
                ((DateTime)transcriptionModel.TranscriberCompleted).ToShortDateString() : string.Empty;

                AuditCheckCompletedTextBox.Text = transcriptionModel.AuditCheckCompleted;
                AuditCheckCompletedTDateDatePicker.Text = transcriptionModel.AuditCheckCompletedDate != null ?
                    ((DateTime)transcriptionModel.AuditCheckCompletedDate).ToShortDateString() : string.Empty;

                FirstEditCompletedTextBox.Text = transcriptionModel.FirstEditCompleted;
                FirstEditCompletedTDateDatePicker.Text = transcriptionModel.FirstEditCompletedDate != null ?
                    ((DateTime)transcriptionModel.FirstEditCompletedDate).ToShortDateString() : string.Empty;

                SecondEditCompletedTextBox.Text = transcriptionModel.SecondEditCompleted;
                SecondEditCompletedTDateDatePicker.Text = transcriptionModel.SecondEditCompletedDate != null ?
                   ((DateTime)transcriptionModel.SecondEditCompletedDate).ToShortDateString() : string.Empty;

                DraftSentDatePicker.Text = transcriptionModel.DraftSentDate != null ?
                    ((DateTime)transcriptionModel.DraftSentDate).ToShortDateString() : string.Empty;

                EditCorrectionCompletedTextBox.Text = transcriptionModel.EditWithCorrectionCompleted;
                EditCorrectionCompletedTDateDatePicker.Text = transcriptionModel.EditWithCorrectionDate != null ?
                    ((DateTime)transcriptionModel.EditWithCorrectionDate).ToShortDateString() : string.Empty;

                FinalEditCompletedTextBox.Text = transcriptionModel.FirstEditCompleted;
                FinalEditCompletedTDateDatePicker.Text = transcriptionModel.FirstEditCompletedDate != null ?
                    ((DateTime)transcriptionModel.FirstEditCompletedDate).ToShortDateString() : string.Empty;

                MetadataDraftTextBox.Text = transcriptionModel.MetadataDraft;

                FinalSentDatePicker.Text = transcriptionModel.FinalSentDate != null ?
                    ((DateTime)transcriptionModel.FinalSentDate).ToShortDateString() : string.Empty;

                TranscriptStatusCompleteCheckBox.IsChecked = transcriptionModel.TranscriptStatus;
                TranscriptStatusInProgressCheckBox.IsChecked = !transcriptionModel.TranscriptStatus;

                SentOutYesCheckBox.IsChecked = transcriptionModel.SentOut;
                SentOutNoCheckBox.IsChecked = !transcriptionModel.SentOut;

                TranscriberLocationTextBox.Text = transcriptionModel.TranscriberLocation;

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

                MasterFileLocationTextBox.Text = transcriptionModel.MasterFileLocation;
                AccessFileLocationTextBox.Text = transcriptionModel.AccessFileLocation;

                //// Tab 4 
                TitleTextBox.Text = transcriptionModel.Title;
                Interviewee2TextBox.Text = transcriptionModel.Interviewee;
                InterviewerFilteredComboBox.Text = transcriptionModel.Interviewer;

                InterviewDate2DateDatePicker.Text = transcriptionModel.InterviewDate != null ?
                    transcriptionModel.InterviewDate.ToShortDateString() : string.Empty;

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
                RelationIsPartofTextBox.Text = transcriptionModel.CoverageSpatial;
                CoverageTemporalTextBox.Text = transcriptionModel.CoverageTemporal;
                RightsTextBox.Text = transcriptionModel.Rights;
                LabguageTextBox.Text = transcriptionModel.Language;
                LabguageTextBox.Text = transcriptionModel.Identifier;
                LabguageTextBox.Text = transcriptionModel.Transcript;
                FileNameTextBox.Text = transcriptionModel.FileName;

                //// Tab 5 

                OnContentDmYesCheckBox.IsChecked = transcriptionModel.IsInContentDm;
                OnContentDmNoCheckBox.IsChecked = !transcriptionModel.IsInContentDm;

                InRosettaYesCheckBox.IsChecked = transcriptionModel.IsRosetta;
                InRosettaNoCheckBox.IsChecked = !transcriptionModel.IsRosetta;

                ReleaseFormYesCheckBox.IsChecked = transcriptionModel.ReleaseForm;
                ReleaseFormNoCheckBox.IsChecked = !transcriptionModel.ReleaseForm;

                RestrictionsYesCheckBox.IsChecked = transcriptionModel.IsRestriction;
                RestrictionsNoCheckBox.IsChecked = !transcriptionModel.IsRestriction;

                RestrictionNoteTextBox.Text = transcriptionModel.LegalNote;

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
                case WellKnownUserType.Staff:
                    RestrictionsLabel.Visibility = Visibility.Collapsed;
                    RestrictionsStackPanel.Visibility = Visibility.Collapsed;
                    RestrictionsBorder2.Visibility = Visibility.Collapsed;
                    RestrictionNoteTextBox.Visibility = Visibility.Collapsed;
                    RestrictionNoteLabel.Visibility = Visibility.Collapsed;
                    break;
                case WellKnownUserType.AdminUser:
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
            App.BaseUserControl.InitializeComponent();

            PopulateIntializeView();
        }

        #endregion

    }
}
