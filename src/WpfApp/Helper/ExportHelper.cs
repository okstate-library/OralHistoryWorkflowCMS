using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp.Helper
{
    /// <summary>
    /// Defiens the functionalities and properties of Export helper class.
    /// </summary>
    public class ExportHelper
    {
        /// <summary>
        /// The temporary file
        /// </summary>
        string tempFile = Path.GetTempPath() + "/out.xls";

        /// <summary>
        /// Handles the Click event of the ExportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void Export(List<TranscriptionModel> transcriptions)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Title";
            xlWorkSheet.Cells[1, 2] = "Interviewee";
            xlWorkSheet.Cells[1, 3] = "Interviewer";
            xlWorkSheet.Cells[1, 4] = "Date Original";
            xlWorkSheet.Cells[1, 5] = "Date Digital";
            xlWorkSheet.Cells[1, 6] = "Subject";
            xlWorkSheet.Cells[1, 7] = "Keyword";
            xlWorkSheet.Cells[1, 8] = "Description";
            xlWorkSheet.Cells[1, 9] = "Scope and Contents";
            xlWorkSheet.Cells[1, 10] = "Format Type";
            xlWorkSheet.Cells[1, 11] = "Publisher";
            xlWorkSheet.Cells[1, 12] = "Collection Name";
            xlWorkSheet.Cells[1, 13] = "Subseries";
            xlWorkSheet.Cells[1, 14] = "Coverage - Spatial";
            xlWorkSheet.Cells[1, 15] = "Coverage - Temporal";
            xlWorkSheet.Cells[1, 16] = "Rights";
            xlWorkSheet.Cells[1, 17] = "Language";
            xlWorkSheet.Cells[1, 18] = "Project Code";

            int i = 2;

            foreach (TranscriptionModel transcriptionModel in transcriptions)
            {
                xlWorkSheet.Cells[i, 1] = transcriptionModel.Title;
                xlWorkSheet.Cells[i, 2] = transcriptionModel.Interviewee;
                xlWorkSheet.Cells[i, 3] = transcriptionModel.Interviewer;
                xlWorkSheet.Cells[i, 4] = transcriptionModel.InterviewDate;
                xlWorkSheet.Cells[i, 5] = transcriptionModel.ConvertToDigitalDate;
                xlWorkSheet.Cells[i, 6] = transcriptionModel.Subject;
                xlWorkSheet.Cells[i, 7] = transcriptionModel.Keywords;
                xlWorkSheet.Cells[i, 8] = transcriptionModel.Description;
                xlWorkSheet.Cells[i, 9] = transcriptionModel.ScopeAndContents;

                string format = string.Empty;

                if (transcriptionModel.IsAudioFormat && transcriptionModel.IsVideoFormat)
                {
                    format = "Audio/Video";
                }
                else if (transcriptionModel.IsAudioFormat)
                {
                    format = "Audio";
                }
                else if (transcriptionModel.IsVideoFormat)
                {
                    format = "Video";
                }

                xlWorkSheet.Cells[i, 10] = format;

                xlWorkSheet.Cells[i, 11] = transcriptionModel.Publisher;
                xlWorkSheet.Cells[i, 12] = transcriptionModel.CollectionName;
                xlWorkSheet.Cells[i, 13] = transcriptionModel.SubseriesName;
                xlWorkSheet.Cells[i, 14] = transcriptionModel.CoverageSpatial;
                xlWorkSheet.Cells[i, 15] = transcriptionModel.CoverageTemporal;
                xlWorkSheet.Cells[i, 16] = transcriptionModel.Rights;
                xlWorkSheet.Cells[i, 17] = transcriptionModel.Language;
                xlWorkSheet.Cells[i, 18] = transcriptionModel.ProjectCode;

                i++;
            }

            var path = System.IO.Path.GetFullPath(tempFile);

            FileHelper.DeleteFile(path);

            xlWorkBook.SaveAs(path,
                Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive,
                misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

            Process.Start(path);

        }

        /// <summary>
        /// Exports all.
        /// </summary>
        /// <param name="transcriptions">The transcriptions.</param>
        public void ExportAll(List<TranscriptionModel> transcriptions)
        {
            var path = System.IO.Path.GetFullPath(tempFile);

            FileHelper.DeleteFile(path);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Project Code";
            xlWorkSheet.Cells[1, 2] = "Priority";
            xlWorkSheet.Cells[1, 3] = "Reason Of Priority";
            xlWorkSheet.Cells[1, 4] = "Initial Note";
            xlWorkSheet.Cells[1, 5] = "Collection";

            xlWorkSheet.Cells[1, 6] = "Subseries";
            xlWorkSheet.Cells[1, 7] = "Interviewee";
            xlWorkSheet.Cells[1, 8] = "Interviewer";
            xlWorkSheet.Cells[1, 9] = "Date Original";
            xlWorkSheet.Cells[1, 10] = "Date Digital";

            xlWorkSheet.Cells[1, 11] = "Title";
            xlWorkSheet.Cells[1, 12] = "Subject";
            xlWorkSheet.Cells[1, 13] = "Keyword";
            xlWorkSheet.Cells[1, 14] = "Description";
            xlWorkSheet.Cells[1, 15] = "Interviewer Description";

            xlWorkSheet.Cells[1, 16] = "Interviewer Keywords";
            xlWorkSheet.Cells[1, 17] = "Interviewer Subjects";
            xlWorkSheet.Cells[1, 18] = "Scope and Contents";
            xlWorkSheet.Cells[1, 19] = "Format";
            xlWorkSheet.Cells[1, 20] = "Type";

            xlWorkSheet.Cells[1, 21] = "Publisher";
            xlWorkSheet.Cells[1, 22] = "Relation Is Part Of";
            xlWorkSheet.Cells[1, 23] = "Coverage Spatial";
            xlWorkSheet.Cells[1, 24] = "Coverage Temporal";
            xlWorkSheet.Cells[1, 25] = "Rights";

            xlWorkSheet.Cells[1, 26] = "Language";
            xlWorkSheet.Cells[1, 27] = "Identifier";
            xlWorkSheet.Cells[1, 28] = "Transcript";
            xlWorkSheet.Cells[1, 29] = "Filename";
            xlWorkSheet.Cells[1, 30] = "Online";

            xlWorkSheet.Cells[1, 31] = "Rosetta";
            xlWorkSheet.Cells[1, 32] = "Rosetta Form";
            xlWorkSheet.Cells[1, 33] = "Restriction";
            xlWorkSheet.Cells[1, 34] = "Restriction Note";
            xlWorkSheet.Cells[1, 35] = "Dark Archive";

            xlWorkSheet.Cells[1, 36] = "Dark Archive Note";
            xlWorkSheet.Cells[1, 37] = "Audio Equipment";
            xlWorkSheet.Cells[1, 38] = "Video Equipment";
            xlWorkSheet.Cells[1, 39] = "Equipment Number";
            xlWorkSheet.Cells[1, 40] = "Interviewer Note";

            xlWorkSheet.Cells[1, 41] = "General Note";
            xlWorkSheet.Cells[1, 42] = "Release Form";
            xlWorkSheet.Cells[1, 43] = "Place";
            xlWorkSheet.Cells[1, 44] = "Transcriber Assigned";
            xlWorkSheet.Cells[1, 45] = "Audit Completed";

            xlWorkSheet.Cells[1, 46] = "First Edit Complete";
            xlWorkSheet.Cells[1, 47] = "Second Edit Complete";
            xlWorkSheet.Cells[1, 48] = "Third Edit Complete";
            xlWorkSheet.Cells[1, 49] = "Draft Sent";
            xlWorkSheet.Cells[1, 50] = "Edit With Correction";

            xlWorkSheet.Cells[1, 51] = "Final Edit Complete";
            xlWorkSheet.Cells[1, 52] = "Final Sent";
            xlWorkSheet.Cells[1, 53] = "Status";
            xlWorkSheet.Cells[1, 54] = "Metadata Draft";
            xlWorkSheet.Cells[1, 55] = "Transcript Location";

            xlWorkSheet.Cells[1, 56] = "Transcript Note";
            xlWorkSheet.Cells[1, 57] = "Audio Format";
            xlWorkSheet.Cells[1, 58] = "Video Format";
            xlWorkSheet.Cells[1, 59] = "Born Digital";
            xlWorkSheet.Cells[1, 60] = "Original Medium";

            xlWorkSheet.Cells[1, 61] = "Convert to Digital";
            xlWorkSheet.Cells[1, 62] = "Access Media Status";
            xlWorkSheet.Cells[1, 63] = "Technical Specfication";
            xlWorkSheet.Cells[1, 64] = "Master File Location";
            xlWorkSheet.Cells[1, 65] = "Access File Location";

            xlWorkSheet.Cells[1, 66] = "Sent out";

            int i = 2;

            foreach (TranscriptionModel transcriptionModel in transcriptions)
            {
                xlWorkSheet.Cells[i, 1] = transcriptionModel.ProjectCode;
                xlWorkSheet.Cells[i, 2] = transcriptionModel.IsPriority.ToString();
                xlWorkSheet.Cells[i, 3] = transcriptionModel.ReasonForPriority;
                xlWorkSheet.Cells[i, 4] = transcriptionModel.InitialNote;
                xlWorkSheet.Cells[i, 5] = transcriptionModel.CollectionName;

                xlWorkSheet.Cells[i, 6] = transcriptionModel.SubseriesName;
                xlWorkSheet.Cells[i, 7] = transcriptionModel.Interviewee;
                xlWorkSheet.Cells[i, 8] = transcriptionModel.Interviewer;
                xlWorkSheet.Cells[i, 9] = transcriptionModel.InterviewDate + " " + transcriptionModel.InterviewDate1 + " " + transcriptionModel.InterviewDate2;
                xlWorkSheet.Cells[i, 10] = transcriptionModel.ConvertToDigitalDate;

                xlWorkSheet.Cells[i, 11] = transcriptionModel.Title;
                xlWorkSheet.Cells[i, 12] = transcriptionModel.Subject;
                xlWorkSheet.Cells[i, 13] = transcriptionModel.Keywords;
                xlWorkSheet.Cells[i, 14] = transcriptionModel.Description;
                xlWorkSheet.Cells[i, 15] = transcriptionModel.InterviewerDescription;

                xlWorkSheet.Cells[i, 16] = transcriptionModel.InterviewerKeywords;
                xlWorkSheet.Cells[i, 17] = transcriptionModel.InterviewerSubjects;
                xlWorkSheet.Cells[i, 18] = transcriptionModel.ScopeAndContents;

                string format = string.Empty;

                if (transcriptionModel.IsAudioFormat && transcriptionModel.IsVideoFormat)
                {
                    format = "Audio/Video";
                }
                else if (transcriptionModel.IsAudioFormat)
                {
                    format = "Audio";
                }
                else if (transcriptionModel.IsVideoFormat)
                {
                    format = "Video";
                }
                xlWorkSheet.Cells[i, 19] = format;
                xlWorkSheet.Cells[i, 20] = transcriptionModel.Type;

                xlWorkSheet.Cells[i, 21] = transcriptionModel.Publisher;
                xlWorkSheet.Cells[i, 22] = transcriptionModel.RelationIsPartOf;
                xlWorkSheet.Cells[i, 23] = transcriptionModel.CoverageSpatial;
                xlWorkSheet.Cells[i, 24] = transcriptionModel.CoverageTemporal;
                xlWorkSheet.Cells[i, 25] = transcriptionModel.Rights;

                xlWorkSheet.Cells[i, 26] = transcriptionModel.Language;
                xlWorkSheet.Cells[i, 27] = transcriptionModel.Identifier;
                xlWorkSheet.Cells[i, 28] = transcriptionModel.Transcript;
                xlWorkSheet.Cells[i, 29] = transcriptionModel.FileName;
                xlWorkSheet.Cells[i, 30] = transcriptionModel.IsOnline.ToString();

                xlWorkSheet.Cells[i, 31] = transcriptionModel.IsRosetta.ToString();
                xlWorkSheet.Cells[i, 32] = transcriptionModel.IsRosettaForm.ToString();
                xlWorkSheet.Cells[i, 33] = transcriptionModel.IsRestriction.ToString();
                xlWorkSheet.Cells[i, 34] = transcriptionModel.RestrictionNote;
                xlWorkSheet.Cells[i, 35] = transcriptionModel.IsDarkArchive.ToString();

                xlWorkSheet.Cells[i, 36] = transcriptionModel.DarkArchiveNote;
                xlWorkSheet.Cells[i, 37] = transcriptionModel.AudioEquipmentUsed;
                xlWorkSheet.Cells[i, 38] = transcriptionModel.VideoEquipmentUsed;
                xlWorkSheet.Cells[i, 39] = transcriptionModel.EquipmentNumber;
                xlWorkSheet.Cells[i, 40] = transcriptionModel.InterviewerNote;

                xlWorkSheet.Cells[i, 41] = transcriptionModel.GeneralNote;
                xlWorkSheet.Cells[i, 42] = transcriptionModel.ReleaseForm;
                xlWorkSheet.Cells[i, 43] = transcriptionModel.Place;
                xlWorkSheet.Cells[i, 44] = transcriptionModel.TranscriberAssigned;
                xlWorkSheet.Cells[i, 45] = transcriptionModel.AuditCheckCompleted;

                xlWorkSheet.Cells[i, 46] = transcriptionModel.FirstEditCompleted;
                xlWorkSheet.Cells[i, 47] = transcriptionModel.SecondEditCompleted;
                xlWorkSheet.Cells[i, 48] = transcriptionModel.ThirdEditCompleted;
                xlWorkSheet.Cells[i, 49] = transcriptionModel.DraftSentDate;
                xlWorkSheet.Cells[i, 50] = transcriptionModel.EditWithCorrectionCompleted;

                xlWorkSheet.Cells[i, 51] = transcriptionModel.FinalEditCompleted;
                xlWorkSheet.Cells[i, 52] = transcriptionModel.FinalSentDate;
                xlWorkSheet.Cells[i, 53] = transcriptionModel.TranscriptStatus ? "Complete" : "In Progress";
                xlWorkSheet.Cells[i, 54] = transcriptionModel.MetadataDraft;
                xlWorkSheet.Cells[i, 55] = transcriptionModel.TranscriptLocation;

                xlWorkSheet.Cells[i, 56] = transcriptionModel.TranscriptNote;
                xlWorkSheet.Cells[i, 57] = transcriptionModel.IsAudioFormat.ToString();
                xlWorkSheet.Cells[i, 58] = transcriptionModel.IsVideoFormat.ToString();
                xlWorkSheet.Cells[i, 59] = transcriptionModel.IsBornDigital.ToString();
                xlWorkSheet.Cells[i, 60] = transcriptionModel.OriginalMedium;

                xlWorkSheet.Cells[i, 61] = transcriptionModel.IsConvertToDigital.ToString();
                xlWorkSheet.Cells[i, 62] = transcriptionModel.IsAccessMediaStatus;
                xlWorkSheet.Cells[i, 63] = transcriptionModel.TechnicalSpecification;
                xlWorkSheet.Cells[i, 64] = transcriptionModel.MasterFileLocation;
                xlWorkSheet.Cells[i, 65] = transcriptionModel.AccessFileLocation;

                xlWorkSheet.Cells[i, 66] = transcriptionModel.SentOut;

                i++;
            }

            xlWorkBook.SaveAs(path,
                Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive,
                misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

            Process.Start(path);

        }

        public void ExportAllCollection(List<CollectionModel> collections)
        {
            var path = System.IO.Path.GetFullPath(tempFile);

            FileHelper.DeleteFile(path);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Collection Name";
            xlWorkSheet.Cells[1, 2] = "Number";
            xlWorkSheet.Cells[1, 3] = "Dates";
            xlWorkSheet.Cells[1, 4] = "Note";
            xlWorkSheet.Cells[1, 5] = "Subjects";

            xlWorkSheet.Cells[1, 6] = "Keywords";
            xlWorkSheet.Cells[1, 7] = "Description";
            xlWorkSheet.Cells[1, 8] = "Scope And Content";
            xlWorkSheet.Cells[1, 9] = "Custodial History";
            xlWorkSheet.Cells[1, 10] = "Size";

            xlWorkSheet.Cells[1, 11] = "Acquisition info";
            xlWorkSheet.Cells[1, 12] = "Language";
            xlWorkSheet.Cells[1, 13] = "Preservation Note";
            xlWorkSheet.Cells[1, 14] = "Rights";
            xlWorkSheet.Cells[1, 15] = "Access Restrictions";

            xlWorkSheet.Cells[1, 16] = "Publication Rights";
            xlWorkSheet.Cells[1, 17] = "Preferred Citation";
            xlWorkSheet.Cells[1, 18] = "Related Collection";
            xlWorkSheet.Cells[1, 19] = "Separated Material";
            xlWorkSheet.Cells[1, 20] = "Original Location";

            xlWorkSheet.Cells[1, 21] = "Copies Location";
            xlWorkSheet.Cells[1, 22] = "Publication Note";
            xlWorkSheet.Cells[1, 23] = "Creator";
            xlWorkSheet.Cells[1, 24] = "Contributors";
            xlWorkSheet.Cells[1, 25] = "ProcessedBy";

            xlWorkSheet.Cells[1, 26] = "Sponsors";

            int i = 2;

            foreach (CollectionModel collection in collections)
            {
                xlWorkSheet.Cells[i, 1] = collection.CollectionName;
                xlWorkSheet.Cells[i, 2] = collection.Number;
                xlWorkSheet.Cells[i, 3] = collection.Dates;
                xlWorkSheet.Cells[i, 4] = collection.Note;
                xlWorkSheet.Cells[i, 5] = collection.Subjects;

                xlWorkSheet.Cells[i, 6] = collection.Keywords;
                xlWorkSheet.Cells[i, 7] = collection.Description;
                xlWorkSheet.Cells[i, 8] = collection.ScopeAndContent;
                xlWorkSheet.Cells[i, 9] = collection.CustodialHistory;
                xlWorkSheet.Cells[i, 10] = collection.Size;

                xlWorkSheet.Cells[i, 11] = collection.Acquisitioninfo;
                xlWorkSheet.Cells[i, 12] = collection.Language;
                xlWorkSheet.Cells[i, 13] = collection.PreservationNote;
                xlWorkSheet.Cells[i, 14] = collection.Rights;
                xlWorkSheet.Cells[i, 15] = collection.AccessRestrictions;

                xlWorkSheet.Cells[i, 16] = collection.PublicationRights;
                xlWorkSheet.Cells[i, 17] = collection.PreferredCitation;
                xlWorkSheet.Cells[i, 18] = collection.RelatedCollection;
                xlWorkSheet.Cells[i, 19] = collection.SeparatedMaterial;
                xlWorkSheet.Cells[i, 20] = collection.OriginalLocation;

                xlWorkSheet.Cells[i, 21] = collection.CopiesLocation;
                xlWorkSheet.Cells[i, 22] = collection.PublicationNote;
                xlWorkSheet.Cells[i, 23] = collection.Creator;
                xlWorkSheet.Cells[i, 24] = collection.Contributors;
                xlWorkSheet.Cells[i, 25] = collection.ProcessedBy;

                xlWorkSheet.Cells[i, 26] = collection.Sponsors;

                i++;
            }

            xlWorkBook.SaveAs(path,
                Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive,
                misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

            Process.Start(path);

        }

        public void ExportAllSubseries(List<SubseryModel> subseriesList)
        {
            var path = System.IO.Path.GetFullPath(tempFile);

            FileHelper.DeleteFile(path);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Collection Name";
            xlWorkSheet.Cells[1, 2] = "Subseries Name";
            xlWorkSheet.Cells[1, 3] = "Number";
            xlWorkSheet.Cells[1, 4] = "Dates";
            xlWorkSheet.Cells[1, 5] = "Note";

            xlWorkSheet.Cells[1, 6] = "Subjects";
            xlWorkSheet.Cells[1, 7] = "Keywords";
            xlWorkSheet.Cells[1, 8] = "Description";
            xlWorkSheet.Cells[1, 9] = "Scope And Content";
            xlWorkSheet.Cells[1, 10] = "Custodial History";

            xlWorkSheet.Cells[1, 11] = "Size";
            xlWorkSheet.Cells[1, 12] = "Acquisition info";
            xlWorkSheet.Cells[1, 13] = "Language";
            xlWorkSheet.Cells[1, 14] = "Preservation Note";
            xlWorkSheet.Cells[1, 15] = "Rights";

            xlWorkSheet.Cells[1, 16] = "Access Restrictions";
            xlWorkSheet.Cells[1, 17] = "Publication Rights";
            xlWorkSheet.Cells[1, 18] = "Preferred Citation";
            xlWorkSheet.Cells[1, 19] = "Related Collection";
            xlWorkSheet.Cells[1, 20] = "Separated Material";

            xlWorkSheet.Cells[1, 21] = "Original Location";
            xlWorkSheet.Cells[1, 22] = "Copies Location";
            xlWorkSheet.Cells[1, 23] = "Publication Note";
            xlWorkSheet.Cells[1, 24] = "Creator";
            xlWorkSheet.Cells[1, 25] = "Contributors";

            xlWorkSheet.Cells[1, 26] = "ProcessedBy";
            xlWorkSheet.Cells[1, 27] = "Sponsors";

            int i = 2;

            foreach (SubseryModel subseries in subseriesList)
            {
                xlWorkSheet.Cells[i, 1] = subseries.CollectionName;
                xlWorkSheet.Cells[i, 2] = subseries.SubseryName;
                xlWorkSheet.Cells[i, 3] = subseries.Number;
                xlWorkSheet.Cells[i, 4] = subseries.Dates;
                xlWorkSheet.Cells[i, 5] = subseries.Note;

                xlWorkSheet.Cells[i, 6] = subseries.Subjects;
                xlWorkSheet.Cells[i, 7] = subseries.Keywords;
                xlWorkSheet.Cells[i, 8] = subseries.Description;
                xlWorkSheet.Cells[i, 9] = subseries.ScopeAndContent;
                xlWorkSheet.Cells[i, 10] = subseries.CustodialHistory;

                xlWorkSheet.Cells[i, 11] = subseries.Size;
                xlWorkSheet.Cells[i, 12] = subseries.Acquisitioninfo;
                xlWorkSheet.Cells[i, 13] = subseries.Language;
                xlWorkSheet.Cells[i, 14] = subseries.PreservationNote;
                xlWorkSheet.Cells[i, 15] = subseries.Rights;

                xlWorkSheet.Cells[i, 16] = subseries.AccessRestrictions;
                xlWorkSheet.Cells[i, 17] = subseries.PublicationRights;
                xlWorkSheet.Cells[i, 18] = subseries.PreferredCitation;
                xlWorkSheet.Cells[i, 19] = subseries.RelatedCollection;
                xlWorkSheet.Cells[i, 20] = subseries.SeparatedMaterial;

                xlWorkSheet.Cells[i, 21] = subseries.OriginalLocation;
                xlWorkSheet.Cells[i, 22] = subseries.CopiesLocation;
                xlWorkSheet.Cells[i, 23] = subseries.PublicationNote;
                xlWorkSheet.Cells[i, 24] = subseries.Creator;
                xlWorkSheet.Cells[i, 25] = subseries.Contributors;

                xlWorkSheet.Cells[i, 26] = subseries.ProcessedBy;
                xlWorkSheet.Cells[i, 27] = subseries.Sponsors;

                i++;
            }

            xlWorkBook.SaveAs(path,
                Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive,
                misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

            Process.Start(path);

        }

        /// <summary>
        /// Releases the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
                //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }

        }

    }
}
