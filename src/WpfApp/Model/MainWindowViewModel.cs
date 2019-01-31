using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Helper;

namespace WpfApp.Model
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string SearchText { get; set; }

        public int TranscriptionId { get; set; }

        public MainWindowViewModel()
        {
            this.LoadInterviewPageCommand = new DelegateCommand(o => LoadInterviewPage());

            LoadTranscriptionQueuePageCommand = new DelegateCommand(o => this.LoadTranscriptionQueuePage());

            //this.LoadBrowsePageCommand = new DelegateCommand(o => this.LoadBrowsePage());

            this.LoadBrowsePageCommand = new DelegateCommand(o => this.LoadBrowsePage(TranscriptionId));

            this.LoadUsersPageCommand = new DelegateCommand(o => this.LoadUsersPage());

            this.LoadFindnReplacePageCommand = new DelegateCommand(o => this.LoadFindnReplacePage());

            this.LoadReportsPageCommand = new DelegateCommand(o => this.LoadReportsPage());

            this.LoadSettingsPageCommand = new DelegateCommand(o => this.LoadSettingsPage());

            this.LoadImportPageCommand = new DelegateCommand(o => this.LoadImportPage());

            this.LoadStatewideValidationPageCommand = new DelegateCommand(o => this.LoadStatewideValidatioPage());

            this.LoadSearchOnBrowsePageCommand = new DelegateCommand(o => this.LoadBrowsePageWithSearch());

            //this.LoadLogoutPageCommand = new DelegateCommand(o => this.LoadLogoutHomePage());
        }

        public MainWindowViewModel(bool isActive)
        {
            this.LoadBrowsePage();
        }

        public ICommand LoadHomePageCommand { get; private set; }

        public ICommand LoadInterviewPageCommand { get; private set; }

        public static ICommand LoadTranscriptionQueuePageCommand { get; private set; }

        public ICommand LoadBrowsePageCommand { get; private set; }

        public ICommand LoadUsersPageCommand { get; private set; }

        public ICommand LoadFindnReplacePageCommand { get; private set; }

        public ICommand LoadReportsPageCommand { get; private set; }

        public ICommand LoadSettingsPageCommand { get; private set; }

        public ICommand LoadImportPageCommand { get; private set; }

        public ICommand LoadStatewideValidationPageCommand { get; private set; }

        public ICommand LoadSearchOnBrowsePageCommand { get; private set; }

        public ICommand LoadLogoutPageCommand { get; private set; }

        // ViewModel that is currently bound to the ContentControl
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                this.OnPropertyChanged("CurrentViewModel");
            }
        }
        
        public void LoadInterviewPage()
        {
            CurrentViewModel = new InterviewPageViewModel(
                new InterviewPage() { PageTitle = "Interview" });
        }

        private void LoadTranscriptionQueuePage()
        {
            CurrentViewModel = new TranscriptionQueueViewModel(
                new TranscriptionQueuePage() { PageTitle = "Transcription Queue" });
        }

        private void LoadBrowsePage()
        {
            CurrentViewModel = new BrowsePageViewModel(
                new BrowsePage() { PageTitle = "Browse" });
        }

        private void LoadBrowsePage(object parameter)
        {
            CurrentViewModel = new BrowsePageViewModel(
                new BrowsePage() { PageTitle = "Browse",  });

            //TranscriptionId = transcriptionI
        }


        private void LoadUsersPage()
        {
            CurrentViewModel = new UsersPageViewModel(
                new UsersPage() { PageTitle = "User" });
        }

        private void LoadFindnReplacePage()
        {
            CurrentViewModel = new FindnReplacePageViewModel(
                new FindnReplacePage() { PageTitle = "Find and Replace" });
        }
        private void LoadReportsPage()
        {
            CurrentViewModel = new ReportsPageViewModel(
                new ReportModel() { PageTitle = "Reports" });
        }

        private void LoadSettingsPage()
        {
            CurrentViewModel = new SettingsPageViewModel(
                new SettingsPage() { PageTitle = "Settings" });
        }

        private void LoadImportPage()
        {
            CurrentViewModel = new ImportPageViewModel(
                new ImportPage() { PageTitle = "Imort" });
        }

        private void LoadStatewideValidatioPage()
        {
            CurrentViewModel = new StatewideValidationPageViewModel(
                new StatewideValidationPage() { PageTitle = "Statewide Validation" });
        }

        private void LoadBrowsePageWithSearch()
        {
            CurrentViewModel = new BrowsePageViewModel(
               new BrowsePage()
               {
                   PageTitle = "Browse",
                   SearchText = SearchText
               });
        }

        //private void LoadLogoutHomePage()
        //{
        //    CurrentViewModel = new HomePageViewModel(
        //        new HomePage()
        //        {
        //            PageTitle = "Home Page"
        //        });
        //}

    }
}
