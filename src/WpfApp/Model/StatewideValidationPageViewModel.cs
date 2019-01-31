using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class StatewideValidationPageViewModel : ViewModelBase
    {
        public StatewideValidationPageViewModel(StatewideValidationPage model)
        {
            this.Model = model;
        }

        public StatewideValidationPage Model { get; private set; }

    }
}
