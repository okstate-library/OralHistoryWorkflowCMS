namespace Core
{
    /// <summary>
    /// Defines the functionalities related to boolean data type.
    /// </summary>
    public class BooleanHelper
    {
        /// <summary>
        /// Gets the bool value.
        /// </summary>
        /// <param name="boolStringValue">The bool string value.</param>
        /// <returns>
        ///
        /// </returns>
        public static bool GetBoolValue(string boolStringValue)
        {
            bool boolValue = false;

            bool.TryParse(boolStringValue, out boolValue);

            return boolValue;
        }
    }
}