#region imports

using System;

#endregion imports

namespace Core
{
    /// <summary>
    ///
    /// </summary>
    public static class CurrencyHelper
    {
        #region Public Fuinctionalities

        /// <summary>
        /// Formats the currency.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static string FormatCurrency(Decimal amount)
        {
            return amount.ToString("0.00");
        }

        #endregion Public Fuinctionalities
    }
}