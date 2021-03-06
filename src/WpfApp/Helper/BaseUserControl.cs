﻿using BusinessServices;
using Model;
using Model.Transfer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApp.Properties;

namespace WpfApp.Helper
{
    /// <summary>
    /// Define base user control.
    /// </summary>
    public class BaseUserControl
    {
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName { get; } = Settings.Default.WelComeApplicationTitle;

        /// <summary>
        /// Gets or sets the transcrption queue record count.
        /// </summary>
        /// <value>
        /// The transcrption queue record count.
        /// </value>
        public int TranscrptionQueueRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the browse record count.
        /// </summary>
        /// <value>
        /// The browse record count.
        /// </value>
        public int BrowseRecordCount { get; set; }
       

        /// <summary>
        /// The instance
        /// </summary>
        private static InternalService instance = null;

        /// <summary>
        /// Gets or sets the user model.
        /// </summary>
        /// <value>
        /// The user model
        /// </value>
        public UserModel UserModel { get; set; }

        /// <summary>
        /// Gets or sets the subseries.
        /// </summary>
        /// <value>
        /// The subseries.
        /// </value>
        public List<SubseryModel> Subseries { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        public static List<KeywordModel> Keywords { get; set; }

        /// <summary>
        /// Gets or sets the subjects.
        /// </summary>
        /// <value>
        /// The subjects.
        /// </value>
        public List<SubjectModel> Subjects { get; set; }

        /// <summary>
        /// Gets or sets the interviewers.
        /// </summary>
        /// <value>
        /// The interviewers.
        /// </value>
        public static List<PredefinedUserModel> PredefinedUsers { get; set; }

        /// <summary>
        /// Gets or sets the audio equipments.
        /// </summary>
        /// <value>
        /// The audio equipments.
        /// </value>
        public static List<string> AudioEquipments { get; set; }

        /// <summary>
        /// Gets or sets the video equipments.
        /// </summary>
        /// <value>
        /// The video equipments.
        /// </value>
        public static List<string> VideoEquipments { get; set; }

        /// <summary>
        /// Gets the usertypes.
        /// </summary>
        /// <value>
        /// The usertypes.
        /// </value>
        public List<UserTypeModel> Usertypes { get; private set; }

        /// <summary>
        /// Gets or sets the collecions.
        /// </summary>
        /// <value>
        /// The collecions.
        /// </value>
        public List<CollectionModel> Collections { get; set; }

        public List<RepositoryModel> Repositories { get; private set; }


        public ObservableCollection<Collection> ObservableCollection { get; set; }

        public ObservableCollection<Repository> ObservablRepository { get; set; }

        /// <summary>
        /// The default collection identifier
        /// </summary>
        public const short DefaultCollectionId = 1;

        /// <summary>
        /// Gets the internal service.
        /// </summary>
        /// <value>
        /// The internal service.
        /// </value>
        public InternalService InternalService
        {
            get
            {
                if (instance == null)
                {
                    instance = new InternalService();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is values changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is values changed; otherwise, <c>false</c>.
        /// </value>
        public static bool IsValuesChanged { get; set; }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public void InitializeComponent(bool isStartup)
        {
            RequestModel request = new RequestModel()
            {
                IsStartup = isStartup,
            };

            ResponseModel response = InternalService.GetSystemInitialize(request);

            if (response.IsOperationSuccess && isStartup)
            {
                TranscrptionQueueRecordCount = response.MainFormModel?.TranscrptionQueueRecordCount ?? 0;
                BrowseRecordCount = response.MainFormModel?.BrowseRecordCount ?? 0;

                Repositories = response.Repositories;
                Subseries = response.Subseries;
                Collections = response.Collections;
                Keywords = response.Keywords;
                Subjects = response.Subjects;

                PredefinedUsers = response.PredefinedUsers;
                AudioEquipments = response.AudioEquipmentsUsed;
                VideoEquipments = response.VideoEquipmentsUsed;
                Usertypes = response.UserTypes;
            }
            else if (response.IsOperationSuccess)
            {
                Repositories = response.Repositories;
                Subseries = response.Subseries;
                Collections = response.Collections;

                PredefinedUsers = response.PredefinedUsers;
                AudioEquipments = response.AudioEquipmentsUsed;
                VideoEquipments = response.VideoEquipmentsUsed;
            }

            SetObservableLists();
        }
        
        /// <summary>
        /// Sets the oberservable lists.
        /// </summary>
        public void SetObservableLists()
        {
            ObservableCollection = new ObservableCollection<Collection>();

            foreach (CollectionModel collectionItem in App.BaseUserControl.Collections)
            {
                List<KeyValuePair<int, string>> series = new List<KeyValuePair<int, string>>();

                foreach (SubseryModel subseryItem in App.BaseUserControl.Subseries.FindAll(s => s.CollectionId == collectionItem.Id))
                {
                    series.Add(new KeyValuePair<int, string>(subseryItem.Id, subseryItem.SubseryName));
                }

                ObservableCollection.Add(new Collection() { Id = (short)collectionItem.Id, Name = collectionItem.CollectionName, Series = series , Subseries = App.BaseUserControl.Subseries });
            }


            ObservablRepository = new ObservableCollection<Repository>();

            foreach (RepositoryModel repositoryModel in App.BaseUserControl.Repositories)
            {              
                ObservablRepository.Add(new Repository() { Id = (short)repositoryModel.Id, Name = repositoryModel.RepositoryName});
            }

        }

    }
}
