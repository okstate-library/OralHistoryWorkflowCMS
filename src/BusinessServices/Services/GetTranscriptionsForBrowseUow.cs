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
            IPagedList<transcription> pagedList = null;

            List<TranscriptionModel> newlist = new List<TranscriptionModel>();

            List<transcription> allTranscriptions = this.TranscriptionRepository.GetAll().ToList();
            
            pagedList = allTranscriptions.ToPagedList(this.Request.SearchRequest.CurrentPage, this.Request.SearchRequest.ListLength);

            if (this.Request.TranscriptionSearchModel.IsSearchRecordsExists() || !string.IsNullOrEmpty(this.Request.SearchWord))
            {
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
                                                    p.Interviewee.Contains(this.Request.SearchWord) ||
                                                    p.Keywords.Contains(this.Request.SearchWord) ||
                                                    p.Title.Contains(this.Request.SearchWord) ||
                                                    p.Subject.Contains(this.Request.SearchWord)
                                                  );
                }

                IEnumerable<transcription> dataset3 = allTranscriptions.Where<transcription>(predicate.Compile());

                pagedList = dataset3.ToPagedList(this.Request.SearchRequest.CurrentPage, this.Request.SearchRequest.ListLength);
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
