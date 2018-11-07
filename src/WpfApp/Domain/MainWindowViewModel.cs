using System.Configuration;
using MaterialDesignDemo;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Controls;
using System;
using Model;
using Core.Enums;
using Core;
using System.Drawing;

namespace WpfApp.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowViewModel
    {
        #region Private Properties

        /// <summary>
        /// The home demo item
        /// </summary>
        public static DemoItem homeDemoItem = new DemoItem("Home", PackIconKind.Home, new Home(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The interview demo item
        /// </summary>
        public static DemoItem interviewDemoItem = new DemoItem("Interview", PackIconKind.CommentQuestion, new Interview(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The transaction queue demo item
        /// </summary>
        public static DemoItem transactionQueueDemoItem = new DemoItem("Transcription Queue", PackIconKind.LibraryPlus, new TranscriptionQueue(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The browse queue demo item
        /// </summary>
        public static DemoItem browseQueueDemoItem = new DemoItem("Browse", PackIconKind.OpenInApp, new Browse(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The queue demo item
        /// </summary>
        public static DemoItem QueueDemoItem = new DemoItem("Users", PackIconKind.NaturePeople, new Users(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The find and replace queue demo item
        /// </summary>
        public static DemoItem findAndReplaceQueueDemoItem = new DemoItem("Find and Replace", PackIconKind.FindReplace, new FindReplace(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The controlled vocabulary queue demo item
        /// </summary>
        public static DemoItem controlledVocabularyQueueDemoItem = new DemoItem("Controlled vocabulary", PackIconKind.AppleKeyboardControl, new ControlledVocabulary(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The reports queue demo item
        /// </summary>
        public static DemoItem reportsQueueDemoItem = new DemoItem("Reports", PackIconKind.FileDocument, new Reports(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The settings queue demo item
        /// </summary>
        public static DemoItem settingsQueueDemoItem = new DemoItem("Setting", PackIconKind.Settings, new Setting(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The import queue demo item
        /// </summary>
        public static DemoItem importQueueDemoItem = new DemoItem("Import", PackIconKind.Upload, new FileImport(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto);

        /// <summary>
        /// The state wide validation queue demo item
        /// </summary>
        public static DemoItem stateWideValidationQueueDemoItem = new DemoItem("Statewide validation", PackIconKind.Upload, new Statewidevalidation(), ScrollBarVisibility.Auto, ScrollBarVisibility.Auto, Color.Aqua);


        /// <summary>
        /// Gets the admin demo items.
        /// </summary>
        /// <value>
        /// The admin demo items.
        /// </value>
        private static DemoItem[] AdminDemoItems { get; } = new[]
        {
            homeDemoItem,
            interviewDemoItem ,
            transactionQueueDemoItem,
            browseQueueDemoItem ,
            findAndReplaceQueueDemoItem,
            controlledVocabularyQueueDemoItem,
            reportsQueueDemoItem,
            settingsQueueDemoItem,
            importQueueDemoItem,
            stateWideValidationQueueDemoItem,
        };

        /// <summary>
        /// Gets the interviewer demo items.
        /// </summary>
        /// <value>
        /// The interviewer demo items.
        /// </value>
        private static DemoItem[] InterviewerDemoItems { get; } = new[]
        {
            homeDemoItem ,
            interviewDemoItem ,
            transactionQueueDemoItem,
            browseQueueDemoItem ,
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>
        /// <param name="snackbarMessageQueue">The snackbar message queue.</param>
        /// <exception cref="ArgumentNullException">snackbarMessageQueue</exception>
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));

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
                    case WellKnownUserType.Interviewer:
                        DemoItems = InterviewerDemoItems;
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