using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessServices.Services;
using EntityData;
using Model;
using Model.Transfer;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    internal class ModifyTranscriptionUow : UnitOfWork
    {

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
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
        private class WellKnownErrors : WellKnownServiceErrors
        {
        }

        /// <summary>
        /// Runs prior to the work being done.
        /// </summary>
        protected override void PreExecute()
        {
            this.WellKnownError = new WellKnownErrors();

            this.TranscriptionRepository = new TranscriptionRepository();

            this.WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            if (this.Request.WellKnownModificationType == Core.Enums.WellKnownModificationType.Add)
            {
                transcription transcription = Util.ConvertToTranscription(this.Request.TranscriptionModel);

                this.TranscriptionRepository.Add(transcription);
                this.TranscriptionRepository.Save();
            }
            else
            {
                transcription daTranscription = null;

                daTranscription = this.TranscriptionRepository.GetTranscription(this.Request.TranscriptionModel.Id);

                TranscriptionModel transcriptionModel = this.Request.TranscriptionModel;

                switch (this.Request.WellKnownTranscriptionModificationType)
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

                        daTranscription.EditWithCorrectionCompleted = transcriptionModel.EditWithCorrectionCompleted;
                        daTranscription.EditWithCorrectionDate = transcriptionModel.EditWithCorrectionDate;

                        daTranscription.FirstEditCompleted = transcriptionModel.FirstEditCompleted;
                        daTranscription.FirstEditCompletedDate = transcriptionModel.FirstEditCompletedDate;

                        daTranscription.FinalSentDate = transcriptionModel.FinalSentDate;

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

                        daTranscription.LegalNote = transcriptionModel.LegalNote;
                        daTranscription.IsAudioFormat = transcriptionModel.IsAudioFormat;
                        //daTranscription.IsVideoFormat = transcriptionModel.IsVideoFormat;
                        daTranscription.EquipmentUsed = transcriptionModel.EquipmentUsed;
                        daTranscription.Place = transcriptionModel.Place;
                        daTranscription.InterviewerNote = transcriptionModel.InterviewerNote;
                        break;
                    default:
                        break;
                }

                daTranscription.UpdatedBy = transcriptionModel.UpdatedBy;
                daTranscription.UpdatedDate = transcriptionModel.UpdatedDate;

                this.TranscriptionRepository.Edit(daTranscription);
                this.TranscriptionRepository.Save();
            }


            this.Response = new ResponseModel()
            {
                IsOperationSuccess = true
            };

        }

        /// <summary>
        /// Runs after the work has been done.
        /// </summary>
        protected override void PostExecute()
        {
            int errorCode = this.WellKnownError.Value.Item1;

            if (errorCode > 0)
            {
                this.Response = new ResponseModel()
                {
                    ErrorCode = errorCode.ToString(),

                    ErrorMessage = this.WellKnownError.Value.Item2,

                    IsOperationSuccess = false

                };

            }
        }
    }
}
