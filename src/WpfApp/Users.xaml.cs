using Core.Enums;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Users : UserControl
    {
        #region private properties

        /// <summary>
        /// Gets or sets the search list.
        /// </summary>
        /// <value>
        /// The search list.
        /// </value>
        private List<string> SearchList { get; set; }

        /// <summary>
        /// The last header clicked
        /// </summary>
        GridViewColumnHeader _lastHeaderClicked = null;

        /// <summary>
        /// The last direction
        /// </summary>
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TranscriptionQueue" /> class.
        /// </summary>
        public Users()
        {
            InitializeComponent();

            SearchList = new List<string>();

            //PopulateList(true);

            Loaded += TranscriptionQueueUserControl_Loaded;
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

            PopulateList(true);

            BackToListButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Sorts the specified sort by.
        /// </summary>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="direction">The direction.</param>
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(UsersListView.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the TranscriptionQueueListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        void UsersListViewListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement clicked = e.OriginalSource as UIElement;

            string name = ((FrameworkElement)clicked).Name;

            if (clicked != null && !string.IsNullOrEmpty(name))
            {
                string[] words = name.Split('_');

                if (words.Length > 1)
                {
                    BackToListButton.Visibility = Visibility.Visible;

                    UserModel userModel = ((FrameworkElement)e.OriginalSource).DataContext as UserModel;

                    MainGrid.Visibility = Visibility.Hidden;

                    cc.Content = new User(userModel.UserId);
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

            PopulateList(false);
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            PopulateList(false);
        }

        /// <summary>
        /// Handles the Click event of the AddNewUserButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            BackToListButton.Visibility = Visibility.Visible;

            UserModel userModel = ((FrameworkElement)e.OriginalSource).DataContext as UserModel;

            MainGrid.Visibility = Visibility.Hidden;

            cc.Content = new User(0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populatrs the list.
        /// </summary>
        /// <param name="isInitialTime">if set to <c>true</c> [is initial time].</param>
        private void PopulateList(bool isInitialTime)
        {
            RequestModel requestModel = new RequestModel()
            {
                FilterKeyWords = SearchList,
                SearchWord = SearchWordTextBox.Text.Trim(),
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetUsers(requestModel);

            if (response.IsOperationSuccess)
            {
                UsersListView.ItemsSource = response.Users;

                RecordCountWordTextBox.Text = response.Users.Count() + " record(s)";

                if (isInitialTime)
                {
                    NumberUsersTextBlock.Text = response.Users.Count() + " user(s)";
                    AdminUsersTextBlock.Text = response.Users.Where(a => a.UserType == (byte)WellKnownUserType.AdminUser).Count() + " admin user(s)";
                }
            }

        }

        /// <summary>
        /// Grids the view column header clicked handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
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

        #endregion
    }
}
