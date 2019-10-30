using Model;
using System.Collections.Generic;

namespace WpfApp.Helper
{
    /// <summary>
    /// Collection model use for binding two combo boxes. ie Collection and series
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the series.
        /// </summary>
        /// <value>
        /// The series.
        /// </value>
        public List<KeyValuePair<int,string>> Series { get; set; }

        public List<SubseryModel> Subseries { get; set; }
    }
}
