using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Core.Enums
{
    /// <summary>
    /// Defines the enumeration values of well known modification type
    /// </summary>
    public enum WellKnownTranscriptionModificationType
    {
        /// <summary>
        /// The transcript
        /// </summary>
        Transcript = 1,

        /// <summary>
        /// The media
        /// </summary>
        Media = 2,

        /// <summary>
        /// The metadata
        /// </summary>
        Metadata = 3,

        /// <summary>
        /// The supplement
        /// </summary>
        Supplement = 4,

    }
}
