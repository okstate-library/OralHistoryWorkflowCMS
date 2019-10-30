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

        public CollectionRepository CollectionRepository
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
            CollectionRepository = new CollectionRepository();

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
            List<transcription> filteredList = null;

            if (Request.IsAdminUser)
            {
                allTranscriptions = TranscriptionRepository.FindBy(p => p.IsDarkArchive == Request.TranscriptionSearchModel.IsDarkArchived).ToList();
            }
            else
            {
                allTranscriptions = TranscriptionRepository.FindBy(p => !p.IsRestriction && !p.IsDarkArchive).ToList();
            }

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
                    predicate = predicate.Or(p => p.IsOnline == bool.Parse(item));
                }

                IEnumerable<transcription> dataset3 = allTranscriptions.Where<transcription>(predicate.Compile());

                if (!string.IsNullOrEmpty(Request.SearchWord))
                {
                    predicate = (p => p.Interviewer.Contains(Request.SearchWord) ||
                                               p.Interviewee.Contains(Request.SearchWord) ||
                                               p.InterviewerNote.Contains(Request.SearchWord) ||
                                               p.Keywords.Contains(Request.SearchWord) ||
                                               p.RestrictionNote.Contains(Request.SearchWord) ||
                                               p.Place.Contains(Request.SearchWord) ||
                                               p.ProjectCode.Contains(Request.SearchWord) ||
                                               p.Title.Contains(Request.SearchWord) ||
                                               p.Subject.Contains(Request.SearchWord) ||
                                               p.Transcript.Contains(Request.SearchWord)
                                             );

                    IEnumerable<transcription> pagedTransactionList = TranscriptionRepository.FindBy(predicate).ToList();

                    dataset3 = dataset3.Union(pagedTransactionList);
                }

                filteredList = dataset3.ToList();

                pagedList = dataset3.ToPagedList(Request.SearchRequest.CurrentPage, Request.SearchRequest.ListLength);
            }
            else
            {
                filteredList = allTranscriptions;

                pagedList = allTranscriptions.ToPagedList(Request.SearchRequest.CurrentPage, Request.SearchRequest.ListLength);
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
                BrowseFormModel = GetFilterBoxes(filteredList),
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

        private BrowseFormModel GetFilterBoxes(IEnumerable<transcription> transcriptions)
        {
            BrowseFormModel browseFormModel = new BrowseFormModel();

            // Interview list

            List<string> interviewerList = new List<string>();
            List<string> subjectList = new List<string>();

            foreach (transcription item in transcriptions)
            {
                List<string> words = item.Interviewer.Split(';').Select(p => p.Trim()).ToList();

                foreach (string word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        interviewerList.Add(word);
                    }
                }

                string[] subjects = item.Subject.Split(';');

                foreach (string word in subjects)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        subjectList.Add(word);
                    }
                }
            }

            var interviewers = from x in interviewerList
                               group x by x into g
                               let count = g.Count()
                               orderby count descending
                               select new { Value = g.Key, Count = count };

            foreach (var interviewer in interviewers)
            {
                browseFormModel.InterviewerList.Add(BrowseFormModel.SetListBoxItem(interviewer.Value.Trim(), interviewer.Count, Request.TranscriptionSearchModel.Interviewers));
            }

            var subjectsList = from x in subjectList
                               group x by x into g
                               let count = g.Count()
                               orderby count descending
                               select new { Value = g.Key.Trim(), Count = count };

            foreach (var subject in subjectsList)
            {
                browseFormModel.SubjectList.Add(BrowseFormModel.SetListBoxItem(subject.Value.Trim(), subject.Count, Request.TranscriptionSearchModel.Subjects));
            }

            //Content DM list 

            var contentDMs = transcriptions
                              .GroupBy(n => n.IsOnline)
                              .Select(n => new
                              {
                                  IsOnline = n.Key,
                                  Count = n.Count()
                              });

            List<KeyValuePair<string, string>> contentDMList = new List<KeyValuePair<string, string>>();

            foreach (var item in contentDMs)
            {
                browseFormModel.ContentDmList.Add(BrowseFormModel.SetListBoxItem(item.IsOnline.ToString(), item.Count, Request.TranscriptionSearchModel.Contentdms));
            }


            // Dark Archieve List 
            ListBoxItem restriction = browseFormModel.RestrictionList.First();

            restriction.Name = Request.TranscriptionSearchModel.IsDarkArchived ? BrowseFormModel.DarkArchive : BrowseFormModel.NotDarkArchive;
            restriction.IsChecked = Request.TranscriptionSearchModel.IsDarkArchived;
            restriction.Count = transcriptions.Count();

            return browseFormModel;
        }




    }
}
