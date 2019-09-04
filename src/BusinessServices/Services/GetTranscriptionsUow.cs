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
            WellKnownError = new WellKnownErrors();

            TranscriptionRepository = new TranscriptionRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {

            List<TranscriptionModel> newlist = new List<TranscriptionModel>();
            
            Expression<Func<transcription, bool>> predicate = PredicateBuilder.True<transcription>();

            predicate = predicate.And(p => p.TranscriptStatus == false);

            if (Request.FilterKeyWords != null && Request.FilterKeyWords.Count > 0)
            {
                foreach (string keyword in Request.FilterKeyWords)
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

            if (!string.IsNullOrEmpty(Request.SearchWord))
            {
                predicate = predicate.And(p => p.Interviewer.Contains(Request.SearchWord) ||
                                                p.AuditCheckCompleted.Contains(Request.SearchWord) ||
                                                p.AccessFileLocation.Contains(Request.SearchWord) ||
                                                p.CoverageSpatial.Contains(Request.SearchWord) ||
                                                p.CoverageTemporal.Contains(Request.SearchWord) ||
                                                p.Description.Contains(Request.SearchWord) ||
                                                p.EditWithCorrectionCompleted.Contains(Request.SearchWord) ||
                                                p.AudioEquipmentUsed.Contains(Request.SearchWord) ||
                                                p.VideoEquipmentUsed.Contains(Request.SearchWord) ||
                                                p.FileName.Contains(Request.SearchWord) ||
                                                p.FinalEditCompleted.Contains(Request.SearchWord) ||
                                                p.FirstEditCompleted.Contains(Request.SearchWord) ||
                                                p.Format.Contains(Request.SearchWord) ||
                                                p.Identifier.Contains(Request.SearchWord) ||
                                                p.InitialNote.Contains(Request.SearchWord) ||
                                                p.Interviewee.Contains(Request.SearchWord) ||
                                                p.InterviewerNote.Contains(Request.SearchWord) ||
                                                p.Keywords.Contains(Request.SearchWord) ||
                                                p.RestrictionNote.Contains(Request.SearchWord) ||
                                                p.Place.Contains(Request.SearchWord) ||
                                                p.ProjectCode.Contains(Request.SearchWord) ||
                                                p.Publisher.Contains(Request.SearchWord) ||
                                                p.ReasonForPriority.Contains(Request.SearchWord) ||
                                                p.RelationIsPartOf.Contains(Request.SearchWord) ||
                                                p.Rights.Contains(Request.SearchWord) ||
                                                p.ScopeAndContents.Contains(Request.SearchWord) ||
                                                p.SecondEditCompleted.Contains(Request.SearchWord) ||
                                                p.Rights.Contains(Request.SearchWord) ||
                                                p.Title.Contains(Request.SearchWord) ||
                                                p.Subject.Contains(Request.SearchWord) ||
                                                p.TranscriberAssigned.Contains(Request.SearchWord) ||
                                                p.TranscriptLocation.Contains(Request.SearchWord) ||
                                                p.Transcript.Contains(Request.SearchWord) ||
                                                p.TranscriptNote.Contains(Request.SearchWord) ||
                                                p.Type.Contains(Request.SearchWord)
                                              );
            }
            
            IPagedList<transcription> pagedTransactionList = TranscriptionRepository.FindBy(predicate).
                OrderBy(t => t.Title).ToPagedList(Request.SearchRequest.CurrentPage,
               Request.SearchRequest.ListLength);

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

            Response = new ResponseModel()
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
