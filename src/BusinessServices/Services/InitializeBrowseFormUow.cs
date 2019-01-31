using EntityData;
using Model;
using Model.Transfer;
using Repository.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to InitializeBrowseFormUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class InitializeBrowseFormUow : UnitOfWork
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
        private CollectionRepository CollectionRepository
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
        /// Initializes a new instance of the <see cref="InitializeBrowseFormUow" /> class.
        /// </summary>
        public InitializeBrowseFormUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private InitializeBrowseFormUow(bool isReadOnly)
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

            BrowseFormModel browseFormModel = new BrowseFormModel();

            IQueryable<transcription> transcriptions = null;

            if (Request.IsAdminUser)
            {
                transcriptions = TranscriptionRepository.GetAll();
            }
            else
            {
                transcriptions = TranscriptionRepository.FindBy(p => !p.IsRestriction);
            }

            // Interview list

            List<string> interviewerList = new List<string>();

            foreach (transcription item in transcriptions)
            {
                string[] words = item.Interviewer.Split(';');

                foreach (string word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        interviewerList.Add(word);
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
                browseFormModel.InterviewerList.Add(SetPair(interviewer.Value, interviewer.Count));
            }
            
            //var intervieweeList = transcriptions
            //                       .GroupBy(n => n.Interviewer)
            //                       .Select(n => new
            //                       {
            //                           Interviewer = n.Key,
            //                           Count = n.Count()
            //                       })
            //                       .OrderBy(n => n.Interviewer);

            //foreach (var item in intervieweeList)
            //{
            //    browseFormModel.InterviewerList.Add(SetPair(item.Interviewer, item.Count));
            //}

            //Collection list 
            var collections = transcriptions
                                .GroupBy(n => n.CollectionId)
                                .Select(n => new
                                {
                                    CollectionId = n.Key,
                                    Count = n.Count()
                                });


            List<collection> daCollections = CollectionRepository.GetCollections();

            foreach (var item in collections)
            {
                collection collectionOjb = daCollections.First(c => c.Id == item.CollectionId);

                browseFormModel.CollectionList.Add(new KeyValuePair<string, string>(
                    collectionOjb.CollectionName + " (" + item.Count + ")", collectionOjb.Id.ToString()));
            }

            //Content DM list 

            var contentDMs = transcriptions
                              .GroupBy(n => n.IsInContentDm)
                              .Select(n => new
                              {
                                  IsInContentDm = n.Key,
                                  Count = n.Count()
                              });

            List<KeyValuePair<string, string>> contentDMList = new List<KeyValuePair<string, string>>();

            foreach (var item in contentDMs)
            {
                browseFormModel.ContentDmList.Add(
                    new KeyValuePair<string, string>(
                    item.IsInContentDm.ToString() + " (" + item.Count + ")", item.IsInContentDm.ToString()));

            }

            // Subject list
            List<string> subjectList = new List<string>();

            foreach (transcription item in transcriptions)
            {
                string[] words = item.Subject.Split(';');

                foreach (string word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        subjectList.Add(word);
                    }
                }
            }

            var subjects = from x in subjectList
                           group x by x into g
                           let count = g.Count()
                           orderby count descending
                           select new { Value = g.Key, Count = count };

            foreach (var subject in subjects)
            {
                browseFormModel.SubjectList.Add(SetPair(subject.Value, subject.Count));
            }

            Response = new ResponseModel()
            {
                BrowseFormModel = browseFormModel,
                IsOperationSuccess = true
            };

        }

        /// <summary>
        /// Sets the pair.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private KeyValuePair<string, string> SetPair(string name, int count)
        {
            return new KeyValuePair<string, string>(name + " (" + count + ")", name);
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
