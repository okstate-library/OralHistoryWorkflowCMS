using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Helper
{
    /// <summary>
    /// Class contains for search helpering properties and methods.
    /// </summary>
    public class SearchHelper
    {
        /// <summary>
        /// The no records found message
        /// </summary>
        public static string NoRecordsFoundMessage = "No records found...";

        /// <summary>
        /// The initial current page
        /// </summary>
        public static int InitialCurrentPage = 1;

        /// <summary>
        /// The initial list length
        /// </summary>
        public static int InitialListLength = 100;

        /// <summary>
        /// The selected page size index
        /// </summary>
        public static int SelectedPageSizeIndex = 1;

        /// <summary>
        /// The page size list
        /// </summary>
        public static List<string> PageSizeList = new List<string>()
        {
            " 50",
            " 100",
            " 200"
        };

        /// <summary>
        /// Gets the record count text.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="totalRecordCount">The total record count.</param>
        /// <returns></returns>
        public static string GetRecordCountText(int recordCount, int pageCount, int totalRecordCount)
        {
            int pageRecords = pageCount * recordCount;

            return (pageRecords - recordCount + 1) + " to " + (totalRecordCount < pageRecords ? totalRecordCount : pageRecords) + " out of " + totalRecordCount + " record(s)";
        }
    }
}
