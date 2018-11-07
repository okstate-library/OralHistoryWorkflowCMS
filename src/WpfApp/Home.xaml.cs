﻿using Core.Enums;
using Model;
using Model.Transfer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Home : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        public Home()
        {
            InitializeComponent();

            this.DataContext = App.BaseUserControl;
            
            Loaded += HomePage_Load;
        }

        /// <summary>
        /// Handles the Load event of the HomePage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void HomePage_Load(object sender, RoutedEventArgs e)
        {
            if (App.BaseUserControl.UserModel != null && (WellKnownUserType)App.BaseUserControl.UserModel.UserType != WellKnownUserType.GuestUser)
            {
                RequestModel request = new RequestModel()
                {
                    UserModel = App.BaseUserControl.UserModel,
                };

                ResponseModel response = App.BaseUserControl.InternalService.GetPostInitializeMainForm(request);

                if (response.IsOperationSuccess)
                {
                    LatestTranscriptions.ItemsSource = response.Transcriptions;
                }
            }
            else
            {
                LatestTranscriptions.ItemsSource = "No data found";
            }

        }

        void LatestTranscriptionsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            //UIElement clicked = e.OriginalSource as UIElement;

            //if (clicked != null)
            //{
            //    //GridViewColumn row = clicked.ParentOfType<GridViewColumn>();

            //    //if (row != null)
            //    //{
            //    //    GridViewColumn cell = row.ChildrenOfType<GridViewColumn>().Where(c => c.Column.UniqueName == "FilePath").FirstOrDefault();
            //    //    if (cell != null)
            //    //    {
            //    //        MessageBox.Show(cell.Content.ToString());
            //    //    }
            //    //}
            //}

            //TranscriptionModel itemTranscriptionModel = ((FrameworkElement)e.OriginalSource).DataContext as TranscriptionModel;

            //TranscriptionQueue transcriptionQueue = new TranscriptionQueue();

            //transcriptionQueue.MainGrid.Visibility = Visibility.Hidden;

            //transcriptionQueue.cc.Content = new Transcription(itemTranscriptionModel.Id, Helper.WellKnownExpander.General);

        }
    }
}
