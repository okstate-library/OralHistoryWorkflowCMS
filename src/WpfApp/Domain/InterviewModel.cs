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
    class InterviewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the collections.
        /// </summary>
        /// <value>
        /// The collections.
        /// </value>
        public ObservableCollection<Collection> Collections { get; set; }

        private string _title;
        private string _interviewee;
        private string _interviewer;
        private DateTime? _interviewDate;
        private string _selectedCollection;
        private string _selectedSeries;

        public InterviewModel()
        {
            SetCollectios();

            LongListToTestComboVirtualization = new List<int>(Enumerable.Range(0, 1000));

            //SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            SelectedCollection = null;
            SelectedSeries = null;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                this.MutateVerbose(ref _title, value, RaisePropertyChanged());
            }
        }

        public string Interviewee
        {
            get { return _interviewee; }
            set
            {
                this.MutateVerbose(ref _interviewee, value, RaisePropertyChanged());
            }
        }

        public string Interviewer
        {
            get { return _interviewer; }
            set
            {
                this.MutateVerbose(ref _interviewer, value, RaisePropertyChanged());
            }
        }

        public DateTime? InterviewDate
        {
            get { return _interviewDate; }
            set
            {
                _interviewDate = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCollection
        {
            get { return _selectedCollection; }
            set
            {
                this.MutateVerbose(ref _selectedCollection, value, RaisePropertyChanged());
            }
        }

        public string SelectedSeries
        {
            get { return _selectedSeries; }
            set
            {
                this.MutateVerbose(ref _selectedSeries, value, RaisePropertyChanged());
            }
        }

        public IList<int> LongListToTestComboVirtualization { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

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
