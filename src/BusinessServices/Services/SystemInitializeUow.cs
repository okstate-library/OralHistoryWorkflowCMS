using BusinessServices.Services;
using EntityData;
using Model;
using Model.Transfer;
using Repository.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to SystemInitializeUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class SystemInitializeUow : UnitOfWork
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
        /// Gets or sets the SubseryRepository repository.
        /// </summary>
        /// <value>
        /// The collection repository.
        /// </value>
        private SubseryRepository SubseryRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the subject repository.
        /// </summary>
        /// <value>
        /// The subject repository.
        /// </value>
        private SubjectRepository SubjectRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the keyword repository.
        /// </summary>
        /// <value>
        /// The keyword repository.
        /// </value>
        private KeywordRepository KeywordRepository
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
        /// Initializes a new instance of the <see cref="SystemInitializeUow" /> class.
        /// </summary>
        public SystemInitializeUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private SystemInitializeUow(bool isReadOnly)
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
            this.CollectionRepository = new CollectionRepository();
            this.SubseryRepository = new SubseryRepository();
            this.SubjectRepository = new SubjectRepository();
            this.KeywordRepository = new KeywordRepository();

            this.WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {

            MainFormModel mainFormModel = new MainFormModel()
            {
                BrowseRecordCount = this.TranscriptionRepository.GetAll().Count(),
                TranscrptionQueueRecordCount = this.TranscriptionRepository.FindBy(t => t.TranscriptStatus == false).Count(),
            };

            List<CollectionModel> newlist = new List<CollectionModel>();

            foreach (collection item in CollectionRepository.GetCollections())
            {
                newlist.Add(Util.ConvertToCollectionModel(item));
            }

            List<SubseryModel> newSubseriesList = new List<SubseryModel>();

            foreach (subsery item in SubseryRepository.GetSubseries())
            {
                newSubseriesList.Add(Util.ConvertToSubseryModel(item));
            }

            List<SubjectModel> subjectList = new List<SubjectModel>();

            foreach (subject item in SubjectRepository.GetSubjects())
            {
                subjectList.Add(Util.ConvertToSubjectModel(item));
            }

            List<KeywordModel> keywordList = new List<KeywordModel>();

            foreach (keyword item in KeywordRepository.GetKeywords())
            {
                keywordList.Add(Util.ConvertToKeywordModel(item));
            }

            this.Response = new ResponseModel()
            {
                Subseries = newSubseriesList,
                Collecions = newlist,
                Subjects = subjectList,
                Keywords = keywordList,

                MainFormModel = mainFormModel,
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
