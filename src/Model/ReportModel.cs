using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Defines the properties and methods of report search model
    /// </summary>
    public class ReportModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is digitally migrated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is digitally migrated; otherwise, <c>false</c>.
        /// </value>
        public bool IsBornDigitally { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is converted digitally.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is converted digitally; otherwise, <c>false</c>.
        /// </value>
        public bool IsConvertedDigital { get; set; }

        /// <summary>
        /// Gets or sets the begin date.
        /// </summary>
        /// <value>
        /// The begin date.
        /// </value>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the interviewer.
        /// </summary>
        /// <value>
        /// The interviewer.
        /// </value>
        public string Interviewer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is online.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is online; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is offline.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is offline; otherwise, <c>false</c>.
        /// </value>
        public bool IsOffline { get; set; }
             
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }
    }
}
