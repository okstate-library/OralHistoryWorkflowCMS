﻿using BusinessServices;
using Model;
using Model.Transfer;
using System.Collections.Generic;
using WpfApp.Properties;

namespace WpfApp.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseUserControl
    {
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName  { get; } = Settings.Default.WelComeApplicationTitle;

        /// <summary>
        /// Gets or sets the transcrption queue record count.
        /// </summary>
        /// <value>
        /// The transcrption queue record count.
        /// </value>
        public static int TranscrptionQueueRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the browse record count.
        /// </summary>
        /// <value>
        /// The browse record count.
        /// </value>
        public static int BrowseRecordCount { get; set; }

        /// <summary>
        /// The instance
        /// </summary>
        private static InternalService instance = null;

        /// <summary>
        /// Gets or sets the user model.
        /// </summary>
        /// <value>
        /// The user model.
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
        /// Gets or sets the collecions.
        /// </summary>
        /// <value>
        /// The collecions.
        /// </value>
        public List<CollectionModel> Collecions { get; set; }

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
        /// Initializes the component.
        /// </summary>
        public void InitializeComponent()
        {
            ResponseModel response = this.InternalService.GetSystemInitialize();

            if (response.IsOperationSuccess)
            {
                TranscrptionQueueRecordCount = response.MainFormModel.TranscrptionQueueRecordCount;
                BrowseRecordCount = response.MainFormModel.BrowseRecordCount;

                Subseries = response.Subseries;
                Collecions = response.Collecions;
                Keywords = response.Keywords;
                Subjects = response.Subjects;
            }

        }
    }
}