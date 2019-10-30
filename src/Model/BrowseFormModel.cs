using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Defiens the methods and the properties of the browse from model.
    /// </summary>
    public class BrowseFormModel
    {

        public const string NotDarkArchive = "Not Dark Archive";

        public const string DarkArchive = "Dark Archive";

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseFormModel"/> class.
        /// </summary>
        public BrowseFormModel()
        {
            CollectionList = new List<ListBoxItem>();

            SubseriesList = new List<ListBoxItem>();

            SubjectList = new List<ListBoxItem>();

            InterviewerList = new List<ListBoxItem>();

            ContentDmList = new List<ListBoxItem>();

            RestrictionList = new List<ListBoxItem>();

            RestrictionList.Add(SetListBoxItem(NotDarkArchive, 0, new List<string>()));
        }

        /// <summary>
        /// Gets or sets the collection list.
        /// </summary>
        /// <value>
        /// The collection list.
        /// </value>
        public List<ListBoxItem> CollectionList { get; set; }

        public List<ListBoxItem> SubseriesList { get; set; }

        /// <summary>
        /// Gets or sets the subject list.
        /// </summary>
        /// <value>
        /// The subject list.
        /// </value>
        public List<ListBoxItem> SubjectList { get; set; }

        /// <summary>
        /// Gets or sets the interviewer list.
        /// </summary>
        /// <value>
        /// The interviewer list.
        /// </value>
        public List<ListBoxItem> InterviewerList { get; set; }

        /// <summary>
        /// Gets or sets the content dm list.
        /// </summary>
        /// <value>
        /// The content dm list.
        /// </value>
        public List<ListBoxItem> ContentDmList { get; set; }

        public List<ListBoxItem> RestrictionList { get; set; }

        public static ListBoxItem SetListBoxItem(string name, int count, List<string> requests)
        {
            return new ListBoxItem
            {
                Name = name,
                Count = count,
                Value = name.Trim(),
                IsChecked = requests.Contains(name) ? true : false
            };
        }

    }


    public class ListBoxItem
    {
        //public string Key { get; set; }

        public string Key
        {
            get
            {
                return Name.Trim() + " (" + Count + ")";
            }
        }

        public string Value { get; set; }

        public bool IsChecked { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }
    }
}
