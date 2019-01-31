using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class ImportPageViewModel : ViewModelBase
    {
        public ImportPageViewModel(ImportPage model)
        {
            this.Model = model;
        }

        public ImportPage Model { get; private set; }

    }
}
