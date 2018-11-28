using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessServices.Services;
using Core.Enums;
using EntityData;
using Model;
using Model.Transfer;
using Repository;
using Repository.Implementations;
using PagedList;
using Model.Transfer.Search;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to GetTranscriptionsUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class GetTranscriptionsUow : UnitOfWork
    {

        /// <summary>
        /// gets and sets the request model
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
        /// Initializes a new instance of the <see cref="GetTranscriptionsUow" /> class.
        /// </summary>
        public GetTranscriptionsUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private GetTranscriptionsUow(bool isReadOnly)
            : base(isReadOnly)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="BusinessServices.WellKnownServiceErrors" />
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
            
            Expression<Func<transcription, bool>> predicate = PredicateBuilder.True<transcription>();

            predicate = predicate.And(p => p.TranscriptStatus == false);

            if (this.Request.FilterKeyWords != null && this.Request.FilterKeyWords.Count > 0)
            {
                foreach (string keyword in this.Request.FilterKeyWords)
                {

                    WellKnownTranscriptionQueueOption option =
                        (WellKnownTranscriptionQueueOption)Enum.Parse(
                            typeof(WellKnownTranscriptionQueueOption), keyword, true);

                    switch (option)
                    {
                        case WellKnownTranscriptionQueueOption.All:
                            break;
                        case WellKnownTranscriptionQueueOption.Priority:
                            predicate = predicate.And(p => p.IsPriority == true);
                            break;
                        case WellKnownTranscriptionQueueOption.Transcribed:
                            predicate = predicate.And(p => !string.IsNullOrEmpty(p.TranscriberAssigned));
                            break;
                        case WellKnownTranscriptionQueueOption.AuditCheck:
                            predicate = predicate.And(p => !string.IsNullOrEmpty(p.AuditCheckCompleted));
                            break;
                        case WellKnownTranscriptionQueueOption.FirstEdit:
                            predicate = predicate.And(p => !string.IsNullOrEmpty(p.FirstEditCompleted));
                            break;
                        case WellKnownTranscriptionQueueOption.SecondEdit:
                            predicate = predicate.And(p => !string.IsNullOrEmpty(p.SecondEditCompleted));
                            break;
                        case WellKnownTranscriptionQueueOption.DraftSent:
                            predicate = predicate.And(p => !string.IsNullOrEmpty(p.DraftSentDate.ToString()));
                            break;
                        case WellKnownTranscriptionQueueOption.Corrections:
                            predicate = predicate.And(p => !string.IsNullOrEmpty(p.EditWithCorrectionCompleted));
                            break;
                        case WellKnownTranscriptionQueueOption.FinalEdit:
                            predicate = predicate.And(p => !string.IsNullOrEmpty(p.FinalEditCompleted));
                            break;
                        case WellKnownTranscriptionQueueOption.SentOut:
                            predicate = predicate.And(p => !string.IsNullOrEmpty(p.FinalSentDate.ToString()));
                            break;
                        default:
                            break;
                    }

                }
            }

            if (!string.IsNullOrEmpty(this.Request.SearchWord))
            {
                predicate = predicate.And(p => p.Interviewer.Contains(this.Request.SearchWord) ||
                                                p.AuditCheckCompleted.Contains(this.Request.SearchWord) ||
                                                p.AccessFileLocation.Contains(this.Request.SearchWord) ||
                                                p.CoverageSpatial.Contains(this.Request.SearchWord) ||
                                                p.CoverageTemporal.Contains(this.Request.SearchWord) ||
                                                p.Description.Contains(this.Request.SearchWord) ||
                                                p.EditWithCorrectionCompleted.Contains(this.Request.SearchWord) ||
                                                p.EquipmentUsed.Contains(this.Request.SearchWord) ||
                                                p.FileName.Contains(this.Request.SearchWord) ||
                                                p.FinalEditCompleted.Contains(this.Request.SearchWord) ||
                                                p.FirstEditCompleted.Contains(this.Request.SearchWord) ||
                                                p.Format.Contains(this.Request.SearchWord) ||
                                                p.Identifier.Contains(this.Request.SearchWord) ||
                                                p.InitialNote.Contains(this.Request.SearchWord) ||
                                                p.Interviewee.Contains(this.Request.SearchWord) ||
                                                p.InterviewerNote.Contains(this.Request.SearchWord) ||
                                                p.Keywords.Contains(this.Request.SearchWord) ||
                                                p.LegalNote.Contains(this.Request.SearchWord) ||
                                                p.Place.Contains(this.Request.SearchWord) ||
                                                p.ProjectCode.Contains(this.Request.SearchWord) ||
                                                p.Publisher.Contains(this.Request.SearchWord) ||
                                                p.ReasonForPriority.Contains(this.Request.SearchWord) ||
                                                p.RelationIsPartOf.Contains(this.Request.SearchWord) ||
                                                p.Rights.Contains(this.Request.SearchWord) ||
                                                p.ScopeAndContents.Contains(this.Request.SearchWord) ||
                                                p.SecondEditCompleted.Contains(this.Request.SearchWord) ||
                                                p.Rights.Contains(this.Request.SearchWord) ||
                                                p.Title.Contains(this.Request.SearchWord) ||
                                                p.Subject.Contains(this.Request.SearchWord) ||
                                                p.TranscriberAssigned.Contains(this.Request.SearchWord) ||
                                                p.TranscriberLocation.Contains(this.Request.SearchWord) ||
                                                p.Transcript.Contains(this.Request.SearchWord) ||
                                                p.TranscriptNote.Contains(this.Request.SearchWord) ||
                                                p.Type.Contains(this.Request.SearchWord)
                                              );
            }
            
            IPagedList<transcription> pagedTransactionList = this.TranscriptionRepository.FindBy(predicate).
                OrderBy(t => t.Title).ToPagedList(this.Request.SearchRequest.CurrentPage,
               this.Request.SearchRequest.ListLength);

            PaginationInfo page = page = new PaginationInfo()
            {
                CurrentPage = pagedTransactionList.PageNumber,

                TotalListLength = pagedTransactionList.TotalItemCount,

                TotalPages = pagedTransactionList.PageCount,

                ListLength = pagedTransactionList.PageSize
            };

            foreach (transcription item in pagedTransactionList.ToList())
            {
                newlist.Add(Util.ConvertToTranscriptionModel(item));
            }

            this.Response = new ResponseModel()
            {
                PaginationInfo = page,
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
