using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel(SettingsPage model)
        {
            this.Model = model;
        }

        public SettingsPage Model { get; private set; }

    }
}
