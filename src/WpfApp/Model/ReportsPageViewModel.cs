using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class ReportsPageViewModel : ViewModelBase
    {
        public ReportsPageViewModel(ReportModel model)
        {
            this.Model = model;
        }

        public ReportModel Model { get; private set; }

    }
}
