﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TranscriptionModel
    {
        public int Id { get; set; }

        public int InterviewId { get; set; }
        public string ProjectCode { get; set; }
        public bool IsPriority { get; set; }
        public string ReasonForPriority { get; set; }
        public string InitialNote { get; set; }
        public short CollectionId { get; set; }
        public int SubseriesId { get; set; }
        public string Interviewee { get; set; }
        public string Interviewer { get; set; }
        public System.DateTime InterviewDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Subject { get; set; }
        public string ScopeAndContents { get; set; }
        public string Format { get; set; }
        public string Type { get; set; }
        public string Publisher { get; set; }
        public string RelationIsPartOf { get; set; }
        public string CoverageSpatial { get; set; }
        public string CoverageTemporal { get; set; }
        public string Rights { get; set; }
        public string Language { get; set; }
        public string Identifier { get; set; }
        public string Transcript { get; set; }
        public string FileName { get; set; }
        public bool IsInContentDm { get; set; }
        public bool IsRosetta { get; set; }
        public bool IsRosettaForm { get; set; }
        public bool IsRestriction { get; set; }
        public string LegalNote { get; set; }
        public string EquipmentUsed { get; set; }
        public string InterviewerNote { get; set; }
        public bool IsAudioFormat { get; set; }

        public bool IsVideoFormat { get; set; }

        public string Place { get; set; }
        public string TranscriberAssigned { get; set; }
        public Nullable<System.DateTime> TranscriberCompleted { get; set; }
        public string AuditCheckCompleted { get; set; }
        public Nullable<System.DateTime> AuditCheckCompletedDate { get; set; }
        public string FirstEditCompleted { get; set; }
        public Nullable<System.DateTime> FirstEditCompletedDate { get; set; }
        public string SecondEditCompleted { get; set; }
        public Nullable<System.DateTime> SecondEditCompletedDate { get; set; }
        public Nullable<System.DateTime> DraftSentDate { get; set; }
        public string EditWithCorrectionCompleted { get; set; }
        public Nullable<System.DateTime> EditWithCorrectionDate { get; set; }
        public string FinalEditCompleted { get; set; }
        public Nullable<System.DateTime> FinalEditDate { get; set; }
        public Nullable<System.DateTime> FinalSentDate { get; set; }
        public bool TranscriptStatus { get; set; }
        public byte TranscriptLocation { get; set; }
        public string TranscriptNote { get; set; }
        public bool IsBornDigital { get; set; }
        public byte OriginalMediumType { get; set; }
        public string OriginalMedium { get; set; }
        public bool IsConvertToDigital { get; set; }
        public Nullable<System.DateTime> ConvertToDigitalDate { get; set; }
        public bool IsAccessMediaStatus { get; set; }
        public string MasterFileLocation { get; set; }
        public string AccessFileLocation { get; set; }
        public string TranscriberLocation { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool ReleaseForm { get; set; }
        public string CollectionName { get; set; }

        public string SubseriesName { get; set; }

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

    }
}
