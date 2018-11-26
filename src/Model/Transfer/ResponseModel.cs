using Model.Transfer.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Transfer
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Model.BaseResponse" />
    public class ResponseModel : BaseResponse
    {
        /// <summary>
        /// Gets or sets the collecions.
        /// </summary>
        /// <value>
        /// The collecions.
        /// </value>
        public List<CollectionModel> Collecions { get; set; }

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
        /// Sets the transcription ids.
        /// </summary>
        public void SetTranscriptionIds()
        {
            this.TranscriptionIds = new List<int>();

            foreach (TranscriptionModel item in Transcriptions)
            {
                this.TranscriptionIds.Add(item.Id);
            }
        }

    }
}
