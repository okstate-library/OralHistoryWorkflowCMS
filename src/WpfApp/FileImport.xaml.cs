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
using System.Windows.Documents;
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

        private string IntervieweeColumnName = "Interviewee";
        private string CollectionColumnName = "Collection Name";
        private string SubseriesColumnName = "Subseries";
        private string DateDigitalColumnName = "Date Digital";
        private string DateOriginalColumnName = "Date Original";
        private string CoverageSpatialColumnName = "Coverage-Spatial";
        private string CoverageTemporalColumnName = "Coverage-Temporal";
        private string DescriptionColumnName = "Description";
        private string TranscriptionNotesColumnName = "Transcription Notes";
        private string EquipmentNumberColumnName = "Equipment #";
        private string MetadataDraftColumnName = "Metadata Draft";
        private string FormatColumnName = "Format";
        private string InterviewerColumnName = "Interviewer";
        private string InterviewerNotesColumnName = "Interviewer Notes";
        private string AccessMediaStatusColumnName = "Access Media Status";
        private string BornDigitalColumnName = "Born digital";
        private string ConvertedColumnName = "Converted";
        private string RestrictionsColumnName = "Restrictions";
        private string RestrictionNotesColumnName = "Restriction Notes";
        private string VideoEquipmentUsedColumnName = "Video Equipment Used";
        private string AudioEquipmentUsedColumnName = "Audio Equipment Used";
        private string KeywordColumnName = "Keyword";
        private string LanguageColumnName = "Language";
        private string ProjectCodeColumnName = "Project Code";
        private string PublisherColumnName = "Publisher";
        private string ReleaseFormColumnName = "Release Form";
        private string RightsColumnName = "Rights";
        private string ScopeAndContentsColumnName = "Scope And Contents";
        private string SubjectColumnName = "Subject";
        private string TranscriberAssignedColumnName = "Transcriber Assigned";
        private string LocationOfInterviewColumnName = "Location of Interview";
        private string TitleColumnName = "Title";
        private string TypeColumnName = "Type";
        private string InterviewerDescriptionColumnName = "Interviewer Description";
        private string InterviewerKeywordsColumnName = "Interviewer Keywords";
        private string InterviewerSubjectsColumnName = "Interviewer Subjects";
        private string RelationIsPartOfColumnName = "Relation-is part of";
        private string SentOutColumnName = "Sent Out";

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
            ClearError();

            string filePath = fileUploadLabel.Content.ToString();

            try
            {

                if (!string.IsNullOrEmpty(filePath))
                {
                    fileUploadLabel.Content = filePath;

                    DataTable dt = ReadExcelFile(filePath);

                    if (IsColumnExists(dt))
                    {
                        List<TranscriptionModel> transcriptionModels = new List<TranscriptionModel>();

                        int recordcount = 1;


                        foreach (DataRow item in dt.Rows)
                        {
                            string interviewee = GetStringValue(item, IntervieweeColumnName);

                            try
                            {
                                recordcount++;

                                if (recordcount == 445)
                                {
                                    string s = "";
                                }

                                // Reading and matching the collection name
                                string collectionName = GetStringValue(item, CollectionColumnName);

                                CollectionModel collection = App.BaseUserControl.Collecions.FirstOrDefault(c => c.CollectionName.Contains(collectionName));

                                short collectionId = 0;

                                if (collection != null)
                                {
                                    collectionId = (short)collection.Id;
                                }
                                else
                                {
                                    throw new Exception("collection not found '" + collectionName + "'");
                                }

                                // Reading and matching the subseries name
                                List<SubseryModel> subseriesList = App.BaseUserControl.Subseries.FindAll(s => s.CollectionId == collectionId);
                                int subseriesId = 0;
                                if (subseriesList != null)
                                {
                                    string subseriesName = GetStringValue(item, SubseriesColumnName);

                                    SubseryModel subseries = subseriesList.FirstOrDefault(c => c.SubseryName.Contains(subseriesName));

                                    SubseryModel nAsubseries = subseriesList.FirstOrDefault(s => s.SubseryName.Equals("N/A"));

                                    if (subseries != null)
                                    {
                                        subseriesId = (short)subseries.Id;
                                    }
                                    else if (nAsubseries != null)
                                    {
                                        subseriesId = (short)nAsubseries.Id;
                                    }
                                    else
                                    {
                                        throw new Exception("subseries not found '" + subseriesName + "'");
                                    }

                                }

                                TranscriptionModel transcriptionModel = new TranscriptionModel()
                                {
                                    CollectionId = collectionId,
                                    ConvertToDigitalDate = GetDateValue(item, DateDigitalColumnName),
                                    CoverageSpatial = GetStringValue(item, CoverageSpatialColumnName),
                                    CoverageTemporal = GetStringValue(item, CoverageTemporalColumnName),
                                    CreatedBy = App.BaseUserControl.UserModel.UserId,
                                    CreatedDate = DateTime.Now,
                                    Description = GetStringValue(item, DescriptionColumnName),
                                    TranscriptNote = GetStringValue(item, TranscriptionNotesColumnName),
                                    EquipmentNumber = GetStringValue(item, EquipmentNumberColumnName),
                                    MetadataDraft = GetStringValue(item, MetadataDraftColumnName),

                                    Format = GetStringValue(item, FormatColumnName),

                                    Interviewee = interviewee,
                                    Interviewer = GetStringValue(item, InterviewerColumnName),
                                    InterviewerNote = GetStringValue(item, InterviewerNotesColumnName),
                                    IsAccessMediaStatus = CheckStringContains(GetStringValue(item, AccessMediaStatusColumnName), "complete"),

                                    IsBornDigital = GetBoolValue(item, BornDigitalColumnName),
                                    IsConvertToDigital = GetBoolValue(item, ConvertedColumnName),
                                    IsOnline = false,
                                    IsPriority = false,
                                    IsRestriction = GetBoolValue(item, RestrictionsColumnName),
                                    RestrictionNote = GetStringValue(item, RestrictionNotesColumnName),
                                    IsDarkArchive = false,
                                    AudioEquipmentUsed = GetStringValue(item, AudioEquipmentUsedColumnName),
                                    VideoEquipmentUsed = GetStringValue(item, VideoEquipmentUsedColumnName),

                                    IsVideoFormat = !string.IsNullOrEmpty(GetStringValue(item, VideoEquipmentUsedColumnName)) ? true : false,
                                    IsAudioFormat = !string.IsNullOrEmpty(GetStringValue(item, AudioEquipmentUsedColumnName)) ? true : false,

                                    IsRosetta = false,
                                    IsRosettaForm = false,
                                    Keywords = GetStringValue(item, KeywordColumnName),
                                    Language = GetStringValue(item, LanguageColumnName),

                                    OriginalMediumType = 1,
                                    ProjectCode = GetStringValue(item, ProjectCodeColumnName),
                                    Publisher = GetStringValue(item, PublisherColumnName),

                                    ReleaseForm = GetBoolValue(item, ReleaseFormColumnName),
                                    Rights = GetStringValue(item, RightsColumnName),
                                    ScopeAndContents = GetStringValue(item, ScopeAndContentsColumnName),
                                    Subject = GetStringValue(item, SubjectColumnName),
                                    SubseriesId = subseriesId,
                                    TranscriberAssigned = GetStringValue(item, TranscriberAssignedColumnName),
                                    TranscriptLocation = 1,
                                    TranscriptStatus = true,
                                    Place = GetStringValue(item, LocationOfInterviewColumnName),
                                    Title = GetStringValue(item, TitleColumnName),
                                    Type = GetStringValue(item, TypeColumnName),
                                    InterviewerDescription = GetStringValue(item, InterviewerDescriptionColumnName),
                                    InterviewerKeywords = GetStringValue(item, InterviewerKeywordsColumnName),
                                    InterviewerSubjects = GetStringValue(item, InterviewerSubjectsColumnName),

                                    RelationIsPartOf = GetStringValue(item, RelationIsPartOfColumnName),

                                    SentOut = GetBoolValue(item, SentOutColumnName),

                                    UpdatedBy = App.BaseUserControl.UserModel.UserId,
                                    UpdatedDate = DateTime.Now,
                                };

                                SetDates(ref transcriptionModel, item);


                                transcriptionModels.Add(transcriptionModel);

                            }
                            catch (Exception ex)
                            {
                                //TextBlock tb = new TextBlock
                                //{
                                //    TextWrapping = TextWrapping.Wrap,
                                //    Margin = new Thickness(10)
                                //};

                                //tb.Inlines.Add(new Run("Row id-") { FontWeight = FontWeights.Bold });
                                //tb.Inlines.Add(recordcount.ToString());

                                //tb.Inlines.Add(new Run(" interviewee-") { FontWeight = FontWeights.Bold });
                                //tb.Inlines.Add(interviewee);

                                //tb.Inlines.Add(new Run(" error-") { FontWeight = FontWeights.Bold });
                                //tb.Inlines.Add(ex.Message + "\n");
                                //this.Content = tb;

                                StatTextBox.Text += "Row id-" + recordcount + " interviewee-" + interviewee + " error -" + ex.Message + "\n";
                            }
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
                }
                else
                {
                    App.ShowMessage(false, "Browse and select the file first.");
                }

            }
            catch (Exception ex)
            {
                App.ShowMessage(false, "Upload the correct formatted excel file. \n " + ex.Message);
            }

        }

        /// <summary>
        /// Determines whether [is column exists] [the specified dt].
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>
        ///   <c>true</c> if [is column exists] [the specified dt]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsColumnExists(DataTable dt)
        {
            string errorMessage = string.Empty;

            CheckColumnName(dt, TitleColumnName, ref errorMessage);
            CheckColumnName(dt, IntervieweeColumnName, ref errorMessage);
            CheckColumnName(dt, InterviewerColumnName, ref errorMessage);
            CheckColumnName(dt, DateOriginalColumnName, ref errorMessage);
            CheckColumnName(dt, DateDigitalColumnName, ref errorMessage);

            CheckColumnName(dt, SubjectColumnName, ref errorMessage);
            CheckColumnName(dt, KeywordColumnName, ref errorMessage);
            CheckColumnName(dt, DescriptionColumnName, ref errorMessage);
            CheckColumnName(dt, ScopeAndContentsColumnName, ref errorMessage);
            CheckColumnName(dt, FormatColumnName, ref errorMessage);
            CheckColumnName(dt, TypeColumnName, ref errorMessage);
            CheckColumnName(dt, PublisherColumnName, ref errorMessage);
            CheckColumnName(dt, CollectionColumnName, ref errorMessage);
            CheckColumnName(dt, SubseriesColumnName, ref errorMessage);

            CheckColumnName(dt, CoverageSpatialColumnName, ref errorMessage);
            CheckColumnName(dt, CoverageTemporalColumnName, ref errorMessage);
            CheckColumnName(dt, RightsColumnName, ref errorMessage);
            CheckColumnName(dt, LanguageColumnName, ref errorMessage);
            CheckColumnName(dt, ProjectCodeColumnName, ref errorMessage);
            CheckColumnName(dt, TranscriberAssignedColumnName, ref errorMessage);
            CheckColumnName(dt, TranscriptionNotesColumnName, ref errorMessage);
            CheckColumnName(dt, EquipmentNumberColumnName, ref errorMessage);

            CheckColumnName(dt, MetadataDraftColumnName, ref errorMessage);
            CheckColumnName(dt, LocationOfInterviewColumnName, ref errorMessage);
            CheckColumnName(dt, InterviewerDescriptionColumnName, ref errorMessage);
            CheckColumnName(dt, InterviewerKeywordsColumnName, ref errorMessage);
            CheckColumnName(dt, InterviewerSubjectsColumnName, ref errorMessage);
            CheckColumnName(dt, ReleaseFormColumnName, ref errorMessage);
            CheckColumnName(dt, RestrictionsColumnName, ref errorMessage);
            CheckColumnName(dt, RestrictionNotesColumnName, ref errorMessage);
            CheckColumnName(dt, AudioEquipmentUsedColumnName, ref errorMessage);
            CheckColumnName(dt, VideoEquipmentUsedColumnName, ref errorMessage);
            CheckColumnName(dt, InterviewerNotesColumnName, ref errorMessage);

            CheckColumnName(dt, SentOutColumnName, ref errorMessage);
            CheckColumnName(dt, AccessMediaStatusColumnName, ref errorMessage);
            CheckColumnName(dt, BornDigitalColumnName, ref errorMessage);
            CheckColumnName(dt, ConvertedColumnName, ref errorMessage);
            CheckColumnName(dt, RelationIsPartOfColumnName, ref errorMessage);
            CheckColumnName(dt, IntervieweeColumnName, ref errorMessage);
            CheckColumnName(dt, IntervieweeColumnName, ref errorMessage);
            CheckColumnName(dt, IntervieweeColumnName, ref errorMessage);
            CheckColumnName(dt, IntervieweeColumnName, ref errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                StatTextBox.Text += "*** Excel columns not exists *** \n" + errorMessage;

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks the name of the column.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="errorMessage">The error message.</param>
        private void CheckColumnName(DataTable dt, string columnName, ref string errorMessage)
        {
            if (dt.Columns[columnName] == null)
            {
                errorMessage += columnName + " \n";
            }
        }

        private void SetDates(ref TranscriptionModel transcriptionModel, DataRow item)
        {
            string date = GetStringValue(item, DateOriginalColumnName);

            string[] dates = date.Split(';');

            DateTime temp;

            string dateList = string.Empty;

            for (int i = 0; i < dates.Length; i++)
            {
                date = dates[i].Trim();

                if (DateTime.TryParse(date, out temp))
                {
                    if (i == 0)
                    {
                        transcriptionModel.InterviewDate = temp.ToShortDateString();
                    }

                    if (i == 1)
                    {
                        transcriptionModel.InterviewDate1 = temp.ToShortDateString();
                    }

                    if (i == 2)
                    {
                        transcriptionModel.InterviewDate2 = temp.ToShortDateString();
                    }
                }

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

        /// <summary>
        /// Clears the error.
        /// </summary>
        private void ClearError()
        {
            StatTextBox.Text = string.Empty;
        }
        #endregion
    }
}
