
using Core.Enums;
using Model.Transfer.Search;
using System.Collections.Generic;

namespace Model.Transfer
{
    /// <summary>
    /// Defiens the properties for the request model.
    /// </summary>
    public class RequestModel
    {
        /// <summary>
        /// Gets or sets the interview identifier.
        /// </summary>
        /// <value>
        /// The interview identifier.
        /// </value>
        public int InterviewId { get; set; }

        /// <summary>
        /// Gets or sets the transcription identifier.
        /// </summary>
        /// <value>
        /// The transcription identifier.
        /// </value>
        public int TranscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the type of the well known modification.
        /// </summary>
        /// <value>
        /// The type of the well known modification.
        /// </value>
        public WellKnownModificationType WellKnownModificationType { get; set; }

        /// <summary>
        /// Gets or sets the user model.
        /// </summary>
        /// <value>
        /// The user model.
        /// </value>
        public UserModel UserModel { get; set; }

        /// <summary>
        /// Gets or sets the transcription model.
        /// </summary>
        /// <value>
        /// The transcription model.
        /// </value>
        public TranscriptionModel TranscriptionModel { get; set; }

        /// <summary>
        /// Gets or sets the type of the well known transcription modification.
        /// </summary>
        /// <value>
        /// The type of the well known transcription modification.
        /// </value>
        public WellKnownTranscriptionModificationType WellKnownTranscriptionModificationType { get; set; }

        private string searchWord;

        /// <summary>
        /// Gets or sets the search word.
        /// </summary>
        /// <value>
        /// The search word.
        /// </value>
        public string SearchWord
        {
            get
            {
                return searchWord.ToLower();
            }
            set
            {
                searchWord = value;
            }
        }

        /// <summary>
        /// Gets or sets the filter key words.
        /// </summary>
        /// <value>
        /// The filter key words.
        /// </value>
        public List<string> FilterKeyWords { get; set; }

        /// <summary>
        /// Gets or sets the transcription search model.
        /// </summary>
        /// <value>
        /// The transcription search model.
        /// </value>
        public TranscriptionSearchModel TranscriptionSearchModel { get; set; }

        /// <summary>
        /// Gets or sets the transcription models.
        /// </summary>
        /// <value>
        /// The transcription models.
        /// </value>
        public List<TranscriptionModel> TranscriptionModels { get; set; }

        /// <summary>
        /// Gets or sets the find replace model.
        /// </summary>
        /// <value>
        /// The find replace model.
        /// </value>
        public FindReplaceModel FindReplaceModel { get; set; }

        /// <summary>
        /// Gets or sets the search request dto.
        /// </summary>
        /// <value>
        /// The search request dto.
        /// </value>
        public SearchRequest SearchRequest { get; set; }

        /// <summary>
        /// Gets or sets the report model.
        /// </summary>
        /// <value>
        /// The report model.
        /// </value>
        public ReportModel ReportModel { get; set; }
    }
}
