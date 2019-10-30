using System.Collections.Generic;

namespace WpfApp.Helper
{
    /// <summary>
    /// Collection model use for binding two combo boxes. ie Collection and series
    /// </summary>
    public class Repository
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
    }
}
