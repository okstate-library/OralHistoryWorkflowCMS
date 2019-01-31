using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Model
{
    public class BrowsePage : BasePage
    {
        public int TranscriptionId  { get; set; }

        public string SearchText { get; internal set; }
    }
}
