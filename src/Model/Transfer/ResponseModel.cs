using Model.Transfer.Search;
using System.Collections.Generic;

namespace Model.Transfer
{
    /// <summary>
    /// Defines the properties for the response model.
    /// </summary>
    /// <seealso cref="Model.BaseResponse" />
    public class ResponseModel : BaseResponse
    {
        public List<RepositoryModel> Repositories { get; set; }

        /// <summary>
        /// Gets or sets the collecions.
        /// </summary>
        /// <value>
        /// The collecions.
        /// </value>
        public List<CollectionModel> Collections { get; set; }


        public RepositoryModel Repository { get; set; }

        /// <summary>
        /// Gets or sets the collecion.
        /// </summary>
        /// <value>
        /// The collecion.
        /// </value>
        public CollectionModel Collection { get; set; }

        /// <summary>
        /// Gets or sets the collection model.
        /// </summary>
        /// <value>
        /// The collection model.
        /// </value>
        public CollectionModel CollectionModel { get; set; }

        /// <summary>
        /// Gets or sets the subseries.
        /// </summary>
        /// <value>
        /// The subseries.
        /// </value>
        public List<SubseryModel> Subseries { get; set; }


        public SubseryModel SubseriesModel { get; set; }

        /// <summary>
        /// Gets or sets the transcription.
        /// </summary>
        /// <value>
        /// The transcription.
        /// </value>
        public TranscriptionModel Transcription { get; set; }

        /// <summary>
        /// Gets or sets the user model.
        /// </summary>
        /// <value>
        /// The user model.
        /// </value>
        public UserModel UserModel { get; set; }

        /// <summary>
        /// Gets or sets the transcriptions.
        /// </summary>
        /// <value>
        /// The transcriptions.
        /// </value>
        public List<TranscriptionModel> Transcriptions { get; set; }

        /// <summary>
        /// Gets or sets the transcription ids.
        /// </summary>
        /// <value>
        /// The transcription ids.
        /// </value>
        public List<int> TranscriptionIds { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        public List<KeywordModel> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the interviewers.
        /// </summary>
        /// <value>
        /// The interviewers.
        /// </value>
        public List<PredefinedUserModel> PredefinedUsers { get; set; }

        /// <summary>
        /// Gets or sets the audio equipments used.
        /// </summary>
        /// <value>
        /// The audio equipments used.
        /// </value>
        public List<string> AudioEquipmentsUsed { get; set; }

        /// <summary>
        /// Gets or sets the video equipments used.
        /// </summary>
        /// <value>
        /// The video equipments used.
        /// </value>
        public List<string> VideoEquipmentsUsed { get; set; }

        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        /// <value>
        /// The subjects.
        /// </value>
        public List<SubjectModel> Subjects { get; set; }

        /// <summary>
        /// Gets or sets the browse form model.
        /// </summary>
        /// <value>
        /// The browse form model.
        /// </value>
        public BrowseFormModel BrowseFormModel { get; set; }

        /// <summary>
        /// Gets or sets the main form model.
        /// </summary>
        /// <value>
        /// The main form model.
        /// </value>
        public MainFormModel MainFormModel { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public List<UserModel> Users { get; set; }

        /// <summary>
        /// Gets or sets the pagination information.
        /// </summary>
        /// <value>
        /// The pagination information.
        /// </value>
        public PaginationInfo PaginationInfo { get; set; }

        /// <summary>
        /// Gets or sets the user types.
        /// </summary>
        /// <value>
        /// The user types.
        /// </value>
        public List<UserTypeModel> UserTypes { get; set; }

        /// <summary>
        /// Sets the transcription ids.
        /// </summary>
        public void SetTranscriptionIds()
        {
            TranscriptionIds = new List<int>();

            foreach (TranscriptionModel item in Transcriptions)
            {
                TranscriptionIds.Add(item.Id);
            }
        }

    }
}
