using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Core.Enums
{
    /// <summary>
    /// Defines the enumeration values of well known transcription queue search option
    /// </summary>
    public enum WellKnownTranscriptionQueueOption
    {

        /// <summary>
        /// All well known transcription queue search option
        /// </summary>
        [Description("All")]
        All = 1,

        /// <summary>
        /// The priority well known transcription queue search option
        /// </summary>
        [Description("Priority")]
        Priority = 2,

        /// <summary>
        /// The transcribed well known transcription queue search option
        /// </summary>
        [Description("Transcribed")]
        Transcribed = 3,

        /// <summary>
        /// The audit check well known transcription queue search option
        /// </summary>
        [Description("Audit Check")]
        AuditCheck = 4,

        /// <summary>
        /// The first edit well known transcription queue search option
        /// </summary>
        [Description("First Edit")]
        FirstEdit = 5,

        /// <summary>
        /// The second edit well known transcription queue search option
        /// </summary>
        [Description("Second Edit")]
        SecondEdit = 6,

        /// <summary>
        /// The draft sent well known transcription queue search option
        /// </summary>
        [Description("Draft Sent")]
        DraftSent = 7,

        /// <summary>
        /// The corrections well known transcription queue search option
        /// </summary>
        [Description("Corrections")]
        Corrections = 8,

        /// <summary>
        /// The final edit well known transcription queue search option
        /// </summary>
        [Description("Final Edit")]
        FinalEdit = 9,

        /// <summary>
        /// The sent out well known transcription queue search option
        /// </summary>
        [Description("Sent out")]
        SentOut = 10,

    }
}
