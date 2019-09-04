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
        /// Gets or sets the interviewer repository.
        /// </summary>
        /// <value>
        /// The interviewer repository.
        /// </value>
        public PredefineUserRepository PredefineUserRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the audio equipment used repository.
        /// </summary>
        /// <value>
        /// The audio equipment used repository.
        /// </value>
        public AudioEquipmentUsedRepository AudioEquipmentUsedRepository
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the video equipment used repository.
        /// </summary>
        /// <value>
        /// The video equipment used repository.
        /// </value>
        public VideoEquipmentUsedRepository VideoEquipmentUsedRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user type repository.
        /// </summary>
        /// <value>
        /// The user type repository.
        /// </value>
        public UserTypeRepository UserTypeRepository
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
            WellKnownError = new WellKnownErrors();

            TranscriptionRepository = new TranscriptionRepository();
            CollectionRepository = new CollectionRepository();
            SubseryRepository = new SubseryRepository();
            SubjectRepository = new SubjectRepository();
            KeywordRepository = new KeywordRepository();
            PredefineUserRepository = new PredefineUserRepository();
            AudioEquipmentUsedRepository = new AudioEquipmentUsedRepository();
            VideoEquipmentUsedRepository = new VideoEquipmentUsedRepository();
            UserTypeRepository = new UserTypeRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            MainFormModel mainFormModel = null;

            List<CollectionModel> newlist = new List<CollectionModel>();
            List<SubseryModel> newSubseriesList = new List<SubseryModel>();
            List<SubjectModel> subjectList = new List<SubjectModel>();
            List<KeywordModel> keywordList = new List<KeywordModel>();
            List<UserTypeModel> userTypes = new List<UserTypeModel>();

            //if (Request.IsStartup)
            //{
                mainFormModel = new MainFormModel()
                {
                    BrowseRecordCount = TranscriptionRepository.GetAll().Count(),
                    TranscrptionQueueRecordCount = TranscriptionRepository.FindBy(t => t.TranscriptStatus == false).Count(),
                };

                List<collection> collections = CollectionRepository.GetCollections();

                foreach (collection item in collections)
                {
                    newlist.Add(Util.ConvertToCollectionModel(item));
                }

                foreach (subsery item in SubseryRepository.GetSubseries())
                {
                    newSubseriesList.Add(Util.ConvertToSubseryModel(item, collections.FirstOrDefault(s => s.Id == item.CollectionId).CollectionName));
                }

                foreach (subject item in SubjectRepository.GetSubjects())
                {
                    subjectList.Add(Util.ConvertToSubjectModel(item));
                }

                foreach (keyword item in KeywordRepository.GetKeywords())
                {
                    keywordList.Add(Util.ConvertToKeywordModel(item));
                }

                foreach (usertype item in UserTypeRepository.GetAll())
                {
                    userTypes.Add(Util.ConvertToUsertypeModel(item));
                }
            //}

            List<PredefinedUserModel> predefineUserList = Util.ConvertToPredefinedUserModel(PredefineUserRepository.GetPredefinedUsers());

            List<string> audioEquipmentsUsed = AudioEquipmentUsedRepository.List();

            List<string> videoEquipmentsUsed = VideoEquipmentUsedRepository.List();
            
            Response = new ResponseModel()
            {
                Subseries = newSubseriesList,
                Collections = newlist,
                Subjects = subjectList,
                Keywords = keywordList,

                PredefinedUsers = predefineUserList,
                AudioEquipmentsUsed = audioEquipmentsUsed,
                VideoEquipmentsUsed = videoEquipmentsUsed,
                UserTypes = userTypes,

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
