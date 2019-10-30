using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp.Helper;

namespace WpfApp.Domain
{
    /// <summary>
    /// Defiens the interview model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class CollectionUIModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the collections.
        /// </summary>
        /// <value>
        /// The collections.
        /// </value>
        public ObservableCollection<Repository> Repositories { get; set; }

        /// <summary>
        /// The selected collection
        /// </summary>
        private string _selectedRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterviewModel"/> class.
        /// </summary>
        public CollectionUIModel()
        {
            Repositories = App.BaseUserControl.ObservablRepository;
        }
        
        public string SelectedRepository
        {
            get { return _selectedRepository; }
            set
            {
                if (_selectedRepository == value) return;
                _selectedRepository = value;
                OnPropertyChanged("SelectedRepository");
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

    }
}
