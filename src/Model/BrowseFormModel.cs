using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Defiens the methods and the properties of the browse from model.
    /// </summary>
    public class BrowseFormModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseFormModel"/> class.
        /// </summary>
        public BrowseFormModel()
        {
            CollectionList = new List<KeyValuePair<string, string>>();
            SubjectList = new List<KeyValuePair<string, string>>();
            InterviewerList = new List<KeyValuePair<string, string>>();
            ContentDmList = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Gets or sets the collection list.
        /// </summary>
        /// <value>
        /// The collection list.
        /// </value>
        public List<KeyValuePair<string, string>> CollectionList { get; set; }

        /// <summary>
        /// Gets or sets the subject list.
        /// </summary>
        /// <value>
        /// The subject list.
        /// </value>
        public List<KeyValuePair<string, string>> SubjectList { get; set; }

        /// <summary>
        /// Gets or sets the interviewer list.
        /// </summary>
        /// <value>
        /// The interviewer list.
        /// </value>
        public List<KeyValuePair<string, string>> InterviewerList { get; set; }

        /// <summary>
        /// Gets or sets the content dm list.
        /// </summary>
        /// <value>
        /// The content dm list.
        /// </value>
        public List<KeyValuePair<string, string>> ContentDmList { get; set; }
    }
}
