﻿namespace Model.Transfer.Search
{
    /// <summary>
    ///Defiens the properties for the pagination info model.
    /// </summary>
    public class PaginationInfo
    {
        #region [Public Properties]

        /// <summary>
        /// Gets/Sets the Current Page
        /// </summary>
        public int CurrentPage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Set the List Length
        /// </summary>
        public int ListLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Set the Total List Length
        /// </summary>
        public int TotalListLength
        {
            get;
            set;
        }

        /// <summary>
        /// page count
        /// </summary>
        public int TotalPages
        {
            get;
            set;
        }

        #endregion [Public Properties]
    }
}