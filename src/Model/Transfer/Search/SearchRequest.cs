using System;

namespace Model.Transfer.Search
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SearchRequest
    {
        /// <summary>
        /// The search
        /// </summary>
        private string search;

        /// <summary>
        /// Gets or sets the search query.
        /// </summary>
        /// <value>
        /// The search query.
        /// </value>
        public string Search
        {
            get
            {
                return string.IsNullOrEmpty(search) ? string.Empty : search.ToLower();
            }
            set
            {
                search = value;
            }
        }

        /// <summary>
        /// Gets/Sets the Current Page
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Set the List Length
        /// </summary>
        /// <value>
        /// The length of the list.
        /// </value>
        public int ListLength
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchRequest"/> class.
        /// </summary>
        public SearchRequest(int currentPage, int listLength)
        {
            this.CurrentPage = currentPage;
            this.ListLength = listLength;
        }
    }
}
