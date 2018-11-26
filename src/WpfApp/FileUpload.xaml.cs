using Microsoft.Win32;
using Model;
using Model.Transfer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for FileImport.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class FileImport : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileUpload" /> class.
        /// </summary>
        public FileImport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the UploadButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = ".xlsx";
            fileDialog.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx|CSV Files (*.csv)|*.csv";

            if (fileDialog.ShowDialog() == true)
            {
                string filePath = fileDialog.FileName;

                fileUploadLabel.Content = filePath;

                System.Threading.Thread.Sleep(2000);

                DataTable dt = ReadExcelFile(filePath);

                List<TranscriptionModel> transcriptionModels = new List<TranscriptionModel>();

                foreach (DataRow item in dt.Rows)
                {
                    CollectionModel collection = App.BaseUserControl.Collecions.FirstOrDefault(c => c.CollectionName.Contains(getStringValue(item, "Collection Name")));
                    short collectionId = collection != null ? (short)collection.Id : (short)1;

                    SubseryModel subseries = App.BaseUserControl.Subseries.FirstOrDefault(c => c.SubseryName.Contains(getStringValue(item, "Subseries")));
                    short subseriesId = subseries != null ? (short)subseries.Id : (short)1;

                    TranscriptionModel transcriptionModel = new TranscriptionModel()
                    {

                        //AccessFileLocation = GetStringValue(item, "Access File Location"),
                        //AuditCheckCompleted = GetStringValue(item, "Audit Check Completed"),
                        //AuditCheckCompletedDate = getDateValue(item, "Audit Check Completed Date"),
                        CollectionId = 1,//collectionId,
                        ConvertToDigitalDate = getDateValue(item, "Date Digital"),
                        CoverageSpatial = getStringValue(item, "Coverage-Spatial"),
                        CoverageTemporal = getStringValue(item, "Coverage-Temporal"),
                        CreatedBy = App.BaseUserControl.UserModel.UserId,
                        CreatedDate = DateTime.Now,
                        Description = getStringValue(item, "Description"),
                        //DraftSentDate = getDateValue(item, "Draft Sent Date"),
                        //EditWithCorrectionCompleted = GetStringValue(item, "Edit With Correction Completed"),
                        //EditWithCorrectionDate = getDateValue(item, "Edit With Correction Date"),
                        //EquipmentUsed = GetStringValue(item, "Equipment Used"),
                        //FileName = GetStringValue(item, "File Name"),
                        //FinalEditCompleted = GetStringValue(item, "Final Edit Completed"),
                        //FinalEditDate = getDateValue(item, "Final Edit Date"),
                        //FinalSentDate = getDateValue(item, "Final Sent Date"),
                        //FirstEditCompleted = GetStringValue(item, "First Edit Completed"),
                        //FirstEditCompletedDate = getDateValue(item, "First Edit Completed Date"),
                        Format = getStringValue(item, "Format"),
                        //Identifier = GetStringValue(item, "Identifier"),
                        //InitialNote = GetStringValue(item, "Initial Note"),
                        InterviewDate = (DateTime)getDateValue(item, "Date Original"),
                        Interviewee = getStringValue(item, "Interviewee"),
                        Interviewer = getStringValue(item, "Interviewer"),
                        //InterviewerNote = GetStringValue(item, "Interviewer Note"),
                        IsAccessMediaStatus = false, // getBoolValue(item, "Access Media Status"),
                        IsAudioFormat = false, // getBoolValue(item, "Audio"),
                        IsBornDigital = false, //getBoolValue(item, "Born Digital"),
                        IsConvertToDigital = false, //getBoolValue(item, "Convert To Digital"),
                        IsInContentDm = false, //getBoolValue(item, "In ContentDm"),
                        IsPriority = false, //getBoolValue(item, "Priority"),
                        IsRestriction = false, // getBoolValue(item, "Restriction"),
                        IsRosetta = false, // getBoolValue(item, "Rosetta"),
                        IsRosettaForm = false, //getBoolValue(item, "Rosetta Form"),
                        Keywords = getStringValue(item, "Keyword"),
                        Language = getStringValue(item, "Language"),
                        //LegalNote = GetStringValue(item, "Legal Note"),
                        //MasterFileLocation = GetStringValue(item, "Master File"),
                        //OriginalMedium = GetStringValue(item, "Original Medium"),
                        OriginalMediumType = 1,// GetStringValue(item, "Original Medium"),
                                               //Place = GetStringValue(item, "Place"),
                        ProjectCode = getStringValue(item, "Project Code"),
                        Publisher = getStringValue(item, "Publisher"),
                        //ReasonForPriority = GetStringValue(item, "Reason For Priority"),
                        //RelationIsPartOf = GetStringValue(item, "RelationIsPartOf"),
                        ReleaseForm = false, // getBoolValue(item, "Release Form"),
                        Rights = getStringValue(item, "Rights"),
                        ScopeAndContents = getStringValue(item, "Scope And Contents"),
                        //SecondEditCompleted = GetStringValue(item, "Second Edit Completed"),
                        //SecondEditCompletedDate =getDateValue(item, "Second Edit Completed Date"),
                        Subject = getStringValue(item, "Subject"),
                        SubseriesId = 1,//subseriesId,
                                        //TranscriberAssigned = GetStringValue(item, "Transcriber Assigned"),
                                        //TranscriberCompleted = getDateValue(item, "Transcriber Completed"),
                                        //TranscriberLocation = GetStringValue(item, "Transcriber Location"),
                                        //Transcript = GetStringValue(item, "Transcript"),
                        TranscriptLocation = 1, //GetStringValue(item, "TranscriptLocation"),
                                                //TranscriptNote = GetStringValue(item, "Transcript Note"),
                        TranscriptStatus = false, //getBoolValue(item, "Transcript Status"),
                        Title = getStringValue(item, "Title"),
                        Type = getStringValue(item, "Type"),
                        UpdatedBy = App.BaseUserControl.UserModel.UserId,
                        UpdatedDate = DateTime.Now,
                    };

                    transcriptionModels.Add(transcriptionModel);
                }

                RequestModel requestModel = new RequestModel()
                {
                    TranscriptionModels = transcriptionModels,
                };

                ResponseModel response = App.BaseUserControl.InternalService.ImportTranscription(requestModel);

                if (response.IsOperationSuccess)
                {
                    App.ShowMessage(true, string.Format(" {0} record(s) were \n successfully imported!", transcriptionModels.Count));
                }
                else
                {
                    App.ShowMessage(false, response.ErrorMessage);
                }
            }
        }
        
        /// <summary>
        /// Reads the excel file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private DataTable ReadExcelFile(string path)
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string fileExtension = System.IO.Path.GetExtension(path);

                if (fileExtension == ".xls")
                {
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                }
                else if (fileExtension == ".xlsx")
                {
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                }

                conn.Open();

                DataTable dts = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string[] excelSheets = new string[dts.Rows.Count];

                int i = 0;

                foreach (DataRow row in dts.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                using (OleDbCommand comm = new OleDbCommand())
                {
                    foreach (string sheetname in excelSheets)
                    {
                        comm.CommandText = "Select * from [" + sheetname + "]";
                        comm.Connection = conn;

                        using (OleDbDataAdapter da = new OleDbDataAdapter())
                        {
                            da.SelectCommand = comm;
                            da.Fill(dt);

                        }
                    }

                    return dt;
                }


            }
        }

        /// <summary>
        /// Gets the cell value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string getStringValue(DataRow row, string columnName)
        {
            object value = row[columnName];

            if (value != DBNull.Value)
            {
                return row[columnName].ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the time value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        private DateTime? getDateValue(DataRow row, string columnName)
        {
            if (!row.IsNull(columnName))
            {
                return Convert.ToDateTime(row[columnName].ToString());
            }

            return null;
        }

        /// <summary>
        /// Gets the bool value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        private bool getBoolValue(DataRow row, string columnName)
        {
            if (!row.IsNull(columnName))
            {
                return bool.Parse(row[columnName].ToString());
            }

            return false;
        }
        
        private void DownoadSampleButton_Click(object sender, RoutedEventArgs e)
        {
            var path = System.IO.Path.GetFullPath("Assets/cms_sample.xlsx");
            Process.Start(path);
        }
 
        private void Hyperlink_Click(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
