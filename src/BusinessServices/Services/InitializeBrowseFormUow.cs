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
    internal class InitializeBrowseFormUow : UnitOfWork
    {

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
        /// 
        /// </summary>
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
            this.CollectionRepository = new CollectionRepository();

            this.WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {

            BrowseFormModel browseFormModel = new BrowseFormModel();

            IQueryable<transcription> transcriptions = this.TranscriptionRepository.GetAll();
            
            // Interview list
            var intervieweeList = transcriptions
                                   .GroupBy(n => n.Interviewer)
                                   .Select(n => new
                                   {
                                       Interviewer = n.Key,
                                       Count = n.Count()
                                   })
                                   .OrderBy(n => n.Interviewer);
            
            foreach (var item in intervieweeList)
            {
                browseFormModel.InterviewerList.Add(SetPair(item.Interviewer, item.Count));
            }

            //Collection list 
            var collections = transcriptions
                                .GroupBy(n => n.CollectionId)
                                .Select(n => new
                                {
                                    CollectionId = n.Key,
                                    Count = n.Count()
                                });


            List<collection> daCollections = this.CollectionRepository.GetCollections();

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
                string[] words = item.Subject.Split(',');

                foreach (string word in words)
                {
                    subjectList.Add(word);
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
            

            this.Response = new ResponseModel()
            {
                BrowseFormModel = browseFormModel,
                IsOperationSuccess = true
            };

        }

        private KeyValuePair<string, string> SetPair(string name, int count)
        {
            return new KeyValuePair<string, string>(name + " (" + count + ")",name);
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
