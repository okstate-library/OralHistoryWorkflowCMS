using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WpfApp.Model.ViewModelBase" />
    public class BrowsePageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsePageViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public BrowsePageViewModel(BrowsePage model)
        {            
            this.Model = model;
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public BrowsePage Model { get; private set; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText
        {
            get
            {
                return this.Model.SearchText;
            }
            set
            {
                this.Model.SearchText = value;
                this.OnPropertyChanged("SearchText");
            }
        }

        /// <summary>
        /// Gets or sets the transcription identifier.
        /// </summary>
        /// <value>
        /// The transcription identifier.
        /// </value>
        public int TranscriptionId
        {
            get
            {
                return this.Model.TranscriptionId;
            }
            set
            {
                this.Model.TranscriptionId = value;
                this.OnPropertyChanged("SearchText");
            }
        }
    }
}
