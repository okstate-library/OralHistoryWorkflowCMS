using Model;
using Model.Transfer;
using System.Collections.Generic;
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
    public partial class ModifySubseries : UserControl
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int SubseriesId { get; set; }

        #endregion

        #region Constructor

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ModifySubseries" /> class.
        ///// </summary>
        //public ModifySubseries()
        //{
        //    InitializeComponent();

        //    Loaded += ModifySubseriesUserControl_Loaded;

        //    DataContext = new SubseriesUIModel();
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="subseriesId">The user identifier.</param>
        public ModifySubseries(int subseriesId)
        {
            InitializeComponent();
            
            Loaded += ModifySubseriesUserControl_Loaded;

            SubseriesId = subseriesId;

          DataContext = new SubseriesUIModel();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Loaded event of the UserUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ModifySubseriesUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (SubseriesId > 0)
            {
                ViewSubseries();

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
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void SaveUserDetails_Click(object sender, RoutedEventArgs e)
        {
            if (FormValidation())
            {
                //short collectionId = short.Parse(CollectionComboBox.SelectedValue.ToString());

                if (IsAddNewRecord)
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        SubseryModel = GetSubseriesModel(true),
                        
                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Add,

                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifySubseries(requestModel);

                    ShowMessage(response);
                }
                else
                {
                    RequestModel requestModel = new RequestModel()
                    {
                        SubseryModel = GetSubseriesModel(false),

                        WellKnownModificationType = Core.Enums.WellKnownModificationType.Edit,

                    };

                    ResponseModel response = App.BaseUserControl.InternalService.ModifySubseries(requestModel);

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

        private SubseryModel GetSubseriesModel(bool isNewRecord)
        {
            SubseryModel subseryModel = new SubseryModel()
            {
                CollectionId = short.Parse(((SubseriesUIModel)DataContext).SelectedCollection),
                SubseryName = SubseriesNameTextBox.Text,
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
                subseryModel.Id = SubseriesId;
            }

            return subseryModel;
        }

        /// <summary>
        /// Forms the validation.
        /// </summary>
        /// <returns></returns>
        private bool FormValidation()
        {
            if (!string.IsNullOrEmpty(SubseriesNameTextBox.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Views the subseries.
        /// </summary>
        private void ViewSubseries()
        {
            RequestModel requestModel = new RequestModel()
            {
                SubseriesId = SubseriesId,
            };

            ResponseModel response = App.BaseUserControl.InternalService.GetSubseies(requestModel);

            if (response.IsOperationSuccess)
            {
                SubseryModel subseries = response.SubseriesModel;

                ((SubseriesUIModel)DataContext).SelectedCollection = subseries.CollectionId.ToString();               
                SubseriesNameTextBox.Text = subseries.SubseryName;

                NumberTextBox.Text = subseries.Number;
                DatesTextBox.Text = subseries.Dates;
                NotesTextBox.Text = subseries.Note;
                SubjectTextBox.Text = subseries.Subjects;
                KeywordsTextBox.Text = subseries.Keywords;
                DescriptionTextBox.Text = subseries.Description;
                ScopeAndContentTextBox.Text = subseries.ScopeAndContent;
                CustodialHistoryTextBox.Text = subseries.CustodialHistory;
                SizeTextBox.Text = subseries.Size;
                AcquisitioninfoTextBox.Text = subseries.Acquisitioninfo;
                LanguageTextBox.Text = subseries.Language;

                PreservationNoteTextBox.Text = subseries.PreservationNote;
                RightsTextBox.Text = subseries.Rights;
                AccessRestrictionsTextBox.Text = subseries.AccessRestrictions;
                PublicationRightsTextBox.Text = subseries.PublicationRights;

                PreferredCitationTextBox.Text = subseries.PreferredCitation;
                RelatedCollectionTextBox.Text = subseries.RelatedCollection;
                SeparatedMaterialTextBox.Text = subseries.SeparatedMaterial;
                OriginalLocationTextBox.Text = subseries.OriginalLocation;
                CopiesLocationTextBox.Text = subseries.CopiesLocation;
                PublicationNoteTextBox.Text = subseries.PublicationNote;
                CreatorTextBox.Text = subseries.Creator;
                ContributorsTextBox.Text = subseries.Contributors;
                ProcessedByTextBox.Text = subseries.ProcessedBy;
                SponsorsTextBox.Text = subseries.Sponsors;
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }

        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="response">The response.</param>
        private void ShowMessage(ResponseModel response)
        {
            if (response.IsOperationSuccess)
            {
                App.ShowMessage(true, string.Empty);
            }
            else
            {
                App.ShowMessage(false, response.ErrorMessage);
            }
        }

        #endregion
    }
}
