﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessServices.Services;
using EntityData;
using Model;
using Model.Transfer;
using Repository;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    internal class GetTranscriptionsForBrowseUow : UnitOfWork
    {

        /// <summary>
        /// gets and sets the request model
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
        /// Gets or sets the TranscriptionRepository repository.
        /// </summary>
        /// <value>
        /// The TranscriptionRepository repository.
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
        /// Initializes a new instance of the <see cref="GetTranscriptionsForBrowseUow" /> class.
        /// </summary>
        public GetTranscriptionsForBrowseUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private GetTranscriptionsForBrowseUow(bool isReadOnly)
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
            List<TranscriptionModel> newlist = new List<TranscriptionModel>();

            var predicate = PredicateBuilder.False<transcription>();

            foreach (string item in this.Request.TranscriptionSearchModel.CollectionNames)
            {
                predicate = predicate.Or(p => p.CollectionId == short.Parse(item));
            }

            foreach (string item in this.Request.TranscriptionSearchModel.Subjects)
            {
                predicate = predicate.Or(p => p.Subject.Contains(item));
            }

            foreach (string item in this.Request.TranscriptionSearchModel.Interviewers)
            {
                predicate = predicate.Or(p => p.Interviewer.Equals(item));
            }

            foreach (string item in this.Request.TranscriptionSearchModel.Contentdms)
            {
                predicate = predicate.Or(p => p.IsInContentDm == bool.Parse(item));
            }

            if (!string.IsNullOrEmpty(this.Request.SearchWord))
            {
                predicate = predicate.And(p =>
                                                p.Interviewer.Contains(Request.SearchWord) ||
                                                p.AuditCheckCompleted.Contains(this.Request.SearchWord) ||
                                                p.AccessFileLocation.Contains(this.Request.SearchWord) ||
                                                p.CoverageSpatial.Contains(this.Request.SearchWord) ||
                                                p.CoverageTemporal.Contains(this.Request.SearchWord) ||
                                                p.Description.Contains(this.Request.SearchWord) ||
                                                p.EditWithCorrectionCompleted.Contains(this.Request.SearchWord) ||
                                                //p.EquipmentUsed.Contains(this.Request.SearchWord) ||
                                                //p.FileName.Contains(this.Request.SearchWord) ||
                                                //p.FinalEditCompleted.Contains(this.Request.SearchWord) ||
                                                //p.FirstEditCompleted.Contains(this.Request.SearchWord) ||
                                                //p.Format.Contains(this.Request.SearchWord) ||
                                                //p.Identifier.Contains(this.Request.SearchWord) ||
                                                //p.InitialNote.Contains(this.Request.SearchWord) ||
                                                p.Interviewee.Contains(this.Request.SearchWord) ||
                                                //p.InterviewerNote.Contains(this.Request.SearchWord) ||
                                                p.Keywords.Contains(this.Request.SearchWord) ||
                                                //p.LegalNote.Contains(this.Request.SearchWord) ||
                                                //p.Place.Contains(this.Request.SearchWord) ||
                                                //p.ProjectCode.Contains(this.Request.SearchWord) ||
                                                //p.Publisher.Contains(this.Request.SearchWord) ||
                                                //p.ReasonForPriority.Contains(this.Request.SearchWord) ||
                                                //p.RelationIsPartOf.Contains(this.Request.SearchWord) ||
                                                //p.Rights.Contains(this.Request.SearchWord) ||
                                                //p.ScopeAndContents.Contains(this.Request.SearchWord) ||
                                                //p.SecondEditCompleted.Contains(this.Request.SearchWord) ||
                                                //p.Rights.Contains(this.Request.SearchWord) ||
                                                p.Title.Contains(this.Request.SearchWord) ||
                                                p.Subject.Contains(this.Request.SearchWord)
                                              //p.TranscriberAssigned.Contains(this.Request.SearchWord) ||
                                              //p.TranscriberLocation.Contains(this.Request.SearchWord) ||
                                              //p.Transcript.Contains(this.Request.SearchWord) ||
                                              //p.TranscriptNote.Contains(this.Request.SearchWord) ||
                                              //p.Type.Contains(this.Request.SearchWord)
                                              );
            }

            List<transcription> allTranscription = this.TranscriptionRepository.GetAll().ToList();

            IEnumerable<transcription> dataset3 = allTranscription.Where<transcription>(predicate.Compile());

            List<transcription> all = dataset3.ToList();

            foreach (transcription item in all)
            {
                newlist.Add(Util.ConvertToTranscriptionModel(item));
            }

            this.Response = new ResponseModel()
            {
                Transcriptions = newlist,
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
