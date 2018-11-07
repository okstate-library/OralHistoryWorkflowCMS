using Model;
using Model.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp.Helper;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Browse.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Browse : UserControl
    {
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
        /// Initializes a new instance of the <see cref="Browse"/> class.
        /// </summary>
        public Browse()
        {
            InitializeComponent();

            SearchList = new List<string>();

            this.TranscriptionSearchModel = new TranscriptionSearchModel();
            this.TranscriptionSearchModel.Contentdms = new List<string>();
            this.TranscriptionSearchModel.CollectionNames = new List<string>();
            this.TranscriptionSearchModel.Interviewers = new List<string>();
            this.TranscriptionSearchModel.Subjects = new List<string>();

            //PopulateList();

            Loaded += BrowseUserControl_Loaded;
        }

        public Browse(string searchWord)
        {
            InitializeComponent();

            SearchList = new List<string>();

            this.TranscriptionSearchModel = new TranscriptionSearchModel();
            this.TranscriptionSearchModel.Contentdms = new List<string>();
            this.TranscriptionSearchModel.CollectionNames = new List<string>();
            this.TranscriptionSearchModel.Interviewers = new List<string>();
            this.TranscriptionSearchModel.Subjects = new List<string>();

            SearchWordTextBox.Text = searchWord;

            PopulateList();

            Loaded += BrowseUserControl_Loaded;

        }

        /// <summary>
        /// Handles the Loaded event of the BrowseUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BrowseUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Visible;

            cc.Content = null;

            PopulateIntializeView();

            //PopulateList();

            BackToListButton.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Populates the intialize view.
        /// </summary>
        private void PopulateIntializeView()
        {
            ResponseModel response = App.BaseUserControl.InternalService.InitializeBrowseForm();

            checkedListCollections.ItemsSource = response.BrowseFormModel.CollectionList;

            checkedListInterviewer.ItemsSource = response.BrowseFormModel.InterviewerList;

            checkedListSubject.ItemsSource = response.BrowseFormModel.SubjectList;

            checkedListContentDM.ItemsSource = response.BrowseFormModel.ContentDmList;
        }

        /// <summary>
        /// The last header clicked
        /// </summary>
        GridViewColumnHeader _lastHeaderClicked = null;

        /// <summary>
        /// The last direction
        /// </summary>
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        /// <summary>
        /// Grids the view column header clicked handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        void GridViewColumnHeaderClickedHandler(object sender,
                                                RoutedEventArgs e)
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
        /// Handles the MouseDoubleClick event of the TranscriptionQueueListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the BackToListButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            SearchWordTextBox.Text = string.Empty;

            PopulateList();
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            PopulateList();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the checkedListCollections control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void checkedListCollections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (KeyValuePair<string, string> item in e.RemovedItems)
            {
                TranscriptionSearchModel.CollectionNames.Remove(item.Value);
            }

            foreach (KeyValuePair<string, string> item in e.AddedItems)
            {
                TranscriptionSearchModel.CollectionNames.Add(item.Value);
            }

            PopulateList();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the checkedListInterviewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void checkedListInterviewer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (KeyValuePair<string, string> item in e.RemovedItems)
            {
                TranscriptionSearchModel.Interviewers.Remove(item.Value);
            }

            foreach (KeyValuePair<string, string> item in e.AddedItems)
            {
                TranscriptionSearchModel.Interviewers.Add(item.Value);
            }

            PopulateList();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the checkedListSubject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void checkedListSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (KeyValuePair<string, string> item in e.RemovedItems)
            {
                TranscriptionSearchModel.Subjects.Remove(item.Value);
            }

            foreach (KeyValuePair<string, string> item in e.AddedItems)
            {
                TranscriptionSearchModel.Subjects.Add(item.Value);
            }

            PopulateList();
        }


        /// <summary>
        /// Handles the SelectionChanged event of the checkedListContentDm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void checkedListContentDm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (KeyValuePair<string, string> item in e.RemovedItems)
            {
                TranscriptionSearchModel.Contentdms.Remove(item.Value);
            }

            foreach (KeyValuePair<string, string> item in e.AddedItems)
            {
                TranscriptionSearchModel.Contentdms.Add(item.Value);
            }

            PopulateList();
        }

        /// <summary>
        /// Populatrs the list.
        /// </summary>
        private void PopulateList()
        {

            RequestModel requestModel = new RequestModel()
            {
                TranscriptionSearchModel = this.TranscriptionSearchModel,
                IsTranscriptionQueue = false,
                SearchWord = SearchWordTextBox.Text.Trim()
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetTranscriptionsForBrowse(requestModel);

            if (response.IsOperationSuccess)
            {
                TranscriptionQueueListView.ItemsSource = response.Transcriptions;
                RecordCountWordTextBox.Text = response.Transcriptions.Count() + " record(s)";// StringHelper.ResultString;
            }
            else
            {

            }

        }

    }

}
