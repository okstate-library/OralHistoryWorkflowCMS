using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class FindnReplacePageViewModel : ViewModelBase
    {
        public FindnReplacePageViewModel(FindnReplacePage model)
        {
            this.Model = model;
        }

        public FindnReplacePage Model { get; private set; }

    }
}
