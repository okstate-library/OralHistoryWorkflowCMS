#region imports

using System;
using System.Globalization;
using System.Text;

#endregion imports

namespace Core
{
    /// <summary>
    ///
    /// </summary>
    public static class DateTimeHelper
    {
        #region Properties

        /// <summary>
        /// Gets the minimum date.
        /// </summary>
        /// <value>
        /// The minimum date.
        /// </value>
        public static DateTime MinDate
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets the short date now.
        /// </summary>
        /// <value>
        /// The short date now.
        /// </value>
        public static string ShortDateNow
        {
            get
            {
                return DateTime.Now.ToShortDateString();
            }
        }

        /// <summary>
        /// Get current date and time form datetime helper.
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public static string Time
        {
            get
            {
                return DateTime.Now.ToShortTimeString();
            }
        }

        /// <summary>
        /// Gets the day after tomorrow.
        /// </summary>
        /// <value>
        /// The day after tomorrow.
        /// </value>
        public static DateTime DayAfterTomorrow
        {
            get
            {
                return DateTime.Now.AddDays(2);
            }
        }

        /// <summary>
        /// Gets the yesterday.
        /// </summary>
        /// <value>
        /// The yesterday.
        /// </value>
        public static DateTime Yesterday
        {
            get
            {
                return DateTime.Now.AddDays(-1).Date;
            }
        }

        public static DateTime DayBeforeYesterday
        {
            get
            {
                return DateTime.Now.AddDays(-2).Date;
            }
        }

        /// <summary>
        /// Gets the next five days.
        /// </summary>
        /// <value>
        /// The next five days.
        /// </value>
        public static DateTime NextFiveDays
        {
            get
            {
                return DateTime.Now.AddDays(5).Date;
            }
        }

        /// <summary>
        /// Gets the last7 days.
        /// </summary>
        /// <value>
        /// The last7 days.
        /// </value>
        public static DateRange Last7Days
        {
            get
            {
                return GetLast7Days();
            }
        }

        /// <summary>
        /// Gets the last week.
        /// </summary>
        /// <value>
        /// The last week.
        /// </value>
        public static DateRange LastWeek
        {
            get
            {
                return GetLastWeek();
            }
        }

        /// <summary>
        /// Gets the last month.
        /// </summary>
        /// <value>
        /// The last month.
        /// </value>
        public static DateRange LastMonth
        {
            get
            {
                return GetLastMonth();
            }
        }

        /// <summary>
        /// Gets the last30 days.
        /// </summary>
        /// <value>
        /// The last30 days.
        /// </value>
        public static DateRange Last30Days
        {
            get
            {
                return GetLast30Days();
            }
        }

        /// <summary>
        /// Defines the date range struct
        /// </summary>
        public struct DateRange
        {
            /// <summary>
            /// Gets or sets the start.
            /// </summary>
            /// <value>
            /// The start.
            /// </value>
            public DateTime Start
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the end.
            /// </summary>
            /// <value>
            /// The end.
            /// </value>
            public DateTime End
            {
                get;
                set;
            }
        }

        #endregion Properties

        #region Public Fuinctionalities

        /// <summary>
        /// Formats to short date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string FormatToShortDate(string date)
        {
            return DateTime.Parse(date).ToShortDateString();
        }

        /// <summary>
        /// Formats the universal time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string FormatUniversalTime(string date)
        {
            string value = DateTime.Parse(date).ToString("HH:mm");

            return value;
        }

        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Returns the formatted date.
        /// </returns>
        public static string FormatDate(DateTime date)
        {
            return string.Format("{0:MMMM dd, yyyy}", date);
        }

        /// <summary>
        /// Formats the date with time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string FormatDateWithTime(DateTime date)
        {
            return string.Format("{0:MMMM dd, yyyy, h:mm tt}", date);
        }

        /// <summary>
        /// Formats the date with only month and date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string FormatDateWithOnlyMonthAndDate(string date)
        {
            return DateTime.Parse(date).ToString("MMMM-d");
        }

        /// <summary>
        /// Formats the date with only time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string FormatDateWithOnlyTime(DateTime date)
        {
            return date.ToString("h:mm tt");
        }

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <param name="timeInMiliseconds">The time in milliseconds.</param>
        /// <returns></returns>
        public static string GetTime(int timeInMilliseconds)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(timeInMilliseconds);

            StringBuilder timeString = new StringBuilder();

            timeString.Append(t.Hours);
            timeString.Append(" ");
            timeString.Append("hr(s)");
            timeString.Append(" ");
            timeString.Append(t.Minutes);
            timeString.Append(" ");
            timeString.Append("min(s)");
            timeString.Append(" ");
            timeString.Append(t.Seconds);
            timeString.Append(" ");
            timeString.Append("sec(s)");
            timeString.Append(" ");

            return timeString.ToString();
        }

        /// <summary>
        /// Gets the date difference.
        /// </summary>
        /// <param name="firstDate">The first date.</param>
        /// <param name="secondDate">The second date.</param>
        /// <returns>
        /// Returns the date different in between two dates.
        /// </returns>
        public static int GetDateDiff(DateTime firstDate, DateTime secondDate)
        {
            int diff = (firstDate.Date - secondDate.Date).Days;

            return diff;
        }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            return DateTimeHelper.Now.ToString("yyyyMMddHHmmssffff");
        }

        /// <summary>
        /// Gets the last30 days.
        /// </summary>
        /// <returns>
        /// Returns last 30 days range.
        /// </returns>
        private static DateRange GetLast30Days()
        {
            DateTime date = Now;

            DateRange range = new DateRange();

            range.Start = date.AddDays(-30).Date;
            range.End = date.Date;

            return range;
        }

        /// <summary>
        /// Lasts the month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        private static DateRange GetLastMonth()
        {
            DateTime date = Now;

            DateRange range = new DateRange();

            range.Start = (new DateTime(date.Year, date.Month, 1)).AddMonths(-1).Date;
            range.End = range.Start.AddMonths(1).AddSeconds(-1).Date;

            return range;
        }

        /// <summary>
        /// Lasts the week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        private static DateRange GetLast7Days()
        {
            DateTime date = Now;

            DateRange range = new DateRange();

            range.Start = date.AddDays(-7).Date;
            range.End = date.Date;

            return range;
        }

        /// <summary>
        /// Lasts the week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        private static DateRange GetLastWeek()
        {
            DateTime date = Now;

            DateRange range = ThisWeek();

            range.Start = range.Start.AddDays(-7).Date;
            range.End = range.End.AddDays(-7).Date;

            return range;
        }

        /// <summary>
        /// Thises the week.
        /// </summary>
        /// <returns></returns>
        public static DateRange ThisWeek()
        {
            DateTime date = Now;

            DateRange range = new DateRange();

            range.Start = date.Date.AddDays(-(int)date.DayOfWeek).Date;
            range.End = range.Start.AddDays(7).AddSeconds(-1).Date;

            return range;
        }

        /// <summary>
        /// Gets the current hour time.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentHourTime()
        {
            string value = DateTime.Now.ToString("HH:mm");

            return value;
        }

        /// <summary>
        /// Gets the current hour time in universal format.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentHourTimeInUniversalFormat(int hoursToAdd)
        {
            DateTime expectedTime = DateTime.Now.AddHours(hoursToAdd);

            int currentHour = expectedTime.Hour;
            int currentMinute = expectedTime.Minute;

            if (currentMinute > 30)
            {
                currentMinute = 0;
                currentHour++;
            }
            else if (0 < currentMinute && currentMinute < 30)
            {
                currentMinute = 3;
            }

            if (currentHour < 9)
            {
                return string.Format("0{0}:{1}0", currentHour, currentMinute);
            }
            else
            {
                return string.Format("{0}:{1}0", currentHour, currentMinute);
            }

        }

        /// <summary>
        /// Gets the datetime.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static DateTime GetDatetime(string date, string time)
        {
            string datetime = string.Format("{0} {1}", date, time);

            return DateTime.Parse(datetime);
        }

        // <summary>
        /// Gets the datetime.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime GetDatetime(string date)
        {
            string datetime = string.Format("{0}", date);

            return DateTime.Parse(datetime);
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime GetDate(string date)
        {
            string datetime = string.Format("{0}", date);

            return DateTime.Parse(datetime).Date;
        }
          
        #endregion Public Fuinctionalities

    }
}