using Core.Enums;
using Model;
using Model.Transfer;
using Model.Transfer.Search;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp.Helper;

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

        /// <summary>
        /// Gets or sets the current page list.
        /// </summary>
        /// <value>
        /// The current page list.
        /// </value>
        public int CurrentPageList { get; set; }

        /// <summary>
        /// Gets the type of the current user.
        /// </summary>
        /// <value>
        /// The type of the current user.
        /// </value>
        public WellKnownUserType CurrentUserType
        {
            get
            {
                return (WellKnownUserType)App.BaseUserControl.UserModel.UserType;
            }
        }

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

            Loaded += (s, args) =>
            {
                PageComboBox.Loaded += PageLengthComboBox_Loaded;

                PageComboBox.SelectionChanged +=
                        new SelectionChangedEventHandler(PageLengthComboBox_SelectionChanged);
            };
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

            SearchRequest = new SearchRequest(SearchHelper.InitialCurrentPage,
                SearchHelper.InitialListLength);

            PopulateIntializeView();

            PopulateList();

            BackToListButton.Visibility = Visibility.Hidden;

            if (!string.IsNullOrEmpty(InterviewIdTextBox.Text))
            {
                int id = int.Parse(InterviewIdTextBox.Text);

                if (id > 0)
                {
                    LoadBrowseData(id, WellKnownExpander.Supplemental);
                }

                InterviewIdTextBox.Text = string.Empty;
            }

            ControlsVisibility();
        }
        
        /// <summary>
        /// Handles the MouseDoubleClick event of the TranscriptionQueueListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        void BrowseListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement clicked = e.OriginalSource as UIElement;

            string name = ((FrameworkElement)clicked).Name;

            if (clicked != null && !string.IsNullOrEmpty(name))
            {
                string[] words = name.Split('_');

                if (words.Length > 1)
                {
                    WellKnownExpander expader = (WellKnownExpander)Enum.Parse(typeof(WellKnownExpander), words[1]);

                    TranscriptionModel itemTranscriptionModel = ((FrameworkElement)e.OriginalSource).DataContext as TranscriptionModel;

                    LoadBrowseData(itemTranscriptionModel.Id, expader);
                }
            }
        }

        private void LoadBrowseData(int id, WellKnownExpander expader)
        {
            BackToListButton.Visibility = Visibility.Visible;

            MainGrid.Visibility = Visibility.Hidden;

            cc.Content = new Transcription(id, expader);
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

            SearchRequest.CurrentPage = requestPage;

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

            PopulateList();

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
            else if (chkBox.Name.Equals("CheckedBoxDarkArchive"))
            {
                if ((bool)chkBox.IsChecked)
                {
                    TranscriptionSearchModel.IsDarkArchived = true;
                }
                else
                {
                    TranscriptionSearchModel.IsDarkArchived = false;
                }
            }

            SearchRequest = new SearchRequest(SearchHelper.InitialCurrentPage, CurrentPageList);

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
            comboBox.SelectedIndex = SearchHelper.SelectedPageSizeIndex;
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

                int pageList = int.Parse(value);
                SearchRequest.ListLength = pageList;
                CurrentPageList = pageList;

                SearchRequest.CurrentPage = 1;
                NextPageNumber = 0;
                PreviousPageNumber = 0;
                CurrentPageTextBox.Text = string.Empty;

                PopulateList();
            }
        }

        /// <summary>
        /// Handles the Click event of the ExportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            List<TranscriptionModel> transcriptions = new List<TranscriptionModel>();

            foreach (int transcriptionId in CurrentIdList)
            {
                RequestModel requestModel = new RequestModel()
                {
                    TranscriptionId = transcriptionId,
                };

                ResponseModel response = App.BaseUserControl.InternalService.GetTranscription(requestModel);

                transcriptions.Add(response.Transcription);
            }

            ExportHelper helper = new ExportHelper();

            helper.Export(transcriptions);

        }

        #endregion

        #region Private methods

        private void ControlsVisibility()
        {
            switch (CurrentUserType)
            {
                case WellKnownUserType.GuestUser:
                case WellKnownUserType.Student:
                case WellKnownUserType.Staff:
                    DarkArchieveExpander.Visibility = Visibility.Collapsed;
                    break;
                case WellKnownUserType.AdminUser:
                    DarkArchieveExpander.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Initializes the search option.
        /// </summary>
        private void InitializeSearchOption()
        {
            SearchWordTextBox.Text = string.Empty;

            SearchList = new List<string>();

            TranscriptionSearchModel = new TranscriptionSearchModel
            {
                Contentdms = new List<string>(),
                CollectionNames = new List<string>(),
                Interviewers = new List<string>(),
                Subjects = new List<string>()
            };
        }

        /// <summary>
        /// Populates the intialize view.
        /// </summary>
        private void PopulateIntializeView()
        {
            RequestModel requestModel = new RequestModel()
            {
                IsAdminUser = (CurrentUserType == WellKnownUserType.AdminUser ? true : false),
            };

            ResponseModel response = App.BaseUserControl.InternalService.InitializeBrowseForm(requestModel);

            CollectionListBox.ItemsSource = response.BrowseFormModel.CollectionList;

            InterviewerListBox.ItemsSource = response.BrowseFormModel.InterviewerList;

            SubjectListBox.ItemsSource = response.BrowseFormModel.SubjectList;

            ContentDMListBox.ItemsSource = response.BrowseFormModel.ContentDmList;

            List<KeyValuePair<string, string>> restrictionList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Dark Archive", "Dark Archive"),
            };

            DarkArchiveListBox.ItemsSource = restrictionList;
        }

        /// <summary>
        /// Populatrs the list.
        /// </summary>
        private void PopulateList()
        {
            IsPageInitialize = true;

            RequestModel requestModel = new RequestModel()
            {
                IsAdminUser = (CurrentUserType == WellKnownUserType.AdminUser ? true : false),
                SearchRequest = SearchRequest,
                TranscriptionSearchModel = TranscriptionSearchModel,
                SearchWord = SearchWordTextBox.Text.Trim()
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetTranscriptionsForBrowse(requestModel);

            if (response.IsOperationSuccess)
            {
                BrowseListView.ItemsSource = response.Transcriptions;

                response.SetTranscriptionIds();

                CurrentIdList = response.TranscriptionIds;

                if (response.Transcriptions.Count == 0)
                {
                    SetZeroListMessage();
                }
                else
                {
                    SetPagination(response.PaginationInfo);
                }

            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

        }

        /// <summary>
        /// Sets the zero list message.
        /// </summary>
        private void SetZeroListMessage()
        {
            ExportButton.Visibility = Visibility.Hidden;

            CurrentPageTextBox.Visibility = Visibility.Hidden;

            RecordCountWordTextBox.Text = SearchHelper.NoRecordsFoundMessage;
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
            else if (paginationInfo.TotalPages == paginationInfo.CurrentPage && paginationInfo.CurrentPage != 1)
            {
                PrevoiousTextBlock.Visibility = Visibility.Visible;
                PreviousPageNumber = paginationInfo.CurrentPage - 1;
            }

            CurrentPageTextBox.Text = paginationInfo.CurrentPage + " Page";

            RecordCountWordTextBox.Text = SearchHelper.GetRecordCountText(paginationInfo.ListLength,
              paginationInfo.CurrentPage, paginationInfo.TotalListLength);

            CurrentPageTextBox.Visibility = Visibility.Visible;
            RecordCountWordTextBox.Visibility = Visibility.Visible;

            ExportButton.Visibility = Visibility.Visible;
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
