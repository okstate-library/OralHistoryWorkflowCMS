using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class InterviewPageViewModel : ViewModelBase
    {
        public InterviewPageViewModel(InterviewPage model)
        {
            this.Model = model;
        }

        public InterviewPage Model { get; private set; }

    }
}
