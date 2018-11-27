﻿using Core.Enums;
using Model;
using Model.Transfer;
using Model.Transfer.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp.Helper;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TranscriptionQueue.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class TranscriptionQueue : UserControl
    {
        #region Private properties

        /// <summary>
        /// Gets or sets a value indicating whether [page initialize].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [page initialize]; otherwise, <c>false</c>.
        /// </value>
        public bool PageInitialize { get; set; }

        /// <summary>
        /// Gets or sets the search list.
        /// </summary>
        /// <value>
        /// The search list.
        /// </value>
        private List<string> SearchList { get; set; }

        /// <summary>
        /// Gets or sets the search request dto.
        /// </summary>
        /// <value>
        /// The search request dto.
        /// </value>
        public SearchRequest SearchRequest { get; set; }

        /// <summary>
        /// The last header clicked
        /// </summary>
        GridViewColumnHeader _lastHeaderClicked = null;

        /// <summary>
        /// The last direction
        /// </summary>
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

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
        /// Gets or sets the current page list.
        /// </summary>
        /// <value>
        /// The current page list.
        /// </value>
        public int CurrentPageList { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TranscriptionQueue" /> class.
        /// </summary>
        public TranscriptionQueue()
        {
            InitializeComponent();

            //PopulateList();

            Loaded += TranscriptionQueueUserControl_Loaded;

            this.Loaded += (s, args) =>
            {
                PageComboBox.Loaded += PageLengthComboBox_Loaded;

                PageComboBox.SelectionChanged +=
                        new SelectionChangedEventHandler(PageLengthComboBox_SelectionChanged);
            };
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the MyWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void TranscriptionQueueUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Visible;

            cc.Content = null;

            BackToListButton.Visibility = Visibility.Hidden;

            AllToggleButton.IsChecked = true;

            SearchList = new List<string>();

            this.SearchRequest = new SearchRequest(SearchHelper.InitialCurrentPage,
                  SearchHelper.InitialListLength);

            this.CurrentPageList = SearchHelper.InitialListLength;

            PopulateList();

            PageInitialize = true;
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
            SearchWordTextBox.Text = string.Empty;

            PopulateList();
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
        /// Handles the Click event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = ((FrameworkElement)sender);

            string toggleButtonName = ((FrameworkElement)sender).Name;

            string selectOption = toggleButtonName.Replace("ToggleButton", "");

            WellKnownTranscriptionQueueOption option = (WellKnownTranscriptionQueueOption)Enum.Parse(typeof(WellKnownTranscriptionQueueOption), selectOption, true);

            string selectedOption = option.ToString();

            ManageToggleButtons(option);

            if ((bool)(sender as ToggleButton).IsChecked)
            {
                if (selectedOption.Equals("All"))
                {
                    SearchList = new List<string>();
                }

                SearchList.Add(selectedOption);

                this.SearchRequest = new SearchRequest(SearchHelper.InitialCurrentPage, this.CurrentPageList);
            }
            else
            {
                if (SearchList.Contains(selectedOption))
                {
                    SearchList.Remove(selectedOption);
                }
                
              
            }
            
            PopulateList();
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
        /// Manages the toggle buttons.
        /// </summary>
        /// <param name="option">The option.</param>
        private void ManageToggleButtons(WellKnownTranscriptionQueueOption option)
        {
            if (option == WellKnownTranscriptionQueueOption.All)
            {
                PriorityToggleButton.IsChecked = false;
                TranscribedToggleButton.IsChecked = false;
                AuditCheckToggleButton.IsChecked = false;
                FirstEditToggleButton.IsChecked = false;
                SecondEditToggleButton.IsChecked = false;
                DraftSentToggleButton.IsChecked = false;
                CorrectionsToggleButton.IsChecked = false;
                FinalEditToggleButton.IsChecked = false;
                SentOutToggleButton.IsChecked = false;
            }
            else
            {
                if ((bool)AllToggleButton.IsChecked)
                {
                    AllToggleButton.IsChecked = false;
                }
                else if (!(bool)PriorityToggleButton.IsChecked &&
                            !(bool)TranscribedToggleButton.IsChecked &&
                            !(bool)AuditCheckToggleButton.IsChecked &&
                            !(bool)FirstEditToggleButton.IsChecked &&
                           !(bool)SecondEditToggleButton.IsChecked &&
                           !(bool)DraftSentToggleButton.IsChecked &&
                            !(bool)CorrectionsToggleButton.IsChecked &&
                            !(bool)FinalEditToggleButton.IsChecked &&
                            !(bool)SentOutToggleButton.IsChecked)
                {
                    AllToggleButton.IsChecked = true;

                    SearchList = new List<string>();
                }
            }
        }

        /// <summary>
        /// Handles the Loaded event of the PageLengthComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
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
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void PageLengthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsPageInitialize)
            {
                var comboBox = (ComboBox)sender;

                // ... Set SelectedItem as Window Title.
                string value = comboBox.SelectedItem as string;

                int pageList = int.Parse(value);
                this.SearchRequest.ListLength = pageList;
                this.CurrentPageList = pageList;

                this.SearchRequest.CurrentPage = 1;
                NextPageNumber = 0;
                PreviousPageNumber = 0;
                CurrentPageTextBox.Text = string.Empty;

                PopulateList();
            }
            else
            {
                IsPageInitialize = true;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Populatrs the list.
        /// </summary>
        private void PopulateList()
        {
            RequestModel requestModel = new RequestModel()
            {
                FilterKeyWords = SearchList,
                SearchWord = SearchWordTextBox.Text.Trim(),
                SearchRequest = this.SearchRequest,
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetTranscriptions(requestModel);

            if (response.IsOperationSuccess)
            {
                TranscriptionQueueListView.ItemsSource = response.Transcriptions;

                SetPagination(response.PaginationInfo);
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
            else if (paginationInfo.TotalPages == paginationInfo.CurrentPage && paginationInfo.CurrentPage != 1)
            {
                PrevoiousTextBlock.Visibility = Visibility.Visible;
                PreviousPageNumber = paginationInfo.CurrentPage - 1;
            }
           
            CurrentPageTextBox.Text = paginationInfo.CurrentPage + " Page";

            RecordCountWordTextBox.Text = SearchHelper.GetRecordCountText(paginationInfo.ListLength ,
                paginationInfo.CurrentPage, paginationInfo.TotalListLength);
        }

        /// <summary>
        /// Grids the view column header clicked handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
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

        #endregion
    }
}