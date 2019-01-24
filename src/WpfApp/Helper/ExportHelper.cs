using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp.Helper
{
    /// <summary>
    /// Defiens the functionalities and properties of Export helper class.
    /// </summary>
    public class ExportHelper
    {
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
                xlWorkSheet.Cells[i, 4] = transcriptionModel.CreatedDate;
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

            var path = System.IO.Path.GetFullPath("out.xls");

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
            catch (Exception ex)
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
