using Core;
using Core.Enums;
using System;

namespace WpfApp.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationModel
    {
        #region Private Properties

        /// <summary>
        /// The home demo item
        /// </summary>
        public static DemoItem searchDemoItem = new DemoItem("Search" , Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);
        
        /// <summary>
        /// The home demo item
        /// </summary>
        public static DemoItem homeDemoItem = new DemoItem("Home", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The interview demo item
        /// </summary>
        public static DemoItem interviewDemoItem = new DemoItem("Interview", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The transaction queue demo item
        /// </summary>
        public static DemoItem transactionQueueDemoItem = new DemoItem("Transcription Queue", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The browse queue demo item
        /// </summary>
        public static DemoItem browseQueueDemoItem = new DemoItem("Browse", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The queue demo item
        /// </summary>
        public static DemoItem usersQueueDemoItem = new DemoItem("Users", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The find and replace queue demo item
        /// </summary>
        public static DemoItem findAndReplaceQueueDemoItem = new DemoItem("Find and Replace", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The controlled vocabulary queue demo item
        /// </summary>
        public static DemoItem controlledVocabularyQueueDemoItem = new DemoItem("Controlled vocabulary", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The reports queue demo item
        /// </summary>
        public static DemoItem reportsQueueDemoItem = new DemoItem("Reports", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The settings queue demo item
        /// </summary>
        public static DemoItem settingsQueueDemoItem = new DemoItem("Setting", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);

        /// <summary>
        /// The import queue demo item
        /// </summary>
        public static DemoItem importQueueDemoItem = new DemoItem("Import", Model.MainWindowViewModel.LoadTranscriptionQueuePageCommand);
             
        /// <summary>
        /// Gets the admin demo items.
        /// </summary>
        /// <value>
        /// The admin demo items.
        /// </value>
        public static DemoItem[] AdminDemoItems { get; } = new[]
        {
            searchDemoItem,
            homeDemoItem,
            interviewDemoItem ,
            transactionQueueDemoItem,
            browseQueueDemoItem ,
            findAndReplaceQueueDemoItem,
            //controlledVocabularyQueueDemoItem,
            reportsQueueDemoItem,
            usersQueueDemoItem,
            settingsQueueDemoItem,
            importQueueDemoItem,
        };

        /// <summary>
        /// Gets the staff demo items.
        /// </summary>
        /// <value>
        /// The staff demo items.
        /// </value>
        private static DemoItem[] StaffDemoItems { get; } = new[]
        {
            homeDemoItem ,
            interviewDemoItem ,
            transactionQueueDemoItem,
            browseQueueDemoItem ,
            reportsQueueDemoItem,
        };

        /// <summary>
        /// Gets the student demo items.
        /// </summary>
        /// <value>
        /// The student demo items.
        /// </value>
        private static DemoItem[] StudentDemoItems { get; } = new[]
        {
            homeDemoItem ,
            interviewDemoItem ,
            transactionQueueDemoItem,
            browseQueueDemoItem,
        };

        /// <summary>
        /// Gets the guest user demo items.
        /// </summary>
        /// <value>
        /// The guest user demo items.
        /// </value>
        private static DemoItem[] GuestUserDemoItems { get; } = new[]
        {
            homeDemoItem ,
            interviewDemoItem,
            transactionQueueDemoItem,
            browseQueueDemoItem,
        };

        /// <summary>
        /// Gets the home demo items.
        /// </summary>
        /// <value>
        /// The home demo items.
        /// </value>
        private static DemoItem[] HomeDemoItems { get; } = new[]
        {
            homeDemoItem
        };

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationModel" /> class.
        /// </summary>
        /// <param name="isValidToProcess">if set to <c>true</c> [is valid to process].</param>
        /// <exception cref="ArgumentNullException">snackbarMessageQueue</exception>
        public AuthorizationModel(bool isValidToProcess)
        {
            if (isValidToProcess)
            {
                if (App.BaseUserControl.UserModel != null)
                {
                    UserName = App.BaseUserControl.UserModel.Name;

                    // user type based menu selection
                    WellKnownUserType userType = (WellKnownUserType)App.BaseUserControl.UserModel.UserType;
                    Usertype = "(" + EnumHelper.StringValueOf((WellKnownUserType)userType) + ")";

                    switch (userType)
                    {
                        case WellKnownUserType.GuestUser:
                            DemoItems = GuestUserDemoItems;
                            break;
                        case WellKnownUserType.Student:
                            DemoItems = StudentDemoItems;
                            break;
                        case WellKnownUserType.Staff:
                            DemoItems = StaffDemoItems;
                            break;
                        case WellKnownUserType.AdminUser:
                            DemoItems = AdminDemoItems;

                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    DemoItems = GuestUserDemoItems;
                }
            }
            else
            {
                DemoItems = HomeDemoItems;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the demo items.
        /// </summary>
        /// <value>
        /// The demo items.
        /// </value>
        public DemoItem[] DemoItems { get; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the usertype.
        /// </summary>
        /// <value>
        /// The usertype.
        /// </value>
        public string Usertype { get; set; }

        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        /// <value>
        /// The application version.
        /// </value>
        public string ApplicationVersion { get; set; } = "Version " + Properties.Settings.Default.Version;

        #endregion
    }
}