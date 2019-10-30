using MaterialDesignThemes.Wpf;
using Model;
using Model.Transfer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Helper;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Setting : UserControl
    {
        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the selected settings.
        /// </summary>
        /// <value>
        /// The selected settings.
        /// </value>
        public WellKnownSettings SelectedSettings { get; set; }

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting" /> class.
        /// </summary>
        public Setting()
        {
            InitializeComponent();

            PopulateUserList(true);

            Loaded += SettingsUserControl_Loaded;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the SettingsUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SettingsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Visible;

            cc.Content = null;

            PopulateUserList(true);

            BackToListButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Handles the OnDialogClosing event of the Confirmaiton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="DialogClosingEventArgs" /> instance containing the event data.</param>
        private void Confirmaiton_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter.Equals("true"))
            {
                ClearDatabase();
            }
        }

        private void ExportAllConfirmaiton_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter.Equals("true"))
            {
                ExportAll();
            }
        }

        private void ExportAllCollectionConfirmaiton_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter.Equals("true"))
            {
                ExportAllCollection();
            }
        }

        private void ExportAllSubseriesConfirmaiton_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter.Equals("true"))
            {
                ExportAllSubseries();
            }
        }


        /// <summary>
        /// Handles the Click event of the Users control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Users_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Users;

            SetVisibility(Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed,
                Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed);

            PopulateUserList(true);
        }

        private void Repository_Click(object sender, RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Repository;

            SetVisibility(Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed);

            PopulateRepositoryList(true);
        }

        /// <summary>
        /// Handles the Click event of the Collection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Collection_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Collection;

            SetVisibility(Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed);

            PopulateCollectionList(true);
        }

        /// <summary>
        /// Handles the Click event of the Subseries control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Subseries_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Subseries;

            SetVisibility(Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed);

            PopulateSubseriesList(true);
        }

        /// <summary>
        /// Handles the Click event of the ExportAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ExportAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.ExportAll;

            SetVisibility(Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed);
        }

        /// <summary>
        /// Handles the Click event of the Reset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void Reset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Reset;

            SetVisibility(Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible);
        }

        /// <summary>
        /// Handles the Click event of the ResetButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            //SearchWordTextBox.Text = string.Empty;

            //PopulateList(false);
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            // PopulateList(false);
        }

        /// <summary>
        /// Handles the Click event of the AddNewUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Users;

            BackToListButton.Visibility = Visibility.Visible;

            MainGrid.Visibility = Visibility.Hidden;

            cc.Content = new ModifyUser(0);
        }

        private void AddRepository_Click(object sender, RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Repository;

            BackToListButton.Visibility = Visibility.Visible;

            MainGrid.Visibility = Visibility.Hidden;

            cc.Content = new ModifyRepository(0);
        }

        /// <summary>
        /// Handles the Click event of the AddCollection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddCollection_Click(object sender, RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Collection;

            BackToListButton.Visibility = Visibility.Visible;

            MainGrid.Visibility = Visibility.Hidden;

            cc.Content = new ModifyCollection(0);
        }

        /// <summary>
        /// Handles the Click event of the AddSubseries control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddSubseries_Click(object sender, RoutedEventArgs e)
        {
            SelectedSettings = WellKnownSettings.Subseries;

            BackToListButton.Visibility = Visibility.Visible;

            MainGrid.Visibility = Visibility.Hidden;

            cc.Content = new ModifySubseries(0);
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

            switch (SelectedSettings)
            {
                case WellKnownSettings.Users:
                    PopulateUserList(true);
                    break;
                case WellKnownSettings.Repository:
                    PopulateRepositoryList(true);
                    break;                  
                case WellKnownSettings.Collection:
                    PopulateCollectionList(true);
                    break;
                case WellKnownSettings.Subseries:
                    App.BaseUserControl.InitializeComponent(false);
                    PopulateSubseriesList(true);
                    break;
                case WellKnownSettings.None:
                case WellKnownSettings.ExportAll:
                case WellKnownSettings.Reset:
                    break;
                default:
                    break;
            }

            SelectedSettings = WellKnownSettings.None;

            BackToListButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the UsersListViewListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
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

                    cc.Content = new ModifyUser(userModel.UserId);
                }

            }
        }

        void RepositoryListViewListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement clicked = e.OriginalSource as UIElement;

            string name = ((FrameworkElement)clicked).Name;

            if (clicked != null && !string.IsNullOrEmpty(name))
            {
                string[] words = name.Split('_');

                if (words.Length > 1)
                {
                    BackToListButton.Visibility = Visibility.Visible;

                    RepositoryModel repositoryModel = ((FrameworkElement)e.OriginalSource).DataContext as RepositoryModel;

                    MainGrid.Visibility = Visibility.Hidden;

                    cc.Content = new ModifyRepository(repositoryModel.Id);
                }

            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the CollectionListViewListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        void CollectionListViewListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement clicked = e.OriginalSource as UIElement;

            string name = ((FrameworkElement)clicked).Name;

            if (clicked != null && !string.IsNullOrEmpty(name))
            {
                string[] words = name.Split('_');

                if (words.Length > 1)
                {
                    BackToListButton.Visibility = Visibility.Visible;

                    CollectionModel collectionModel = ((FrameworkElement)e.OriginalSource).DataContext as CollectionModel;

                    MainGrid.Visibility = Visibility.Hidden;

                    cc.Content = new ModifyCollection(collectionModel.Id);
                }

            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the SubseriesListViewListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        void SubseriesListViewListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement clicked = e.OriginalSource as UIElement;

            string name = ((FrameworkElement)clicked).Name;

            if (clicked != null && !string.IsNullOrEmpty(name))
            {
                string[] words = name.Split('_');

                if (words.Length > 1)
                {
                    BackToListButton.Visibility = Visibility.Visible;

                    SubseryModel subseryModel = ((FrameworkElement)e.OriginalSource).DataContext as SubseryModel;

                    MainGrid.Visibility = Visibility.Hidden;

                    cc.Content = new ModifySubseries(subseryModel.Id);
                }

            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clears the database.
        /// </summary>
        private void ClearDatabase()
        {
            ResponseModel response = App.BaseUserControl.InternalService.ResetDatabase(null);

            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, "You successfully deleted \n records from database.");
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        /// <summary>
        /// Exports all.
        /// </summary>
        private void ExportAll()
        {
            ResponseModel response = App.BaseUserControl.InternalService.ExportAll(null);

            ExportHelper helper = new ExportHelper();

            helper.ExportAll(response.Transcriptions);

            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, "You successfully exports \n records from database.");
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        private void ExportAllCollection()
        {
            ResponseModel response = App.BaseUserControl.InternalService.ExportAllCollection(null);

            ExportHelper helper = new ExportHelper();

            helper.ExportAllCollection(response.Collections);

            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, "You successfully exports \n records from database.");
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        private void ExportAllSubseries()
        {
            ResponseModel response = App.BaseUserControl.InternalService.ExportAllSubseries(null);

            ExportHelper helper = new ExportHelper();

            helper.ExportAllSubseries(response.Subseries);

            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, "You successfully exports \n records from database.");
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="subseries">The subseries.</param>
        /// <param name="exportAll">The export all.</param>
        /// <param name="reset">The reset.</param>
        private void SetVisibility(Visibility users, Visibility repository,
            Visibility collection, Visibility subseries,
            Visibility exportAll, Visibility reset)
        {
            UsersStackPanel.Visibility = users;
            RepositoryStackPanel.Visibility = repository;
            SubseriesStackPanel.Visibility = subseries;
            CollectionStackPanel.Visibility = collection;
            ResetStackPanel.Visibility = reset;
            ExportAllStackPanel.Visibility = exportAll;

            RecordCountBorder.Visibility = (SelectedSettings == WellKnownSettings.ExportAll || SelectedSettings == WellKnownSettings.Reset ?
                Visibility.Collapsed : Visibility.Visible);
        }

        /// <summary>
        /// Populatrs the list.
        /// </summary>
        /// <param name="isInitialTime">if set to <c>true</c> [is initial time].</param>
        private void PopulateUserList(bool isInitialTime)
        {
            RequestModel requestModel = new RequestModel()
            {
                //SearchWord = SearchWordTextBox.Text.Trim(),
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetUsers(requestModel);

            if (response.IsOperationSuccess)
            {
                UsersListView.ItemsSource = response.Users;

                RecordCountWordTextBox.Text = response.Users.Count + " user record(s)";
            }

        }

        /// <summary>
        /// Populates the collection list.
        /// </summary>
        /// <param name="isInitialTime">if set to <c>true</c> [is initial time].</param>
        private void PopulateRepositoryList(bool isInitialTime)
        {
            RequestModel requestModel = new RequestModel()
            {
                //FilterKeyWords = SearchList,
                //SearchWord = SearchWordTextBox.Text.Trim(),
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetRepository(requestModel);

            if (response.IsOperationSuccess)
            {
                RepositoryListView.ItemsSource = response.Repositories;

                RecordCountWordTextBox.Text = response.Repositories.Count + " repository record(s)";

            }

        }

        /// <summary>
        /// Populates the collection list.
        /// </summary>
        /// <param name="isInitialTime">if set to <c>true</c> [is initial time].</param>
        private void PopulateCollectionList(bool isInitialTime)
        {
            RequestModel requestModel = new RequestModel()
            {
                //FilterKeyWords = SearchList,
                //SearchWord = SearchWordTextBox.Text.Trim(),
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetCollection(requestModel);

            if (response.IsOperationSuccess)
            {
                CollectionListView.ItemsSource = response.Collections;

                RecordCountWordTextBox.Text = response.Collections.Count + " collection record(s)";

            }

        }

        /// <summary>
        /// Populates the subseries list.
        /// </summary>
        /// <param name="isInitialTime">if set to <c>true</c> [is initial time].</param>
        private void PopulateSubseriesList(bool isInitialTime)
        {
            RequestModel requestModel = new RequestModel()
            {
                //SearchWord = SearchWordTextBox.Text.Trim(),
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetSubseies(requestModel);

            if (response.IsOperationSuccess)
            {
                SubseriesListView.ItemsSource = response.Subseries;

                RecordCountWordTextBox.Text = response.Subseries.Count + " subseries record(s)";

            }
        }

        #endregion
    }
}
