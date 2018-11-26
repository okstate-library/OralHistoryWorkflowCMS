using MaterialDesignThemes.Wpf;
using Model;
using Model.Transfer;
using Model.Transfer.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp.Helper;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Browse.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Markup.IStyleConnector" />
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Browse : UserControl
    {
        #region Private properties

        /// <summary>
        /// Gets or sets the transcription search model.
        /// </summary>
        /// <value>
        /// The transcription search model.
        /// </value>
        private TranscriptionSearchModel TranscriptionSearchModel { get; set; }

        /// <summary>
        /// Gets or sets the search list.
        /// </summary>
        /// <value>
        /// The search list.
        /// </value>
        private List<string> SearchList { get; set; }

        /// <summary>
        /// Gets the search request.
        /// </summary>
        /// <value>
        /// The search request.
        /// </value>
        public SearchRequest SearchRequest { get; private set; }

        /// <summary>
        /// Gets or sets the next page number.
        /// </summary>
        /// <value>
        /// The next page number.
        /// </value>
        public int NextPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        /// <value>
        /// The current page number.
        /// </value>
        public int CurrentPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the previous page number.
        /// </summary>
        /// <value>
        /// The previous page number.
        /// </value>
        public int PreviousPageNumber { get; set; }

        /// <summary>
        /// The last header clicked
        /// </summary>
        GridViewColumnHeader _lastHeaderClicked = null;

        /// <summary>
        /// The last direction
        /// </summary>
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        /// <summary>
        /// Gets a value indicating whether this instance is page initialize.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is page initialize; otherwise, <c>false</c>.
        /// </value>
        public bool IsPageInitialize { get; private set; } = false;

        /// <summary>
        /// Gets or sets the current identifier list.
        /// </summary>
        /// <value>
        /// The current identifier list.
        /// </value>
        public List<int> CurrentIdList { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Browse" /> class.
        /// </summary>
        public Browse()
        {
            InitializeComponent();

            InitializeSearchOption();

            Loaded += BrowseUserControl_Loaded;

            this.Loaded += (s, args) =>
            {
                PageComboBox.Loaded += PageLengthComboBox_Loaded;

                PageComboBox.SelectionChanged +=
                        new SelectionChangedEventHandler(PageLengthComboBox_SelectionChanged);
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Browse"/> class.
        /// </summary>
        /// <param name="searchWord">The search word.</param>
        public Browse(string searchWord)
        {
            InitializeComponent();

            InitializeSearchOption();

            SearchWordTextBox.Text = searchWord;

            PopulateList();

            Loaded += BrowseUserControl_Loaded;

        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the BrowseUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BrowseUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Visible;

            cc.Content = null;

            this.SearchRequest = new SearchRequest(SearchHelper.InitialCurrentPage,
              int.Parse(SearchHelper.PageSizeList[0]));

            PopulateIntializeView();

            PopulateList();

            BackToListButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the TranscriptionQueueListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        void TranscriptionQueueListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement clicked = e.OriginalSource as UIElement;

            string name = ((FrameworkElement)clicked).Name;

            if (clicked != null && !string.IsNullOrEmpty(name))
            {
                string[] words = name.Split('_');

                if (words.Length > 1)
                {
                    BackToListButton.Visibility = Visibility.Visible;

                    WellKnownExpander expader = (WellKnownExpander)Enum.Parse(typeof(WellKnownExpander), words[1]);

                    TranscriptionModel itemTranscriptionModel = ((FrameworkElement)e.OriginalSource).DataContext as TranscriptionModel;

                    MainGrid.Visibility = Visibility.Hidden;

                    cc.Content = new Transcription(itemTranscriptionModel.Id, expader);

                }
            }
        }

        /// <summary>
        /// Handles the Click event of the PageClickButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void PageClickButton_Click(object sender, EventArgs e)
        {
            Hyperlink hyperlink = sender as Hyperlink;

            string hyperlinkName = hyperlink.Name as string;

            int requestPage = 0;

            if (hyperlinkName == "NextHyperlink")
            {
                requestPage = NextPageNumber;
            }
            else if (hyperlinkName == "PrevoiousHyperlink")
            {
                requestPage = PreviousPageNumber;
            }

            this.SearchRequest.CurrentPage = requestPage;

            PopulateList();
        }

        /// <summary>
        /// Handles the Click event of the BackToListButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void BackToListButton_Click(object sender, EventArgs e)
        {
            cc.Content = null;

            MainGrid.Visibility = Visibility.Visible;

            BackToListButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Handles the Click event of the ResetButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            InitializeSearchOption();

            PopulateList();

            ResetOptions();
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            PopulateList();
        }

        /// <summary>
        /// Handles the Process event of the CheckBoxSelection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void CheckBoxSelection_Process(object sender, RoutedEventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;

            string key = chkBox.Tag.ToString();

            if (chkBox.Name.Equals("CheckedBoxCollection"))
            {
                if ((bool)chkBox.IsChecked)
                {
                    TranscriptionSearchModel.CollectionNames.Add(key);
                }
                else
                {
                    TranscriptionSearchModel.CollectionNames.Remove(key);
                }
            }
            else if (chkBox.Name.Equals("CheckedBoxInterviewer"))
            {
                if ((bool)chkBox.IsChecked)
                {
                    TranscriptionSearchModel.Interviewers.Add(key);
                }
                else
                {
                    TranscriptionSearchModel.Interviewers.Remove(key);
                }
            }
            else if (chkBox.Name.Equals("CheckedBoxSubject"))
            {
                if ((bool)chkBox.IsChecked)
                {
                    TranscriptionSearchModel.Subjects.Add(key);
                }
                else
                {
                    TranscriptionSearchModel.Subjects.Remove(key);
                }
            }
            else if (chkBox.Name.Equals("CheckedBoxContentDM"))
            {
                if ((bool)chkBox.IsChecked)
                {
                    TranscriptionSearchModel.Contentdms.Add(key);
                }
                else
                {
                    TranscriptionSearchModel.Contentdms.Remove(key);
                }
            }

            PopulateList();
        }

        /// <summary>
        /// Handles the Loaded event of the PageLengthComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void PageLengthComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = SearchHelper.PageSizeList;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the PageLengthComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void PageLengthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsPageInitialize)
            {
                var comboBox = (ComboBox)sender;

                // ... Set SelectedItem as Window Title.
                string value = comboBox.SelectedItem as string;

                this.SearchRequest.ListLength = int.Parse(value);

                this.SearchRequest.CurrentPage = 1;
                NextPageNumber = 0;
                PreviousPageNumber = 0;
                CurrentPageTextBox.Text = string.Empty;

                PopulateList();
            }
        }

        /// <summary>
        /// Grids the view column header clicked handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);


                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header  
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ExportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExportButton_Click(object sender, RoutedEventArgs e)
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

            foreach (int transcriptionId in CurrentIdList)
            {
                RequestModel requestModel = new RequestModel()
                {
                    TranscriptionId = transcriptionId,
                };

                ResponseModel response = App.BaseUserControl.InternalService.GetTranscription(requestModel);

                TranscriptionModel transcriptionModel = response.Transcription;

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

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the search option.
        /// </summary>
        private void InitializeSearchOption()
        {
            SearchWordTextBox.Text = string.Empty;

            SearchList = new List<string>();

            this.TranscriptionSearchModel = new TranscriptionSearchModel();
            this.TranscriptionSearchModel.Contentdms = new List<string>();
            this.TranscriptionSearchModel.CollectionNames = new List<string>();
            this.TranscriptionSearchModel.Interviewers = new List<string>();
            this.TranscriptionSearchModel.Subjects = new List<string>();
        }

        /// <summary>
        /// Sorts the specified sort by.
        /// </summary>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(TranscriptionQueueListView.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        /// <summary>
        /// Populates the intialize view.
        /// </summary>
        private void PopulateIntializeView()
        {
            ResponseModel response = App.BaseUserControl.InternalService.InitializeBrowseForm();

            CollectionListBox.ItemsSource = response.BrowseFormModel.CollectionList;

            InterviewerListBox.ItemsSource = response.BrowseFormModel.InterviewerList;

            SubjectListBox.ItemsSource = response.BrowseFormModel.SubjectList;

            ContentDMListBox.ItemsSource = response.BrowseFormModel.ContentDmList;
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
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }

        }

        /// <summary>
        /// Populatrs the list.
        /// </summary>
        private void PopulateList()
        {
            IsPageInitialize = true;

            RequestModel requestModel = new RequestModel()
            {
                SearchRequest = this.SearchRequest,
                TranscriptionSearchModel = this.TranscriptionSearchModel,
                SearchWord = SearchWordTextBox.Text.Trim()
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetTranscriptionsForBrowse(requestModel);

            if (response.IsOperationSuccess)
            {
                TranscriptionQueueListView.ItemsSource = response.Transcriptions;

                response.SetTranscriptionIds();

                CurrentIdList = response.TranscriptionIds;

                RecordCountWordTextBox.Text = SearchHelper.GetRecordCountText(response.Transcriptions.Count
                  , response.PaginationInfo.TotalListLength);

                SetPagination(response.PaginationInfo);
            }
            else
            {

            }

        }

        /// <summary>
        /// Sets the pagination.
        /// </summary>
        /// <param name="paginationInfo">The pagination information.</param>
        private void SetPagination(PaginationInfo paginationInfo)
        {

            NextTextBlock.Visibility = Visibility.Hidden;
            PrevoiousTextBlock.Visibility = Visibility.Hidden;

            if (paginationInfo.CurrentPage == 1 && paginationInfo.TotalPages > paginationInfo.CurrentPage)
            {
                NextTextBlock.Visibility = Visibility.Visible;
                NextPageNumber = paginationInfo.CurrentPage + 1;
            }
            else if (paginationInfo.CurrentPage > 1 && paginationInfo.CurrentPage < paginationInfo.TotalPages)
            {
                NextTextBlock.Visibility = Visibility.Visible;
                PrevoiousTextBlock.Visibility = Visibility.Visible;
                NextPageNumber = paginationInfo.CurrentPage + 1;
                PreviousPageNumber = paginationInfo.CurrentPage - 1;
            }
            else if (paginationInfo.TotalPages == paginationInfo.CurrentPage)
            {
                PrevoiousTextBlock.Visibility = Visibility.Visible;
                PreviousPageNumber = paginationInfo.CurrentPage - 1;
            }

            CurrentPageTextBox.Text = paginationInfo.CurrentPage + " Page";
        }

        /// <summary>
        /// Resets the options.
        /// </summary>
        private void ResetOptions()
        {
            //ResetLists(CollectionListBox);

            //ResetLists(InterviewerListBox);

            //ResetLists(InterviewerListBox);

            //ResetLists(InterviewerListBox);
        }

        /// <summary>
        /// Resets the lists.
        /// </summary>
        /// <param name="listbox">The listbox.</param>
        private void ResetLists(ListBox listbox)
        {
            foreach (ListViewItem item in listbox.Items)
            {
                item.IsSelected = false;
            }

        }

        #endregion              
    }
}
