using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Core.Enums
{
    /// <summary>
    /// Defines the enumeration values of well known user types.
    /// </summary>
    public enum WellKnownFindAndReplaceType
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The title
        /// </summary>
        Title = 1,

        /// <summary>
        /// The Description
        /// </summary>
        [Description("Description")]
        Description = 2,

        /// <summary>
        /// The Keywords
        /// </summary>
        [Description("Keywords")]
        Keywords = 3,

        /// <summary>
        /// The Subject
        /// </summary>
        [Description("Subject")]
        Subject = 4,
    }
}
