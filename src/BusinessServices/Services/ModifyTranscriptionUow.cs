﻿using BusinessServices.Services;
using EntityData;
using Model;
using Model.Transfer;
using Repository.Implementations;
using System;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to ModifyTranscriptionUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class ModifyTranscriptionUow : UnitOfWork
    {

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public RequestModel Request
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public ResponseModel Response
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the collection repository.
        /// </summary>
        /// <value>
        /// The collection repository.
        /// </value>
        private TranscriptionRepository TranscriptionRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the interviewer repository.
        /// </summary>
        /// <value>
        /// The interviewer repository.
        /// </value>
        public InterviewerRepository InterviewerRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the audio equipment used repository.
        /// </summary>
        /// <value>
        /// The audio equipment used repository.
        /// </value>
        public AudioEquipmentUsedRepository AudioEquipmentUsedRepository
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the video equipment used repository.
        /// </summary>
        /// <value>
        /// The video equipment used repository.
        /// </value>
        public VideoEquipmentUsedRepository VideoEquipmentUsedRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the well known error.
        /// </summary>
        /// <value>
        /// The well known error.
        /// </value>
        private WellKnownErrors WellKnownError
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyTranscriptionUow" /> class.
        /// </summary>
        public ModifyTranscriptionUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private ModifyTranscriptionUow(bool isReadOnly)
            : base(isReadOnly)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="BusinessServices.WellKnownServiceErrors" />
        private class WellKnownErrors : WellKnownServiceErrors
        {
            /// <summary>
            /// The wrong user identifier supplied
            /// </summary>
            public static readonly Tuple<int, string> NotUniqueProjectCode =
                new Tuple<int, string>(1, "Project code is not unique");
        }

        /// <summary>
        /// Runs prior to the work being done.
        /// </summary>
        protected override void PreExecute()
        {
            WellKnownError = new WellKnownErrors();

            TranscriptionRepository = new TranscriptionRepository();
            InterviewerRepository = new InterviewerRepository();
            AudioEquipmentUsedRepository = new AudioEquipmentUsedRepository();
            VideoEquipmentUsedRepository = new VideoEquipmentUsedRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            transcription daTranscription = TranscriptionRepository.GetTranscription(Request.TranscriptionModel.ProjectCode);

            if (daTranscription != null)
            {
                WellKnownError.Value = WellKnownErrors.NotUniqueProjectCode;
            }
            else
            {
                if (Request.TranscriptionModel.IsANewInterviewer)
                {
                    InterviewerRepository.Add(new interviewer() { InterviewerName = Request.TranscriptionModel.Interviewer });
                    InterviewerRepository.Save();
                }

                if (Request.TranscriptionModel.IsANewAudioEquipment)
                {
                    AudioEquipmentUsedRepository.Add(
                        new audioequipmentused()
                        {
                            AudioEquipmentUsedName = Request.TranscriptionModel.AudioEquipmentUsed
                        });

                    AudioEquipmentUsedRepository.Save();
                }

                if (Request.TranscriptionModel.IsANewVideoEquipment)
                {
                    VideoEquipmentUsedRepository.Add(new videoequipmentused()
                    {
                        VideoEquipmentUsedName = Request.TranscriptionModel.VideoEquipmentUsed
                    });

                    VideoEquipmentUsedRepository.Save();
                }

                if (Request.WellKnownModificationType == Core.Enums.WellKnownModificationType.Add)
                {

                    transcription transcription = Util.ConvertToTranscription(Request.TranscriptionModel);

                    TranscriptionRepository.Add(transcription);
                    TranscriptionRepository.Save();
                }
                else
                {
                    daTranscription = null;

                    daTranscription = TranscriptionRepository.GetTranscription(Request.TranscriptionModel.Id);

                    TranscriptionModel transcriptionModel = Request.TranscriptionModel;

                    switch (Request.WellKnownTranscriptionModificationType)
                    {

                        case Core.Enums.WellKnownTranscriptionModificationType.Transcript:

                            daTranscription.TranscriberAssigned = transcriptionModel.TranscriberAssigned;
                            daTranscription.TranscriberCompleted = transcriptionModel.TranscriberCompleted;

                            daTranscription.AuditCheckCompleted = transcriptionModel.AuditCheckCompleted;
                            daTranscription.AuditCheckCompletedDate = transcriptionModel.AuditCheckCompletedDate;

                            daTranscription.FirstEditCompleted = transcriptionModel.FirstEditCompleted;
                            daTranscription.FirstEditCompletedDate = transcriptionModel.FirstEditCompletedDate;

                            daTranscription.SecondEditCompleted = transcriptionModel.SecondEditCompleted;
                            daTranscription.SecondEditCompletedDate = transcriptionModel.SecondEditCompletedDate;

                            daTranscription.DraftSentDate = transcriptionModel.DraftSentDate;
                            daTranscription.SentOut = transcriptionModel.SentOut;

                            daTranscription.EditWithCorrectionCompleted = transcriptionModel.EditWithCorrectionCompleted;
                            daTranscription.EditWithCorrectionDate = transcriptionModel.EditWithCorrectionDate;

                            daTranscription.FirstEditCompleted = transcriptionModel.FirstEditCompleted;
                            daTranscription.FirstEditCompletedDate = transcriptionModel.FirstEditCompletedDate;

                            daTranscription.FinalSentDate = transcriptionModel.FinalSentDate;
                            daTranscription.MetadataDraft = transcriptionModel.MetadataDraft;

                            daTranscription.TranscriptStatus = transcriptionModel.TranscriptStatus;

                            daTranscription.TranscriberLocation = transcriptionModel.TranscriberLocation;
                            daTranscription.TranscriptNote = transcriptionModel.TranscriptNote;

                            break;
                        case Core.Enums.WellKnownTranscriptionModificationType.Media:

                            daTranscription.IsAudioFormat = transcriptionModel.IsAudioFormat;
                            //daTranscription.IsVideoFormat = transcriptionModel.IsVideoFormat;

                            daTranscription.IsBornDigital = transcriptionModel.IsBornDigital;
                            daTranscription.OriginalMediumType = transcriptionModel.OriginalMediumType;
                            daTranscription.OriginalMedium = transcriptionModel.OriginalMedium;
                            daTranscription.IsConvertToDigital = transcriptionModel.IsConvertToDigital;
                            daTranscription.ConvertToDigitalDate = transcriptionModel.ConvertToDigitalDate;
                            daTranscription.IsAccessMediaStatus = transcriptionModel.IsAccessMediaStatus;
                            daTranscription.MasterFileLocation = transcriptionModel.MasterFileLocation;
                            daTranscription.AccessFileLocation = transcriptionModel.AccessFileLocation;

                            break;
                        case Core.Enums.WellKnownTranscriptionModificationType.Metadata:


                            daTranscription.Title = transcriptionModel.Title;
                            daTranscription.Interviewee = transcriptionModel.Interviewee;
                            daTranscription.Interviewer = transcriptionModel.Interviewer;

                            daTranscription.InterviewDate = transcriptionModel.InterviewDate;
                            daTranscription.ConvertToDigitalDate = transcriptionModel.ConvertToDigitalDate;

                            daTranscription.Subject = transcriptionModel.Subject;
                            daTranscription.Keywords = transcriptionModel.Keywords;
                            daTranscription.Description = transcriptionModel.Description;
                            daTranscription.ScopeAndContents = transcriptionModel.ScopeAndContents;
                            daTranscription.Format = transcriptionModel.Format;
                            daTranscription.Type = transcriptionModel.Type;
                            daTranscription.Publisher = transcriptionModel.Publisher;
                            daTranscription.RelationIsPartOf = transcriptionModel.RelationIsPartOf;
                            daTranscription.CoverageSpatial = transcriptionModel.CoverageSpatial;
                            daTranscription.CoverageTemporal = transcriptionModel.CoverageTemporal;
                            daTranscription.Rights = transcriptionModel.Rights;
                            daTranscription.Language = transcriptionModel.Language;
                            daTranscription.Identifier = transcriptionModel.Identifier;
                            daTranscription.Transcript = transcriptionModel.Transcript;
                            daTranscription.FileName = transcriptionModel.FileName;


                            break;
                        case Core.Enums.WellKnownTranscriptionModificationType.Supplement:

                            daTranscription.IsInContentDm = transcriptionModel.IsInContentDm;
                            daTranscription.IsRosetta = transcriptionModel.IsRosetta;
                            daTranscription.ReleaseForm = transcriptionModel.ReleaseForm;
                            daTranscription.IsRestriction = transcriptionModel.IsRestriction;

                            daTranscription.MetadataDraft = transcriptionModel.MetadataDraft;
                            daTranscription.TranscriptStatus = transcriptionModel.TranscriptStatus;

                            daTranscription.LegalNote = transcriptionModel.LegalNote;
                            daTranscription.IsAudioFormat = transcriptionModel.IsAudioFormat;
                            daTranscription.IsVideoFormat = transcriptionModel.IsVideoFormat;
                            daTranscription.AudioEquipmentUsed = transcriptionModel.AudioEquipmentUsed;
                            daTranscription.VideoEquipmentUsed = transcriptionModel.VideoEquipmentUsed;
                            daTranscription.EquipmentNumber = transcriptionModel.EquipmentNumber;

                            daTranscription.Place = transcriptionModel.Place;

                            daTranscription.InterviewerDescription = transcriptionModel.InterviewerDescription;
                            daTranscription.InterviewerKeywords = transcriptionModel.InterviewerKeywords;
                            daTranscription.InterviewerSubjects = transcriptionModel.InterviewerSubjects;
                            daTranscription.InterviewerNote = transcriptionModel.InterviewerNote;
                            break;
                        default:
                            break;
                    }

                    daTranscription.UpdatedBy = transcriptionModel.UpdatedBy;
                    daTranscription.UpdatedDate = transcriptionModel.UpdatedDate;

                    TranscriptionRepository.Edit(daTranscription);
                    TranscriptionRepository.Save();
                }
            }

            Response = new ResponseModel()
            {
                IsOperationSuccess = true,
            };

        }

        /// <summary>
        /// Runs after the work has been done.
        /// </summary>
        protected override void PostExecute()
        {
            int errorCode = WellKnownError.Value.Item1;

            if (errorCode > 0)
            {
                Response = new ResponseModel()
                {
                    ErrorCode = errorCode.ToString(),

                    ErrorMessage = WellKnownError.Value.Item2,

                    IsOperationSuccess = false

                };

            }
        }
    }
}
