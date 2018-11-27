using System;
using System.Globalization;
using System.Windows.Controls;

namespace WpfApp.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    class PastDateValidationRule : ValidationRule
    {
        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult" /> object.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime time;
            if (!DateTime.TryParse((value ?? "").ToString(),
                CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                out time)) return new ValidationResult(false, "Invalid date");

            return time.Date > DateTime.Now.Date
                ? new ValidationResult(false, "Past or current date required")
                : ValidationResult.ValidResult;
        }
    }
}
