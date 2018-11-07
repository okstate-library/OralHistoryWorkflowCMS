namespace Core
{
    /// <summary>
    /// Defines the functionalities related to decimal data type.
    /// </summary>
    public static class DecimalHelper
    {
        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public static decimal MinValue
        {
            get
            {
                return decimal.Parse("0.00");
            }
        }
    }
}