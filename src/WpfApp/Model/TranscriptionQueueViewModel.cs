using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class TranscriptionQueueViewModel : ViewModelBase
    {
        public TranscriptionQueueViewModel(TranscriptionQueuePage model)
        {
            this.Model = model;
        }

        public TranscriptionQueuePage Model { get; private set; }

    }
}
