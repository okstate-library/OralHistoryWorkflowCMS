﻿using System;

namespace Model.Transfer.Search
{
    /// <summary>
    /// Defiens the properties for the search request model.
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
        /// Initializes a new instance of the <see cref="SearchRequest" /> class.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="listLength">Length of the list.</param>
        public SearchRequest(int currentPage, int listLength)
        {
            CurrentPage = currentPage;
            ListLength = listLength;
        }
    }
}
