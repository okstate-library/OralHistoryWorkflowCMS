using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BrowseFormModel
    {
        public BrowseFormModel()
        {
            CollectionList = new List<KeyValuePair<string, string>>();
            SubjectList = new List<KeyValuePair<string, string>>();
            InterviewerList = new List<KeyValuePair<string, string>>();
            ContentDmList = new List<KeyValuePair<string, string>>();
        }

        public List<KeyValuePair<string, string>> CollectionList { get; set; }

        public List<KeyValuePair<string, string>> SubjectList { get; set; }

        public List<KeyValuePair<string, string>> InterviewerList { get; set; }

        public List<KeyValuePair<string, string>> ContentDmList { get; set; }
    }
}
