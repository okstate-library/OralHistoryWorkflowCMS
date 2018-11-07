using Core.Enums;
using Model;
using Model.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
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

        #region private properties

        public bool PageInitialize { get; set; }

        /// <summary>
        /// Gets or sets the search list.
        /// </summary>
        /// <value>
        /// The search list.
        /// </value>
        private List<string> SearchList { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TranscriptionQueue" /> class.
        /// </summary>
        public TranscriptionQueue()
        {
            InitializeComponent();

            //PopulatrList();

            Loaded += TranscriptionQueueUserControl_Loaded;
        }

        /// <summary>
        /// Handles the Loaded event of the MyWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TranscriptionQueueUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Visible;

            cc.Content = null;

            BackToListButton.Visibility = Visibility.Hidden;

            AllToggleButton.IsChecked = true;

            SearchList = new List<string>();

            PopulatrList();

            PageInitialize = true;

            //AllToggleButton.Checked += CheckBoxChanged;
            //AllToggleButton.Unchecked += CheckBoxChanged;
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

            PopulatrList();
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            PopulatrList();
        }

        /// <summary>
        /// Handles the Click event of the ToggleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = ((System.Windows.FrameworkElement)sender);

            string toggleButtonName = ((System.Windows.FrameworkElement)sender).Name;

            string selectOption = toggleButtonName.Replace("ToggleButton", "");

            WellKnownTranscriptionQueueOption option = (WellKnownTranscriptionQueueOption)Enum.Parse(typeof(WellKnownTranscriptionQueueOption), selectOption, true);

            string selectedOption = option.ToString();

            ManageToggleButtons(option);

            if ((bool)(sender as ToggleButton).IsChecked)
            {
                SearchList.Add(selectedOption);
            }
            else
            {
                if (SearchList.Contains(selectedOption))
                {
                    SearchList.Remove(selectedOption);
                }
            }

            PopulatrList();
        }

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

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            SearchList.Add(WellKnownTranscriptionQueueOption.All.ToString());

            PopulatrList();
        }

        /// <summary>
        /// Populatrs the list.
        /// </summary>
        private void PopulatrList()
        {
            RequestModel requestModel = new RequestModel()
            {
                IsTranscriptionQueue = true,
                FilterKeyWords = SearchList,
                SearchWord = SearchWordTextBox.Text.Trim(),
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetTranscriptions(requestModel);

            if (response.IsOperationSuccess)
            {
                TranscriptionQueueListView.ItemsSource = response.Transcriptions;

                RecordCountWordTextBox.Text = response.Transcriptions.Count() + " record(s)";
            }

        }

    }

}