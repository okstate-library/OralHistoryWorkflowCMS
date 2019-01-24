using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Helper;

namespace WpfApp.Domain
{
    /// <summary>
    /// Defiens the interview model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class InterviewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the collections.
        /// </summary>
        /// <value>
        /// The collections.
        /// </value>
        public ObservableCollection<Collection> Collections { get; set; }

        /// <summary>
        /// The title
        /// </summary>
        private string _title;

        /// <summary>
        /// The interviewee
        /// </summary>
        private string _interviewee;

        /// <summary>
        /// The interviewer
        /// </summary>
        private string _interviewer;

        /// <summary>
        /// The interview date
        /// </summary>
        private DateTime? _interviewDate;

        /// <summary>
        /// The selected collection
        /// </summary>
        private string _selectedCollection;

        /// <summary>
        /// The selected series
        /// </summary>
        private string _selectedSeries;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterviewModel"/> class.
        /// </summary>
        public InterviewModel()
        {
            SetCollectios();

            SelectedCollection = null;
            SelectedSeries = null;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get { return _title; }
            set
            {
                this.MutateVerbose(ref _title, value, RaisePropertyChanged());
            }
        }

        /// <summary>
        /// Gets or sets the interviewee.
        /// </summary>
        /// <value>
        /// The interviewee.
        /// </value>
        public string Interviewee
        {
            get { return _interviewee; }
            set
            {
                this.MutateVerbose(ref _interviewee, value, RaisePropertyChanged());
            }
        }

        /// <summary>
        /// Gets or sets the interviewer.
        /// </summary>
        /// <value>
        /// The interviewer.
        /// </value>
        public string Interviewer
        {
            get { return _interviewer; }
            set
            {
                this.MutateVerbose(ref _interviewer, value, RaisePropertyChanged());
            }
        }

        /// <summary>
        /// Gets or sets the interview date.
        /// </summary>
        /// <value>
        /// The interview date.
        /// </value>
        public DateTime? InterviewDate
        {
            get { return _interviewDate; }
            set
            {
                _interviewDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected collection.
        /// </summary>
        /// <value>
        /// The selected collection.
        /// </value>
        public string SelectedCollection
        {
            get { return _selectedCollection; }
            set
            {
                this.MutateVerbose(ref _selectedCollection, value, RaisePropertyChanged());
            }
        }

        /// <summary>
        /// Gets or sets the selected series.
        /// </summary>
        /// <value>
        /// The selected series.
        /// </value>
        public string SelectedSeries
        {
            get { return _selectedSeries; }
            set
            {
                this.MutateVerbose(ref _selectedSeries, value, RaisePropertyChanged());
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <returns></returns>
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the collectios.
        /// </summary>
        private void SetCollectios()
        {
            Collections = new ObservableCollection<Collection>();

            foreach (Model.CollectionModel collectionItem in App.BaseUserControl.Collecions)
            {
                List<KeyValuePair<int, string>> series = new List<KeyValuePair<int, string>>();

                foreach (SubseryModel subseryItem in App.BaseUserControl.Subseries.FindAll(s => s.CollectionId == collectionItem.Id))
                {
                    series.Add(new KeyValuePair<int, string>(subseryItem.Id, subseryItem.SubseryName));
                }

                Collections.Add(new Collection() { Id = collectionItem.Id, Name = collectionItem.CollectionName, Series = series });
            }
        }
    }
}
