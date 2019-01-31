using BusinessServices;
using Model;
using Model.Transfer;
using System.Collections.Generic;
using WpfApp.Properties;

namespace WpfApp.Helper
{
    /// <summary>
    /// Define base user control.
    /// </summary>
    public class BaseUserControl
    {
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName { get; } = Settings.Default.WelComeApplicationTitle;

        /// <summary>
        /// Gets or sets the transcrption queue record count.
        /// </summary>
        /// <value>
        /// The transcrption queue record count.
        /// </value>
        public int TranscrptionQueueRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the browse record count.
        /// </summary>
        /// <value>
        /// The browse record count.
        /// </value>
        public int BrowseRecordCount { get; set; }

        /// <summary>
        /// The instance
        /// </summary>
        private static InternalService instance = null;

        /// <summary>
        /// Gets or sets the user model.
        /// </summary>
        /// <value>
        /// The user model
        /// </value>
        public UserModel UserModel { get; set; }

        /// <summary>
        /// Gets or sets the subseries.
        /// </summary>
        /// <value>
        /// The subseries.
        /// </value>
        public List<SubseryModel> Subseries { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        public static List<KeywordModel> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        /// <value>
        /// The subjects.
        /// </value>
        public List<SubjectModel> Subjects { get; set; }

        /// <summary>
        /// Gets or sets the interviewers.
        /// </summary>
        /// <value>
        /// The interviewers.
        /// </value>
        public static List<string> Interviewers { get; set; }

        /// <summary>
        /// Gets or sets the audio equipments.
        /// </summary>
        /// <value>
        /// The audio equipments.
        /// </value>
        public static List<string> AudioEquipments { get; set; }

        /// <summary>
        /// Gets or sets the video equipments.
        /// </summary>
        /// <value>
        /// The video equipments.
        /// </value>
        public static List<string> VideoEquipments { get; set; }

        /// <summary>
        /// Gets the usertypes.
        /// </summary>
        /// <value>
        /// The usertypes.
        /// </value>
        public List<UserTypeModel> Usertypes { get; private set; }

        /// <summary>
        /// Gets or sets the collecions.
        /// </summary>
        /// <value>
        /// The collecions.
        /// </value>
        public List<CollectionModel> Collecions { get; set; }

        /// <summary>
        /// The default collection identifier
        /// </summary>
        public const short DefaultCollectionId = 1;

        /// <summary>
        /// Gets the internal service.
        /// </summary>
        /// <value>
        /// The internal service.
        /// </value>
        public InternalService InternalService
        {
            get
            {
                if (instance == null)
                {
                    instance = new InternalService();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is values changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is values changed; otherwise, <c>false</c>.
        /// </value>
        public static bool IsValuesChanged { get; set; }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public void InitializeComponent(bool isStartup)
        {
            RequestModel request = new RequestModel()
            {
                IsStartup = isStartup,
            };

            ResponseModel response = InternalService.GetSystemInitialize(request);

            if (response.IsOperationSuccess && isStartup)
            {
                TranscrptionQueueRecordCount = response.MainFormModel?.TranscrptionQueueRecordCount ?? 0;
                BrowseRecordCount = response.MainFormModel?.BrowseRecordCount ?? 0;

                Subseries = response.Subseries;
                Collecions = response.Collecions;
                Keywords = response.Keywords;
                Subjects = response.Subjects;

                Interviewers = response.Interviewers;
                AudioEquipments = response.AudioEquipmentsUsed;
                VideoEquipments = response.VideoEquipmentsUsed;
                Usertypes = response.UserTypes;
            }
            else if (response.IsOperationSuccess)
            {
                Interviewers = response.Interviewers;
                AudioEquipments = response.AudioEquipmentsUsed;
                VideoEquipments = response.VideoEquipmentsUsed;
            }
        }

    }
}
