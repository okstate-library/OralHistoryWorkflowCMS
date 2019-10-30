using Model;
using Model.Transfer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Helper;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class ModifyCollection : UserControl
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether this instance is add new record.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is add new record; otherwise, <c>false</c>.
        /// </value>
        public bool IsAddNewRecord { get; set; }

        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>
        /// The collection identifier.
        /// </value>
        public int CollectionId { get; set; }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="collectionId"/> class.
        /// </summary>
        /// <param name="collectionId">The user identifier.</param>
        public ModifyCollection(int collectionId)
        {
            InitializeComponent();

            Loaded += ModifyCollectionUserControl_Loaded;

            CollectionId = collectionId;

            DataContext = new CollectionUIModel();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the UserUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ModifyCollectionUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (CollectionId > 0)
            {
                ViewCollection();

                IsAddNewRecord = false;
            }
            else
            {
                IsAddNewRecord = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveUserDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveCollectionDetails_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {

                if (IsAddNewRecord)
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        CollectionModel = GetCollectionModel(true),

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Add,
                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifyCollection(requestModel);

                    ShowMessage(response);
                }
                else
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        CollectionModel = GetCollectionModel(false),

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Edit,

                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifyCollection(requestModel);

                    ShowMessage(response);

                }
            }
            else
            {
                App.ShowMessage(false, "Fill all the fields.");
            }
        }
               
        #endregion

        #region Private Methods

        private CollectionModel GetCollectionModel(bool isNewRecord)
        {
            CollectionModel collectionModel = new CollectionModel()
            {
                RepositoryId = short.Parse(((CollectionUIModel)DataContext).SelectedRepository),
                CollectionName = CollectionNameTextBox.Text,
                Number = NumberTextBox.Text,
                Dates = DatesTextBox.Text,
                Note = NotesTextBox.Text,
                Subjects = SubjectTextBox.Text,
                Keywords = KeywordsTextBox.Text,
                Description = DescriptionTextBox.Text,
                ScopeAndContent = ScopeAndContentTextBox.Text,
                CustodialHistory = CustodialHistoryTextBox.Text,
                Size = SizeTextBox.Text,
                Acquisitioninfo = AcquisitioninfoTextBox.Text,
                Language = LanguageTextBox.Text,

                PreservationNote = PreservationNoteTextBox.Text,
                Rights = RightsTextBox.Text,
                AccessRestrictions = AccessRestrictionsTextBox.Text,
                PublicationRights = PublicationRightsTextBox.Text,

                PreferredCitation = PreferredCitationTextBox.Text,
                RelatedCollection = RelatedCollectionTextBox.Text,
                SeparatedMaterial = SeparatedMaterialTextBox.Text,
                OriginalLocation = OriginalLocationTextBox.Text,
                CopiesLocation = CopiesLocationTextBox.Text,
                PublicationNote = PublicationNoteTextBox.Text,
                Creator = CreatorTextBox.Text,
                Contributors = ContributorsTextBox.Text,
                ProcessedBy = ProcessedByTextBox.Text,
                Sponsors = SponsorsTextBox.Text
            };

            if (!isNewRecord)
            {
                collectionModel.Id = CollectionId;
            }

            return collectionModel;
        }

        private bool FormValidation()
        {

            if (!string.IsNullOrEmpty(CollectionNameTextBox.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Views the user.
        /// </summary>
        private void ViewCollection()
        {
            RequestModel requestModel = new RequestModel()
            {
                CollectionId = CollectionId

            };

            ResponseModel response = App.BaseUserControl.InternalService.GetCollection(requestModel);

            if (response.IsOperationSuccess)
            {
                CollectionModel collection = response.Collection;

                ((CollectionUIModel)DataContext).SelectedRepository = collection.RepositoryId.ToString();

                CollectionNameTextBox.Text = collection.CollectionName;
                NumberTextBox.Text = collection.Number;
                DatesTextBox.Text = collection.Dates;
                NotesTextBox.Text = collection.Note;
                SubjectTextBox.Text = collection.Subjects;
                KeywordsTextBox.Text = collection.Keywords;
                DescriptionTextBox.Text = collection.Description;
                ScopeAndContentTextBox.Text = collection.ScopeAndContent;
                CustodialHistoryTextBox.Text = collection.CustodialHistory;
                SizeTextBox.Text = collection.Size;
                AcquisitioninfoTextBox.Text = collection.Acquisitioninfo;
                LanguageTextBox.Text = collection.Language;

                PreservationNoteTextBox.Text = collection.PreservationNote;
                RightsTextBox.Text = collection.Rights;
                AccessRestrictionsTextBox.Text = collection.AccessRestrictions;
                PublicationRightsTextBox.Text = collection.PublicationRights;

                PreferredCitationTextBox.Text = collection.PreferredCitation;
                RelatedCollectionTextBox.Text = collection.RelatedCollection;
                SeparatedMaterialTextBox.Text = collection.SeparatedMaterial;
                OriginalLocationTextBox.Text = collection.OriginalLocation;
                CopiesLocationTextBox.Text = collection.CopiesLocation;
                PublicationNoteTextBox.Text = collection.PublicationNote;
                CreatorTextBox.Text = collection.Creator;
                ContributorsTextBox.Text = collection.Contributors;
                ProcessedByTextBox.Text = collection.ProcessedBy;
                SponsorsTextBox.Text = collection.Sponsors;

            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

        }

        private void ShowMessage(ResponseModel response)
        {
            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, string.Empty);

                App.BaseUserControl.InitializeComponent(false);
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        #endregion
    }
}
