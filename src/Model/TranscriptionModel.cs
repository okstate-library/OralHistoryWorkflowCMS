using System;

namespace Model
{
    /// <summary>
    /// Defines the properties of the transcription model.
    /// </summary>
    public class TranscriptionModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the interview identifier.
        /// </summary>
        /// <value>
        /// The interview identifier.
        /// </value>
        public int InterviewId { get; set; }
        /// <summary>
        /// Gets or sets the project code.
        /// </summary>
        /// <value>
        /// The project code.
        /// </value>
        public string ProjectCode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is priority.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is priority; otherwise, <c>false</c>.
        /// </value>
        public bool IsPriority { get; set; }
        /// <summary>
        /// Gets or sets the reason for priority.
        /// </summary>
        /// <value>
        /// The reason for priority.
        /// </value>
        public string ReasonForPriority { get; set; }
        /// <summary>
        /// Gets or sets the initial note.
        /// </summary>
        /// <value>
        /// The initial note.
        /// </value>
        public string InitialNote { get; set; }
        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>
        /// The collection identifier.
        /// </value>
        public short CollectionId { get; set; }
        /// <summary>
        /// Gets or sets the subseries identifier.
        /// </summary>
        /// <value>
        /// The subseries identifier.
        /// </value>
        public int SubseriesId { get; set; }
        /// <summary>
        /// Gets or sets the interviewee.
        /// </summary>
        /// <value>
        /// The interviewee.
        /// </value>
        public string Interviewee { get; set; }
        /// <summary>
        /// Gets or sets the interviewer.
        /// </summary>
        /// <value>
        /// The interviewer.
        /// </value>
        public string Interviewer { get; set; }
        /// <summary>
        /// Gets or sets the interview date.
        /// </summary>
        /// <value>
        /// The interview date.
        /// </value>
        public string InterviewDate { get; set; }

        public string InterviewDate1 { get; set; }

        public string InterviewDate2 { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        public string Keywords { get; set; }
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets the scope and contents.
        /// </summary>
        /// <value>
        /// The scope and contents.
        /// </value>
        public string ScopeAndContents { get; set; }
        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        public string Format { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        /// <value>
        /// The publisher.
        /// </value>
        public string Publisher { get; set; }
        /// <summary>
        /// Gets or sets the relation is part of.
        /// </summary>
        /// <value>
        /// The relation is part of.
        /// </value>
        public string RelationIsPartOf { get; set; }
        /// <summary>
        /// Gets or sets the coverage spatial.
        /// </summary>
        /// <value>
        /// The coverage spatial.
        /// </value>
        public string CoverageSpatial { get; set; }
        /// <summary>
        /// Gets or sets the coverage temporal.
        /// </summary>
        /// <value>
        /// The coverage temporal.
        /// </value>
        public string CoverageTemporal { get; set; }
        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>
        /// The rights.
        /// </value>
        public string Rights { get; set; }
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }
        /// <summary>
        /// Gets or sets the transcript.
        /// </summary>
        /// <value>
        /// The transcript.
        /// </value>
        public string Transcript { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is in content dm.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in content dm; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnline { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is rosetta.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rosetta; otherwise, <c>false</c>.
        /// </value>
        public bool IsRosetta { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is rosetta form.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rosetta form; otherwise, <c>false</c>.
        /// </value>
        public bool IsRosettaForm { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is restriction.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is restriction; otherwise, <c>false</c>.
        /// </value>
        public bool IsRestriction { get; set; }

        public bool IsDarkArchive { get; set; }
        
        /// <summary>
        /// Gets or sets the legal note.
        /// </summary>
        /// <value>
        /// The legal note.
        /// </value>
        public string RestrictionNote { get; set; }
        
        public string DarkArchiveNote { get; set; }
        
        /// <summary>
        /// Gets or sets the equipment used.
        /// </summary>
        /// <value>
        /// The equipment used.
        /// </value>
        public string AudioEquipmentUsed { get; set; }

        /// <summary>
        /// Gets or sets the equipment used.
        /// </summary>
        /// <value>
        /// The equipment used.
        /// </value>
        public string VideoEquipmentUsed { get; set; }

        /// <summary>
        /// Gets or sets the interviewer note.
        /// </summary>
        /// <value>
        /// The interviewer note.
        /// </value>
        public string InterviewerNote { get; set; }

        /// <summary>
        /// Gets or sets the general note.
        /// </summary>
        /// <value>
        /// The general note.
        /// </value>
        public string GeneralNote { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is audio format.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is audio format; otherwise, <c>false</c>.
        /// </value>
        public bool IsAudioFormat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is video format.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is video format; otherwise, <c>false</c>.
        /// </value>
        public bool IsVideoFormat { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        /// <value>
        /// The place.
        /// </value>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the transcriber assigned.
        /// </summary>
        /// <value>
        /// The transcriber assigned.
        /// </value>
        public string TranscriberAssigned { get; set; }

        /// <summary>
        /// Gets or sets the audit check completed.
        /// </summary>
        /// <value>
        /// The audit check completed.
        /// </value>
        public string AuditCheckCompleted { get; set; }

        /// <summary>
        /// Gets or sets the first edit completed.
        /// </summary>
        /// <value>
        /// The first edit completed.
        /// </value>
        public string FirstEditCompleted { get; set; }

        /// <summary>
        /// Gets or sets the second edit completed.
        /// </summary>
        /// <value>
        /// The second edit completed.
        /// </value>
        public string SecondEditCompleted { get; set; }

        /// <summary>
        /// Gets or sets the third edit completed.
        /// </summary>
        /// <value>
        /// The third edit completed.
        /// </value>
        public string ThirdEditCompleted { get; set; }
        
        /// <summary>
        /// Gets or sets the draft sent date.
        /// </summary>
        /// <value>
        /// The draft sent date.
        /// </value>
        public Nullable<System.DateTime> DraftSentDate { get; set; }
        /// <summary>
        /// Gets or sets the edit with correction completed.
        /// </summary>
        /// <value>
        /// The edit with correction completed.
        /// </value>
        public string EditWithCorrectionCompleted { get; set; }

        /// <summary>
        /// Gets or sets the final edit completed.
        /// </summary>
        /// <value>
        /// The final edit completed.
        /// </value>
        public string FinalEditCompleted { get; set; }

        /// <summary>
        /// Gets or sets the final sent date.
        /// </summary>
        /// <value>
        /// The final sent date.
        /// </value>
        public Nullable<System.DateTime> FinalSentDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [transcript status].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [transcript status]; otherwise, <c>false</c>.
        /// </value>
        public bool TranscriptStatus { get; set; }

        /// <summary>
        /// Gets or sets the transcript note.
        /// </summary>
        /// <value>
        /// The transcript note.
        /// </value>
        public string TranscriptNote { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is born digital.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is born digital; otherwise, <c>false</c>.
        /// </value>
        public bool IsBornDigital { get; set; }
        /// <summary>
        /// Gets or sets the type of the original medium.
        /// </summary>
        /// <value>
        /// The type of the original medium.
        /// </value>
        public byte OriginalMediumType { get; set; }
        /// <summary>
        /// Gets or sets the original medium.
        /// </summary>
        /// <value>
        /// The original medium.
        /// </value>
        public string OriginalMedium { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is convert to digital.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is convert to digital; otherwise, <c>false</c>.
        /// </value>
        public bool IsConvertToDigital { get; set; }
        /// <summary>
        /// Gets or sets the convert to digital date.
        /// </summary>
        /// <value>
        /// The convert to digital date.
        /// </value>
        public Nullable<System.DateTime> ConvertToDigitalDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is access media status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is access media status; otherwise, <c>false</c>.
        /// </value>
        public bool IsAccessMediaStatus { get; set; }
        
        /// <summary>
        /// Gets or sets the technical specification.
        /// </summary>
        /// <value>
        /// The technical specification.
        /// </value>
        public string TechnicalSpecification { get; set; }

        /// <summary>
        /// Gets or sets the master file location.
        /// </summary>
        /// <value>
        /// The master file location.
        /// </value>
        public string MasterFileLocation { get; set; }
        /// <summary>
        /// Gets or sets the access file location.
        /// </summary>
        /// <value>
        /// The access file location.
        /// </value>
        public string AccessFileLocation { get; set; }
        /// <summary>
        /// Gets or sets the transcriber location.
        /// </summary>
        /// <value>
        /// The transcriber location.
        /// </value>
        public string TranscriptLocation { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public System.DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public int UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public System.DateTime UpdatedDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [release form].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [release form]; otherwise, <c>false</c>.
        /// </value>
        public bool ReleaseForm { get; set; }
        /// <summary>
        /// Gets or sets the name of the collection.
        /// </summary>
        /// <value>
        /// The name of the collection.
        /// </value>
        public string CollectionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the subseries.
        /// </summary>
        /// <value>
        /// The name of the subseries.
        /// </value>
        public string SubseriesName { get; set; }

        /// <summary>
        /// Gets the format text.
        /// </summary>
        /// <value>
        /// The format text.
        /// </value>
        public string FormatText
        {
            get
            {
                if (IsAudioFormat && IsVideoFormat)
                {
                    return "Audio & Video";
                }
                else if (IsAudioFormat && !IsVideoFormat)
                {
                    return "Audio";
                }
                else if (!IsAudioFormat && IsVideoFormat)
                {
                    return "Video";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the interviewer description.
        /// </summary>
        /// <value>
        /// The interviewer description.
        /// </value>
        public string InterviewerDescription { get; set; }

        /// <summary>
        /// Gets or sets the interviewer keywords.
        /// </summary>
        /// <value>
        /// The interviewer keywords.
        /// </value>
        public string InterviewerKeywords { get; set; }

        /// <summary>
        /// Gets or sets the interviewer subjects.
        /// </summary>
        /// <value>
        /// The interviewer subjects.
        /// </value>
        public string InterviewerSubjects { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [sent out].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sent out]; otherwise, <c>false</c>.
        /// </value>
        public bool SentOut { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the metadata draft.
        /// </summary>
        /// <value>
        /// The metadata draft.
        /// </value>
        public string MetadataDraft { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is a new audio equipment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a new audio equipment; otherwise, <c>false</c>.
        /// </value>
        public bool IsANewAudioEquipment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a new videoo equipment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a new videoo equipment; otherwise, <c>false</c>.
        /// </value>
        public bool IsANewVideoEquipment { get; set; }
    }
}
