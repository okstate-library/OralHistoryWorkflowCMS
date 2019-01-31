using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessServices.Services;
using EntityData;
using Model;
using Model.Transfer;
using Model.Transfer.Search;
using PagedList;
using Repository;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to GetTranscriptionsForBrowseUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class GetTranscriptionsForBrowseUow : UnitOfWork
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
            IPagedList<transcription> pagedList = null;

            List<TranscriptionModel> newlist = new List<TranscriptionModel>();
            List<transcription> allTranscriptions = null;

            if (Request.IsAdminUser)
            {
                allTranscriptions = TranscriptionRepository.FindBy(p => p.IsRestriction == Request.TranscriptionSearchModel.IsRestrictionRecords).ToList();
            }
            else
            {
                allTranscriptions = TranscriptionRepository.FindBy(p => !p.IsRestriction).ToList();
            }

            pagedList = allTranscriptions.ToPagedList(Request.SearchRequest.CurrentPage, Request.SearchRequest.ListLength);

            if (Request.TranscriptionSearchModel.IsSearchRecordsExists() || !string.IsNullOrEmpty(Request.SearchWord))
            {
                var predicate = PredicateBuilder.False<transcription>();

                foreach (string item in Request.TranscriptionSearchModel.CollectionNames)
                {
                    predicate = predicate.Or(p => p.CollectionId == short.Parse(item));
                }

                foreach (string item in Request.TranscriptionSearchModel.Subjects)
                {
                    predicate = predicate.Or(p => p.Subject.Contains(item));
                }

                foreach (string item in Request.TranscriptionSearchModel.Interviewers)
                {
                    predicate = predicate.Or(p => p.Interviewer.Contains(item));
                }

                foreach (string item in Request.TranscriptionSearchModel.Contentdms)
                {
                    predicate = predicate.Or(p => p.IsInContentDm == bool.Parse(item));
                }



                IEnumerable<transcription> dataset3 = allTranscriptions.Where<transcription>(predicate.Compile());

                if (!string.IsNullOrEmpty(Request.SearchWord))
                {
                    predicate = (p => p.Interviewer.Contains(Request.SearchWord) ||
                                               p.Interviewee.Contains(Request.SearchWord) ||
                                               p.InterviewerNote.Contains(Request.SearchWord) ||
                                               p.Keywords.Contains(Request.SearchWord) ||
                                               p.LegalNote.Contains(Request.SearchWord) ||
                                               p.Place.Contains(Request.SearchWord) ||
                                               p.ProjectCode.Contains(Request.SearchWord) ||
                                               p.Title.Contains(Request.SearchWord) ||
                                               p.Subject.Contains(Request.SearchWord) ||
                                               p.Transcript.Contains(Request.SearchWord)
                                             );

                    IEnumerable<transcription> pagedTransactionList = TranscriptionRepository.FindBy(predicate).ToList();

                    dataset3 = dataset3.Union(pagedTransactionList);
                }


                pagedList = dataset3.ToPagedList(Request.SearchRequest.CurrentPage, Request.SearchRequest.ListLength);
            }

            PaginationInfo page = page = new PaginationInfo()
            {
                CurrentPage = pagedList.PageNumber,

                TotalListLength = pagedList.TotalItemCount,

                TotalPages = pagedList.PageCount,

                ListLength = pagedList.PageSize
            };

            foreach (transcription item in pagedList.ToList())
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
