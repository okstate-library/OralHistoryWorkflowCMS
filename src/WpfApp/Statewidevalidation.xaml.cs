using Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp.Helper;
using WpfApp.Properties;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Statewidevalidation.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Statewidevalidation : UserControl
    {
        /// <summary>
        /// The people CSV file path
        /// </summary>
        private static string PeopleCSVFilePath = "people.csv";

        /// <summary>
        /// The repository CSV file path
        /// </summary>
        private static string RepositoryCSVFilePath = "repository.csv";

        /// <summary>
        /// The interview CSV file path
        /// </summary>
        private static string CollectionCSVFilePath = "collection.csv";

        /// <summary>
        /// The interview CSV file path
        /// </summary>
        private static string InterviewCSVFilePath = "interview.csv";

        /// <summary>
        /// The dotted line
        /// </summary>
        private static string DottedLine = "-----------------------------------------------------------------";

        /// <summary>
        /// The empty string
        /// </summary>
        private static string EmptyString = "";

        /// <summary>
        /// The repository column name
        /// </summary>
        private static string RepositoryColumnName = "Repository";

        /// <summary>
        /// The interviewer column name
        /// </summary>
        private static string InterviewerColumnName = "Interviewer";

        /// <summary>
        /// The interviewee column name
        /// </summary>
        private static string IntervieweeColumnName = "Interviewee";

        /// <summary>
        /// The interview name column name
        /// </summary>
        private static string CollectionNameColumnName = "Collection Name";

        /// <summary>
        /// The interview sub title column name
        /// </summary>
        private static string CollectionSubTitleColumnName = "Collection Sub Title";

        /// <summary>
        /// The interview keywords column name
        /// </summary>
        private static string CollectionKeywordsColumnName = "Collection Keywords";

        /// <summary>
        /// The interview subject column name
        /// </summary>
        private static string CollectionSubjectColumnName = "Collection Subjects";

        /// <summary>
        /// The interview keywords column name
        /// </summary>
        private static string CollectionCallNumberColumnName = "Collection Call Number";

        /// <summary>
        /// The inclusive begin date column name
        /// </summary>
        private static string InclusiveBeginDateColumnName = "Inclusive Begin Date";

        /// <summary>
        /// The inclusive end date column name
        /// </summary>
        private static string InclusiveEndDateColumnName = "Inclusive End Date";

        /// <summary>
        /// The title column name
        /// </summary>
        private static string TitleColumnName = "Title";

        /// <summary>
        /// The date interview column name
        /// </summary>
        private static string DateInterviewColumnName = "Date Interview";

        /// <summary>
        /// The call number column name
        /// </summary>
        private static string CallNumberColumnName = "Call Number";

        /// <summary>
        /// The keywords column name
        /// </summary>
        private static string KeywordsColumnName = "Keywords";

        /// <summary>
        /// The loc subjects column name
        /// </summary>
        private static string LocSubjectsColumnName = "Loc Subjects";

        /// <summary>
        /// The extra comma
        /// </summary>
        private static string ExtraComma = " ,";
               
        /// <summary>
        /// Gets or sets the interview json models.
        /// </summary>
        /// <value>
        /// The interview json models.
        /// </value>
        public HashSet<CollectionApiModel> CollectionJsonModels { get; set; }

        /// <summary>
        /// Gets or sets the people json models.
        /// </summary>
        /// <value>
        /// The people json models.
        /// </value>
        public HashSet<PeopleModel> PeopleJsonModels { get; set; }

        /// <summary>
        /// Gets or sets the repository json models.
        /// </summary>
        /// <value>
        /// The repository json models.
        /// </value>
        public HashSet<RepositoryModel> RepositoryJsonModels { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Statewidevalidation" /> class.
        /// </summary>
        public Statewidevalidation()
        {
            InitializeComponent();

            WriteStatDetails("Upload the proper excel doucment to create relavant csv files.");

            Loaded += StatewidevalidationForm_Loaded;
        }

        /// <summary>
        /// Handles the Loaded event of the StatewidevalidationForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void StatewidevalidationForm_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeView();
        }

        /// <summary>
        /// Handles the Click event of the PeopleButton control.
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

            InitializeView();

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

                InitializeView();

                string filePath = fileUploadLabel.Content.ToString();

                if (!string.IsNullOrEmpty(filePath))
                {
                    // read the excel file
                    DataTable dt = dt = ReadExcelFile(filePath);

                    DeleteAllFiles();

                    //Read the content for people in excel and write csv file.
                    ReadAndWritePeopleData(dt);

                    //Read the content for repository in excel and write csv file.
                    ReadAndWriteRepositoryData(dt);

                    //Read the content for interview in excel and write csv file.
                    ReadAndWriteCollectionData(dt);

                    // Read the content for interview 
                    ReadAndWriteInterviewData(dt);
                }
                else
                {
                    App.ShowMessage(false, "Browse and select the file first.");
                }
            }
            catch (Exception)
            {
                App.ShowMessage(false, "Upload the correct formatted excel file.");
            }
        }

        /// <summary>
        /// Reads the and write interview data.
        /// </summary>
        /// <param name="dt">The dt.</param>
        private void ReadAndWriteInterviewData(DataTable dt)
        {
            // reading the json objects from the api
            List<InterviewApi> iterviewJsonObjects = GetJsonFileObjectList<InterviewApi>("interview", "interview.json");

            WriteStatDetails(string.Format("{0} interview record(s) found in the modx system", iterviewJsonObjects.Count()));

            // Convert the json object to people model
            HashSet<InterviewApiModel> interviewJsonModels = GetInterviewModelList(iterviewJsonObjects);

            HashSet<InterviewApiModel> collectionsModels = SetInterviewModel(dt);

            var intersectList = (from objA in collectionsModels
                                 join objB in interviewJsonModels on objA.Title equals objB.Title
                                 select objA).ToList();

            WriteStatDetails(string.Format("{0} interview record(s) found.", intersectList.Count()));

            IEnumerable<InterviewApiModel> newList = collectionsModels.Except(intersectList.ToList());

            WriteStatDetails(string.Format("{0} unique interview record(s) found in the uploaded excel document", newList.Count()));

            if (newList.Count() > 0)
            {
                var csv = new StringBuilder();

                csv.Append("parent,template,published,pagetitle,tv6,tv7,tv8,tv10,tv11,tv16,tv17" + Environment.NewLine);

                int correctRecordCount = 0;

                for (int i = 0; i < newList.Count(); i++)
                {
                    InterviewApiModel interviewModel = newList.ElementAt(i);

                    if (!string.IsNullOrEmpty(interviewModel.CollectionID)
                        && !string.IsNullOrEmpty(interviewModel.IntervieweesID)
                        && !string.IsNullOrEmpty(interviewModel.InterviewersID))
                    {

                        correctRecordCount++;

                        string locSubject = string.IsNullOrEmpty(interviewModel.LocSubjects) ? ExtraComma : interviewModel.LocSubjects;

                        if (i == newList.Count() - 1)
                        {
                            csv.Append(string.Format("5,3,1,{0},{1},{2},{3},{4},{5},{6},{7}",
                             interviewModel.Title, interviewModel.CallNumber, interviewModel.CollectionID,
                             interviewModel.Date,
                             interviewModel.IntervieweesID,
                             interviewModel.InterviewersID,
                             interviewModel.Keywords,
                             locSubject));
                        }
                        else
                        {
                            csv.Append(string.Format("5,3,1,{0},{1},{2},{3},{4},{5},{6},{7}{8}",
                              interviewModel.Title, interviewModel.CallNumber, interviewModel.CollectionID,
                              interviewModel.Date,
                              interviewModel.IntervieweesID,
                              interviewModel.InterviewersID,
                              interviewModel.Keywords,
                              locSubject,
                              Environment.NewLine));
                        }
                    }
                }


                if (correctRecordCount > 0)
                {
                    File.WriteAllText(InterviewCSVFilePath, csv.ToString());

                    WriteStatDetails(string.Format("{0} file created.", InterviewCSVFilePath));

                    ActiveButton(InterviewCSVButon);
                }
                else
                {
                    WriteStatDetails("Upload collection, interviewer's or interviewee's data first to create collection details.");

                    WriteStatDetails(string.Format("{0} not created.", "0"));

                    InActiveButton(InterviewCSVButon);
                }

            }
            else
            {
                WriteStatDetails(string.Format("{0} not created.", "0"));

                InActiveButton(InterviewCSVButon);
            }

            WriteStatDetails(string.Format(DottedLine));
            WriteStatDetails(string.Empty);
        }

        /// <summary>
        /// Reads the and write interview data.
        /// </summary>
        /// <param name="dt">The dt.</param>
        private void ReadAndWriteCollectionData(DataTable dt)
        {
            // reading the json objects from the api
            List<CollectionApi> collectionJsonObjects = GetJsonFileObjectList<CollectionApi>("collection", "collection.json");

            WriteStatDetails(string.Format("{0} collection record(s) found in the modx system", collectionJsonObjects.Count()));

            // Convert the json object to people model
            CollectionJsonModels = GetCollectionModelList(collectionJsonObjects);

            HashSet<CollectionApiModel> collectionsModels = SetCollectionModel(dt);

            var intersectList = (from objA in collectionsModels
                                 join objB in collectionJsonObjects on objA.Title equals objB.title
                                 select objA).ToList();

            WriteStatDetails(string.Format("{0} collection record(s) found.", intersectList.Count()));

            IEnumerable<CollectionApiModel> newList = collectionsModels.Except(intersectList.ToList());

            WriteStatDetails(string.Format("{0} unique collection record(s) found in the uploaded excel document", newList.Count()));

            int correctRecordCount = 0;

            if (newList.Count() > 0)
            {
                var csv = new StringBuilder();

                csv.Append("parent,template,published,pagetitle,tv1,tv2,tv3,tv4,tv16,tv17" + Environment.NewLine);

                for (int i = 0; i < newList.Count(); i++)
                {
                    CollectionApiModel repoModel = newList.ElementAt(i);

                    if (!string.IsNullOrEmpty(repoModel.RepositoryID))
                    {
                        correctRecordCount++;

                        string locSubject = string.IsNullOrEmpty(repoModel.LocSubjects) ? ExtraComma : repoModel.LocSubjects;

                        if (i == newList.Count() - 1)
                        {
                            csv.Append(string.Format("3,2,1,{0},{1},{2},{3},{4},{5},{6}",
                           repoModel.Title, repoModel.BeginDate, repoModel.EndDate, repoModel.CallNumber,
                           repoModel.RepositoryID, repoModel.Keywords, locSubject));
                        }
                        else
                        {
                            csv.Append(string.Format("3,2,1,{0},{1},{2},{3},{4},{5},{6}{7}",
                         repoModel.Title, repoModel.BeginDate, repoModel.EndDate, repoModel.CallNumber,
                         repoModel.RepositoryID, repoModel.Keywords, locSubject, Environment.NewLine));

                        }
                    }
                }

                if (correctRecordCount > 0)
                {
                    File.WriteAllText(CollectionCSVFilePath, csv.ToString());

                    WriteStatDetails(string.Format("{0} file not created.", CollectionCSVFilePath));

                    ActiveButton(CollectionCSVButon);
                }
                else
                {
                    WriteStatDetails("Upload repository data first to create interview details.");

                    WriteStatDetails(string.Format("{0} not created.", CollectionCSVFilePath));

                    InActiveButton(CollectionCSVButon);
                }

            }
            else
            {
                WriteStatDetails(string.Format("{0} not created.", CollectionCSVFilePath));

                InActiveButton(CollectionCSVButon);
            }

            WriteStatDetails(string.Format(DottedLine));
            WriteStatDetails(string.Empty);
        }

        /// <summary>
        /// Reads the and write repository data.
        /// </summary>
        /// <param name="dt">The dt.</param>
        private void ReadAndWriteRepositoryData(DataTable dt)
        {
            // check whether the column exists or not in the file.
            if (!dt.Columns.Contains(RepositoryColumnName))
            {
                WriteStatDetails("No records found related to repository in excel file.");

                WriteStatDetails(string.Format(DottedLine));
                WriteStatDetails(string.Empty);

                return;
            }

            // reading the json objects from the api
            List<Repository> repoJsonObjects = GetJsonFileObjectList<Repository>("repository", "repository.json");

            WriteStatDetails(string.Format("{0} repository record(s) found in the modx system", repoJsonObjects.Count()));

            // Convert the json object to people model
            RepositoryJsonModels = GetRepoModelList(repoJsonObjects);

            HashSet<RepositoryModel> repoModels = SetRepoModel(dt);

            var intersectList = (from objA in repoModels
                                 join objB in RepositoryJsonModels on objA.Title equals objB.Title
                                 select objA).ToList();

            WriteStatDetails(string.Format("{0} duplicate record(s) found.", intersectList.Count()));

            IEnumerable<RepositoryModel> newList = repoModels.Except(intersectList.ToList());

            WriteStatDetails(string.Format("{0} unique repository record(s) found in the uploaded excel document", newList.Count()));

            if (newList.Count() > 0)
            {
                var csv = new StringBuilder();

                csv.Append("parent,template,published,pagetitle,content,tv12,tv13,tv14" + Environment.NewLine);

                for (int i = 0; i < newList.Count(); i++)
                {
                    RepositoryModel repoModel = newList.ElementAt(i);

                    if (i == newList.Count() - 1)
                    {
                        csv.Append(string.Format("4,4,1,{0},{1},{2},{3},{4}",
                        repoModel.Title, "Content", "email address", "contact name", "repository url"));
                    }
                    else
                    {
                        csv.Append(string.Format("4,4,1,{0},{1},{2},{3},{4},{5}",
                       repoModel.Title, "Content", "email address", "contact name", "repository url", Environment.NewLine));
                    }
                }

                File.WriteAllText(RepositoryCSVFilePath, csv.ToString());

                WriteStatDetails(string.Format("{0} file created.", RepositoryCSVFilePath));

                ActiveButton(RepositoryCSVButon);
            }
            else
            {
                WriteStatDetails(string.Format("{0} not created.", RepositoryCSVFilePath));

                InActiveButton(RepositoryCSVButon);
            }

            WriteStatDetails(string.Format(DottedLine));
            WriteStatDetails(string.Empty);
        }

        /// <summary>
        /// Reads the and write people data.
        /// </summary>
        /// <param name="dt">The dt.</param>
        private void ReadAndWritePeopleData(DataTable dt)
        {
            if (dt.Columns.Contains(InterviewerColumnName) || dt.Columns.Contains(IntervieweeColumnName))
            {
                // reading the json objects from the api
                List<People> peopleJsonObjects = GetJsonFileObjectList<People>("people", "people.json");

                WriteStatDetails(string.Format("{0} people record(s) found in the modx system", peopleJsonObjects.Count()));

                // Convert the json object to people model
                PeopleJsonModels = GetPeopleModelList(peopleJsonObjects);

                HashSet<PeopleModel> peopleModels = SetPeopleModel(dt);

                var intersectList = (from objA in peopleModels
                                     join objB in PeopleJsonModels on objA.Fullname equals objB.Fullname
                                     select objA).ToList();

                WriteStatDetails(string.Format("{0} duplicate record(s) found.", intersectList.Count()));

                IEnumerable<PeopleModel> newList = peopleModels.Except(intersectList.ToList());

                WriteStatDetails(string.Format("{0} unique people record(s) found in the uploaded excel document", newList.Count()));

                if (newList.Count() > 0)
                {
                    var csv = new StringBuilder();

                    csv.Append("parent,template,published,pagetitle,content,tv18,tv19" + Environment.NewLine);

                    for (int i = 0; i < newList.Count(); i++)
                    {
                        PeopleModel peopleModel = newList.ElementAt(i);

                        if (i == newList.Count() - 1)
                        {
                            csv.Append(string.Format("2,5,1,{0},content for {0},{1},{2}",
                             peopleModel.Fullname, peopleModel.FirstName, peopleModel.LastName));
                        }
                        else
                        {
                            csv.Append(string.Format("2,5,1,{0},content for {0},{1},{2}{3}",
                              peopleModel.Fullname, peopleModel.FirstName, peopleModel.LastName, Environment.NewLine));
                        }
                    }

                    File.WriteAllText(PeopleCSVFilePath, csv.ToString());

                    WriteStatDetails(string.Format("{0} file created.", PeopleCSVFilePath));

                    ActiveButton(PeopleCSVButon);
                }
                else
                {
                    WriteStatDetails(string.Format("{0} not created.", PeopleCSVFilePath));

                    InActiveButton(PeopleCSVButon);
                }
            }
            else
            {
                WriteStatDetails(string.Format("No records found related to people in excel file."));
            }

            WriteStatDetails(string.Format(DottedLine));
            WriteStatDetails(string.Empty);
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        private void InitializeView()
        {
            StatTextBox.Text = string.Empty;

            InActiveButton(PeopleCSVButon);
            InActiveButton(RepositoryCSVButon);
            InActiveButton(CollectionCSVButon);
            InActiveButton(InterviewCSVButon);
        }

        private void ActiveButton(Button btn)
        {
            btn.IsHitTestVisible = true;
            btn.Style = (Style)FindResource("MaterialDesignRaisedAccentButton");
        }

        private void InActiveButton(Button btn)
        {
            btn.IsHitTestVisible = false;
            btn.Style = (Style)FindResource("MaterialDesignRaisedLightButton");
        }

        /// <summary>
        /// Gets the interview model list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        private HashSet<InterviewApiModel> GetInterviewModelList(List<InterviewApi> list)
        {
            HashSet<InterviewApiModel> interviewModels = new HashSet<InterviewApiModel>();

            foreach (InterviewApi interview in list)
            {
                interviewModels.Add(
                                new InterviewApiModel()
                                {
                                    Id = interview.id,
                                    Title = interview.title,
                                    CallNumber = interview.title,
                                    CollectionID = interview.id,
                                    Date = interview.date,
                                    IntervieweesID = interview.intervieweesID,
                                    InterviewersID = interview.interviewersID,
                                    Keywords = interview.title,
                                    LocSubjects = interview.title,
                                    SubTitle = interview.title,
                                    RepositoryID = interview.repositoryID,
                                }
                                );
            }

            return interviewModels;
        }

        /// <summary>
        /// Gets the interview model list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        private HashSet<CollectionApiModel> GetCollectionModelList(List<CollectionApi> list)
        {
            HashSet<CollectionApiModel> collectionModels = new HashSet<CollectionApiModel>();

            foreach (CollectionApi collection in list)
            {
                collectionModels.Add(
                                new CollectionApiModel()
                                {
                                    Id = collection.id,
                                    Title = collection.title,
                                    CallNumber = collection.title,
                                    BeginDate = collection.title,
                                    ContactName = collection.title,
                                    EndDate = collection.title,
                                    Keywords = collection.title,
                                    LocSubjects = collection.title,
                                    SubTitle = collection.title,
                                    RepositoryID = collection.repositoryID,
                                }
                                );
            }

            return collectionModels;
        }

        /// <summary>
        /// Gets the repo model list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        private HashSet<RepositoryModel> GetRepoModelList(List<Repository> list)
        {
            HashSet<RepositoryModel> repoModels = new HashSet<RepositoryModel>();

            foreach (Repository repo in list)
            {
                repoModels.Add(
                                new RepositoryModel()
                                {
                                    Id = repo.id,
                                    Title = repo.title,
                                }
                                );
            }

            return repoModels;
        }

        /// <summary>
        /// Gets the people model list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        private HashSet<PeopleModel> GetPeopleModelList(List<People> list)
        {
            HashSet<PeopleModel> peopleModels = new HashSet<PeopleModel>();

            foreach (People people in list)
            {
                peopleModels.Add(
                                new PeopleModel()
                                {
                                    Id = people.id,
                                    FirstName = people.firstName,
                                    LastName = people.lastName,
                                }
                                );
            }

            return peopleModels;
        }

        /// <summary>
        /// Sets the repo model.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="repoModels">The repo models.</param>
        private HashSet<RepositoryModel> SetRepoModel(DataTable dt)
        {
            HashSet<RepositoryModel> repoModels = new HashSet<RepositoryModel>();

            foreach (DataRow item in dt.Rows)
            {
                string text = GetStringValue(dt, item, RepositoryColumnName);

                if (!string.IsNullOrEmpty(text))
                {
                    RepositoryModel repoModel = new RepositoryModel()
                    {
                        Title = text
                    };

                    repoModels.Add(repoModel);
                }
            }

            return repoModels;
        }

        /// <summary>
        /// Sets the people model.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="peopleModels">The people models.</param>
        private HashSet<PeopleModel> SetPeopleModel(DataTable dt)
        {
            HashSet<PeopleModel> peopleModels = new HashSet<PeopleModel>();

            foreach (DataRow item in dt.Rows)
            {
                string interviewer = GetStringValue(dt, item, InterviewerColumnName);

                SetPeopleModel(interviewer, ref peopleModels);

                string Interviewee = GetStringValue(dt, item, IntervieweeColumnName);

                SetPeopleModel(Interviewee, ref peopleModels);
            }

            return peopleModels;

        }

        private void SetPeopleModel(string text, ref HashSet<PeopleModel> peopleModels)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Split(';').Length > 1)
                {
                    foreach (string splitName in text.Split(';'))
                    {
                        SetPeopleModel(splitName, ref peopleModels);
                    }
                }
                else
                {

                    string[] names = text.Split(',');

                    if (names.Length > 1)
                    {
                        PeopleModel peopleModel = new PeopleModel()
                        {
                            FirstName = names[1].Trim(),
                            LastName = names[0].Trim() + (names.Length == 3 ? " " + names[2].Trim() : string.Empty),
                        };

                        peopleModels.Add(peopleModel);
                    }
                    else
                    {
                        PeopleModel peopleModel = new PeopleModel()
                        {
                            FirstName = names[0].Trim(),
                        };

                        peopleModels.Add(peopleModel);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the interview model.
        /// </summary>
        /// <param name="DataRow">The data row.</param>
        /// <param name="collectionsModels">The collections models.</param>
        private HashSet<CollectionApiModel> SetCollectionModel(DataTable dt)
        {
            HashSet<CollectionApiModel> collectionsModels = new HashSet<CollectionApiModel>();

            foreach (DataRow dataRow in dt.Rows)
            {
                string title = GetStringValue(dt, dataRow, CollectionNameColumnName);

                if (!string.IsNullOrEmpty(title))
                {
                    string beginDate = GetStringValue(dt, dataRow, InclusiveBeginDateColumnName);
                    string callNumber = GetStringValue(dt, dataRow, CollectionCallNumberColumnName);
                    string endDate = GetStringValue(dt, dataRow, InclusiveEndDateColumnName);
                    string keyWords = GetStringValue(dt, dataRow, CollectionKeywordsColumnName);
                    string locSubjects = GetStringValue(dt, dataRow, CollectionSubjectColumnName);
                    string repositoryTitle = GetStringValue(dt, dataRow, RepositoryColumnName);
                    string subTitle = GetStringValue(dt, dataRow, CollectionSubTitleColumnName);

                    RepositoryModel currentRepo = RepositoryJsonModels.FirstOrDefault(r => r.Title == repositoryTitle);

                    CollectionApiModel peopleModel = new CollectionApiModel()
                    {
                        Title = title,
                        BeginDate = !string.IsNullOrEmpty(beginDate) ? beginDate : EmptyString,
                        CallNumber = !string.IsNullOrEmpty(callNumber) ? callNumber : EmptyString,
                        ContactName = EmptyString,
                        EndDate = !string.IsNullOrEmpty(endDate) ? endDate : EmptyString,

                        Keywords = !string.IsNullOrEmpty(keyWords) ? SetDelimeterTest(keyWords) : EmptyString,
                        LocSubjects = !string.IsNullOrEmpty(locSubjects) ? SetDelimeterTest(locSubjects) : EmptyString,

                        RepositoryID = (currentRepo != null ? currentRepo.Id : null),
                        SubTitle = !string.IsNullOrEmpty(subTitle) ? subTitle : EmptyString
                    };

                    collectionsModels.Add(peopleModel);

                }
            }

            return collectionsModels;
        }

        /// <summary>
        /// Sets the interview model.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private HashSet<InterviewApiModel> SetInterviewModel(DataTable dt)
        {
            HashSet<InterviewApiModel> interviewModels = new HashSet<InterviewApiModel>();

            foreach (DataRow dataRow in dt.Rows)
            {
                string title = GetStringValue(dt, dataRow, TitleColumnName);

                if (!string.IsNullOrEmpty(title))
                {
                    string date = GetStringValue(dt, dataRow, DateInterviewColumnName);

                    string intervieweeNames = GetStringValue(dt, dataRow, IntervieweeColumnName);
                    string intervieweerNames = GetStringValue(dt, dataRow, InterviewerColumnName);

                    string keyWords = GetStringValue(dt, dataRow, KeywordsColumnName);
                    string locSubjects = GetStringValue(dt, dataRow, LocSubjectsColumnName);

                    string repositoryTitle = GetStringValue(dt, dataRow, RepositoryColumnName);
                    string collectionTitle = GetStringValue(dt, dataRow, CollectionNameColumnName);

                    string subTitle = GetStringValue(dt, dataRow, CollectionSubTitleColumnName);
                    string callNumber = GetStringValue(dt, dataRow, CallNumberColumnName);

                    RepositoryModel currentRepo = RepositoryJsonModels.FirstOrDefault(r => r.Title == repositoryTitle);
                    CollectionApiModel currentCollection = CollectionJsonModels.FirstOrDefault(r => r.Title == collectionTitle);

                    List<string> intervieweeIds = new List<string>();

                    foreach (string item in intervieweeNames.Split(';'))
                    {
                        PeopleModel intervieweeModel = PeopleJsonModels.FirstOrDefault(r => (r.LastName.Trim() + ", " + r.FirstName.Trim()) == item.Trim());

                        if (intervieweeModel != null)
                        {
                            intervieweeIds.Add(intervieweeModel.Id);
                        }

                    }

                    List<string> intervieweerIds = new List<string>();

                    foreach (string item in intervieweerNames.Split(';'))
                    {
                        PeopleModel intervieweer = PeopleJsonModels.FirstOrDefault(r => (r.LastName.Trim() + ", " + r.FirstName.Trim()) == item.Trim());

                        if (intervieweer != null)
                        {
                            intervieweerIds.Add(intervieweer.Id);
                        }
                    }

                    InterviewApiModel peopleModel = new InterviewApiModel()
                    {
                        Title = title,
                        CollectionID = (currentCollection != null ? currentCollection.Id : null),
                        Date = !string.IsNullOrEmpty(date) ? date : EmptyString,
                        IntervieweesID = (intervieweeIds != null && intervieweeIds.Count > 0 ? string.Join("||", intervieweeIds) : null),
                        InterviewersID = (intervieweerIds != null && intervieweerIds.Count > 0 ? string.Join("||", intervieweerIds) : null),
                        CallNumber = !string.IsNullOrEmpty(callNumber) ? callNumber : EmptyString,
                        Keywords = !string.IsNullOrEmpty(keyWords) ? SetDelimeterTest(keyWords) : EmptyString,

                        LocSubjects = !string.IsNullOrEmpty(locSubjects) ? SetDelimeterTest(locSubjects) : EmptyString,

                        RepositoryID = (currentRepo != null ? currentRepo.Id : null),
                        SubTitle = !string.IsNullOrEmpty(subTitle) ? subTitle : EmptyString,
                    };

                    interviewModels.Add(peopleModel);

                }
            }

            return interviewModels;
        }

        /// <summary>
        /// Gets the json file object list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private List<T> GetJsonFileObjectList<T>(string entity, string filename)
        {
            try
            {
                WriteStatDetails(string.Format("Reading {0} external records in {1} json file...", entity, filename));

                string peopleJsonFile = Settings.Default.ExternalApi + filename;

                var json = new WebClient().DownloadString(peopleJsonFile);

                return JSONObjectMapper.GetObject<List<T>>(json);
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Removes the duplicates.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listPoints">The list points.</param>
        /// <returns></returns>
        public static List<T> RemoveDuplicates<T>(List<T> listPoints)
        {
            List<T> result = new List<T>();

            for (int i = 0; i < listPoints.Count; i++)
            {
                if (!result.Contains(listPoints[i]))
                    result.Add(listPoints[i]);
            }

            return result;
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
                string Import_FileName = path;
                string fileExtension = System.IO.Path.GetExtension(Import_FileName);

                if (fileExtension == ".xls")
                {
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                }
                else if (fileExtension == ".xlsx")
                {
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                }

                conn.Open();

                DataTable dts = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                String[] excelSheets = new String[dts.Rows.Count];

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
        /// Gets the string value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        private string GetStringValue(DataTable dt, DataRow row, string columnName)
        {
            if (dt.Columns.Contains(columnName))
            {
                object value = row[columnName];

                if (value != DBNull.Value)
                {
                    return row[columnName].ToString();
                }

            }

            return string.Empty;
        }

        /// <summary>
        /// Writes the stat details.
        /// </summary>
        /// <param name="stat">The stat.</param>
        private void WriteStatDetails(string stat)
        {
            StatTextBox.Text += stat + Environment.NewLine;
        }

        /// <summary>
        /// Handles the Click event of the DownoadUploadSampleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void DownoadUploadSampleButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadFile("Assets/modx_sample.xlsx");
        }

        /// <summary>
        /// Handles the Click event of the DownoadButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void DownoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag.Equals("people"))
            {
                DownloadFile(PeopleCSVFilePath);
            }
            else if (((Button)sender).Tag.Equals("repository"))
            {
                DownloadFile(RepositoryCSVFilePath);
            }
            else if (((Button)sender).Tag.Equals("interview"))
            {
                DownloadFile(CollectionCSVFilePath);
            }
            else if (((Button)sender).Tag.Equals("interview"))
            {
                DownloadFile(InterviewCSVFilePath);
            }
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        private void DownloadFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                var path = System.IO.Path.GetFullPath(filepath);
                Process.Start(path);
            }
            else
            {
                App.ShowMessage(false, "File not found. Please try again to create the file again.");
            }
        }

        /// <summary>
        /// Sets the delimeter test.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private string SetDelimeterTest(string text)
        {
            return text.Replace(";", "||").Replace(",", " ");
        }

        /// <summary>
        /// Deletes all files.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void DeleteAllFiles()
        {
            FileHelper.DeleteFile(PeopleCSVFilePath);
            FileHelper.DeleteFile(RepositoryCSVFilePath);
            FileHelper.DeleteFile(CollectionCSVFilePath);
            FileHelper.DeleteFile(InterviewCSVFilePath);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class People
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public string id;

        /// <summary>
        /// The title
        /// </summary>
        public string title;

        /// <summary>
        /// The first name
        /// </summary>
        public string firstName;

        /// <summary>
        /// The last name
        /// </summary>
        public string lastName;

        /// <summary>
        /// Gets the fullname.
        /// </summary>
        /// <value>
        /// The fullname.
        /// </value>
        public string fullname
        {
            get
            {
                return firstName + " " + lastName;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{WpfApp.PeopleModel}" />
    public class PeopleModel : IEquatable<PeopleModel>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public string Id;

        /// <summary>
        /// The first name
        /// </summary>
        public string FirstName;

        /// <summary>
        /// The last name
        /// </summary>
        public string LastName;

        /// <summary>
        /// Gets the fullname.
        /// </summary>
        /// <value>
        /// The fullname.
        /// </value>
        public string Fullname
        {
            get
            {
                return FirstName + (!string.IsNullOrEmpty(LastName) ? " " + LastName : "");
            }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Fullname.GetHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(PeopleModel other)
        {
            return Fullname.Equals(other.Fullname);
        }
    }

    /// <summary>
    /// Define proerties in repository class.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public string id;

        /// <summary>
        /// The title
        /// </summary>
        public string title;

        /// <summary>
        /// The sub title
        /// </summary>
        public string subTitle;

        /// <summary>
        /// The contact name
        /// </summary>
        public string contactName;

        /// <summary>
        /// The contact email
        /// </summary>
        public string contactEmail;

    }

    /// <summary>
    /// Define proerties in repository model class.
    /// </summary>
    /// <seealso cref="System.IEquatable{WpfApp.RepositoryModel}" />
    public class RepositoryModel : IEquatable<RepositoryModel>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public string Id;

        /// <summary>
        /// The title
        /// </summary>
        public string Title;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(RepositoryModel other)
        {
            return Title.Equals(other.Title);
        }
    }

    /// <summary>
    /// Define proerties in repository class.
    /// </summary>
    public class CollectionApi
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public string id;

        /// <summary>
        /// The title
        /// </summary>
        public string title;

        /// <summary>
        /// The sub title
        /// </summary>
        public string subTitle;

        /// <summary>
        /// The repository identifier
        /// </summary>
        public string repositoryID;

        /// <summary>
        /// The contact name
        /// </summary>
        public string contactName;

        /// <summary>
        /// The begin date
        /// </summary>
        public string beginDate;

        /// <summary>
        /// The end date
        /// </summary>
        public string endDate;

        /// <summary>
        /// The call number
        /// </summary>
        public string callNumber;

        /// <summary>
        /// The keywords
        /// </summary>
        public string keywords;

        /// <summary>
        /// The loc subjects
        /// </summary>
        public string locSubjects;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{WpfApp.CollectionApiModel}" />
    /// <seealso cref="System.IEquatable{WpfApp.CollectionModel}" />
    public class CollectionApiModel : IEquatable<CollectionApiModel>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public string Id;

        /// <summary>
        /// The title
        /// </summary>
        public string Title;

        /// <summary>
        /// The sub title
        /// </summary>
        public string SubTitle;

        /// <summary>
        /// The repository identifier
        /// </summary>
        public string RepositoryID;

        /// <summary>
        /// The contact name
        /// </summary>
        public string ContactName;

        /// <summary>
        /// The begin date
        /// </summary>
        public string BeginDate;

        /// <summary>
        /// The end date
        /// </summary>
        public string EndDate;

        /// <summary>
        /// The call number
        /// </summary>
        public string CallNumber;

        /// <summary>
        /// The keywords
        /// </summary>
        public string Keywords;

        /// <summary>
        /// The loc subjects
        /// </summary>
        public string LocSubjects;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(CollectionApiModel other)
        {
            return Title.Equals(other.Title);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InterviewApi
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public string id;

        /// <summary>
        /// The title
        /// </summary>
        public string title;

        /// <summary>
        /// The sub title
        /// </summary>
        public string subTitle;

        /// <summary>
        /// The interview identifier
        /// </summary>
        public string collectionID;

        /// <summary>
        /// The repository identifier
        /// </summary>
        public string repositoryID;

        /// <summary>
        /// The contact name
        /// </summary>
        public string callNumber;

        /// <summary>
        /// The begin date
        /// </summary>
        public string date;

        /// <summary>
        /// The end date
        /// </summary>
        public string intervieweesID;

        /// <summary>
        /// The call number
        /// </summary>
        public string interviewersID;

        /// <summary>
        /// The keywords
        /// </summary>
        public string keywords;

        /// <summary>
        /// The loc subjects
        /// </summary>
        public string locSubjects;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{WpfApp.InterviewApiModel}" />
    public class InterviewApiModel : IEquatable<InterviewApiModel>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public string Id;

        /// <summary>
        /// The title
        /// </summary>
        public string Title;

        /// <summary>
        /// The sub title
        /// </summary>
        public string SubTitle;

        /// <summary>
        /// The interview identifier
        /// </summary>
        public string CollectionID;

        /// <summary>
        /// The repository identifier
        /// </summary>
        public string RepositoryID;

        /// <summary>
        /// The date
        /// </summary>
        public string Date;

        /// <summary>
        /// The call number
        /// </summary>
        public string CallNumber;

        /// <summary>
        /// The interviewees identifier
        /// </summary>
        public string IntervieweesID;

        /// <summary>
        /// The interviewers identifier
        /// </summary>
        public string InterviewersID;

        /// <summary>
        /// The keywords
        /// </summary>
        public string Keywords;

        /// <summary>
        /// The loc subjects
        /// </summary>
        public string LocSubjects;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(InterviewApiModel other)
        {
            return Title.Equals(other.Title);
        }
    }
}
