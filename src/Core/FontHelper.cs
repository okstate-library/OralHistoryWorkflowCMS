namespace Core
{
    /// <summary>
    ///
    /// </summary>
    public class FontHelper
    {
        /// <summary>
        /// Uppercases the first.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// Text with first letter capital
        /// </returns>
        public static string UppercaseFirst(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            char[] charList = text.ToCharArray();

            charList[0] = char.ToUpper(charList[0]);

            return new string(charList);
        }
    }
}