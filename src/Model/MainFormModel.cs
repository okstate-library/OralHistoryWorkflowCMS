using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Defines the main form model properties.
    /// </summary>
    public class MainFormModel
    {
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
    }
}
