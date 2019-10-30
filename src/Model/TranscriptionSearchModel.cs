using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Defines the properties and methods of transcription search model
    /// </summary>
    public class TranscriptionSearchModel
    {
        /// <summary>
        /// Gets or sets the collection names.
        /// </summary>
        /// <value>
        /// The collection names.
        /// </value>
        public List<string> CollectionNames { get; set; }

        public List<int> CollectionIds { get; set; }

        /// <summary>
        /// Gets or sets the interviewers.
        /// </summary>
        /// <value>
        /// The interviewers.
        /// </value>
        public List<string> Interviewers { get; set; }

        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        /// <value>
        /// The subjects.
        /// </value>
        public List<string> Subjects { get; set; }

        /// <summary>
        /// Gets or sets the contentdms.
        /// </summary>
        /// <value>
        /// The contentdms.
        /// </value>
        public List<string> Contentdms { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is restriction records.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is restriction records; otherwise, <c>false</c>.
        /// </value>
        public bool IsDarkArchived { get; set; }

        /// <summary>
        /// Determines whether [is search records exists].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is search records exists]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSearchRecordsExists()
        {
            if (IsSearchRecordsExists(CollectionNames)
                || IsSearchRecordsExists(Interviewers)
                || IsSearchRecordsExists(Subjects)
                || IsSearchRecordsExists(Contentdms))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is search records exists] [the specified list].
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>
        ///   <c>true</c> if [is search records exists] [the specified list]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSearchRecordsExists(List<string> list)
        {
            if (list != null && list.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
