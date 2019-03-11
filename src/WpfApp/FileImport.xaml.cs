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
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileUpload" /> class.
        /// </summary>
        public FileImport()
        {
            InitializeComponent();

            Loaded += FileImportUserControl_Loaded;
        }

        #endregion

        #region Events

        private void FileImportUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        /// <summary>
        /// Handles the Click event of the BrowseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = ".xlsx";
            fileDialog.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx|CSV Files (*.csv)|*.csv";

            if (fileDialog.ShowDialog() == true)
            {
                fileUploadLabel.Content = fileDialog.FileName;

                UploadButton.Visibility = Visibility.Visible;
            }
            else
            {
                UploadButton.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Handles the Click event of the UploadButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                string filePath = fileUploadLabel.Content.ToString();

                if (!string.IsNullOrEmpty(filePath))
                {
                    fileUploadLabel.Content = filePath;

                    DataTable dt = ReadExcelFile(filePath);

                    List<TranscriptionModel> transcriptionModels = new List<TranscriptionModel>();

                    foreach (DataRow item in dt.Rows)
                    {
    
                        CollectionModel collection = App.BaseUserControl.Collecions.FirstOrDefault(c => c.CollectionName.Contains(GetStringValue(item, "Collection Name")));
                        short collectionId = collection != null ? (short)collection.Id : (short)1;

                        SubseryModel subseries = App.BaseUserControl.Subseries.FirstOrDefault(c => c.SubseryName.Contains(GetStringValue(item, "Subseries")));
                        short subseriesId = subseries != null ? (short)subseries.Id : (short)1;

                        TranscriptionModel transcriptionModel = new TranscriptionModel()
                        {                          
                            CollectionId = 1,//collectionId,
                            ConvertToDigitalDate = GetDateValue(item, "Date Digital"),
                            CoverageSpatial = GetStringValue(item, "Coverage-Spatial"),
                            CoverageTemporal = GetStringValue(item, "Coverage-Temporal"),
                            CreatedBy = App.BaseUserControl.UserModel.UserId,
                            CreatedDate = DateTime.Now,
                            Description = GetStringValue(item, "Description"),
                            TranscriptNote = GetStringValue(item, "Transcription Notes"),
                            EquipmentNumber = GetStringValue(item, "Equipment #"),
                            MetadataDraft = GetStringValue(item, "Metadata Draft"),
                            
                            Format = GetStringValue(item, "Format"),
                     
                            InterviewDate = (DateTime)GetDateValue(item, "Date Original"),
                            Interviewee = GetStringValue(item, "Interviewee"),
                            Interviewer = GetStringValue(item, "Interviewer"),
                            InterviewerNote = GetStringValue(item, "Interviewer Notes"),
                            IsAccessMediaStatus = CheckStringContains(GetStringValue(item, "Access Media Status"), "complete"),

                            IsBornDigital = GetBoolValue(item, "Born digital"),
                            IsConvertToDigital = false, 
                            IsInContentDm = GetBoolValue(item, "Online"),
                            IsPriority = false,
                            IsRestriction = GetBoolValue(item, "Restrictions"),
                            LegalNote = GetStringValue(item, "Restriction Notes"),

                            AudioEquipmentUsed = GetStringValue(item, "Audio Equipment Used"),
                            VideoEquipmentUsed = GetStringValue(item, "Video Equipment Used"),

                            IsVideoFormat = !string.IsNullOrEmpty(GetStringValue(item, "Video Equipment Used")) ? true : false,
                            IsAudioFormat = !string.IsNullOrEmpty(GetStringValue(item, "Audio Equipment Used")) ? true : false,

                            IsRosetta = false, 
                            IsRosettaForm = false,
                            Keywords = GetStringValue(item, "Keyword"),
                            Language = GetStringValue(item, "Language"),
                         
                            OriginalMediumType = 1,
                            ProjectCode = GetStringValue(item, "Project Code"),
                            Publisher = GetStringValue(item, "Publisher"),
                         
                            ReleaseForm = GetBoolValue(item, "Release Form"), 
                            Rights = GetStringValue(item, "Rights"),
                            ScopeAndContents = GetStringValue(item, "Scope And Contents"),                        
                            Subject = GetStringValue(item, "Subject"),
                            SubseriesId = 1,//subseriesId,
                            TranscriberAssigned = GetStringValue(item, "Transcriber Assigned"),                         
                            TranscriptLocation = 1, //GetStringValue(item, "TranscriptLocation"),
                            TranscriptStatus = true,
                            Place = GetStringValue(item, "Location of Interview"),
                            Title = GetStringValue(item, "Title"),
                            Type = GetStringValue(item, "Type"),
                            InterviewerDescription = GetStringValue(item, "Interviewer Description"),
                            InterviewerKeywords = GetStringValue(item, "Interviewer Keywords"),
                            InterviewerSubjects = GetStringValue(item, "Interviewer Subjects"),

                            SentOut = GetBoolValue(item, "Sent Out"),

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
                        App.BaseUserControl.InitializeComponent(false);

                        App.ShowMessage(true, response.ErrorMessage);

                    }
                    else
                    {
                        App.ShowMessage(false, response.ErrorMessage);
                    }

                    ClearAll();
                }
                else
                {

                    App.ShowMessage(false, "Browse and select the file first.");
                }
            }
            catch (Exception ex)
            {
                App.ShowMessage(false, "Upload the correct formatted excel file.");
            }
        }

        /// <summary>
        /// Handles the Click event of the DownoadSampleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void DownoadSampleButton_Click(object sender, RoutedEventArgs e)
        {
            var path = System.IO.Path.GetFullPath("Assets/cms_sample.xlsx");
            Process.Start(path);
        }

        /// <summary>
        /// Handles the Click event of the Hyperlink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RequestNavigateEventArgs" /> instance containing the event data.</param>
        private void Hyperlink_Click(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check the vlue contain in the string
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private bool CheckStringContains(string word, string value)
        {

            bool iss = word.ToLower().Contains(value);
            return iss;
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
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";" + "Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
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
        private string GetStringValue(DataRow row, string columnName)
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
        private DateTime? GetDateValue(DataRow row, string columnName)
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
        private bool GetBoolValue(DataRow row, string columnName)
        {
            if (!row.IsNull(columnName))
            {
                if (row[columnName].ToString().ToLower().Equals("yes"))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Clears all.
        /// </summary>
        private void ClearAll()
        {
            fileUploadLabel.Content = string.Empty;
            UploadButton.Visibility = Visibility.Hidden;
        }

        #endregion
    }
}
