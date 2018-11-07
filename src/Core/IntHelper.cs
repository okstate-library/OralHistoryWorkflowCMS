using System;
namespace Core
{
    /// <summary>
    /// Defines the functionalities related to boolean data type.
    /// </summary>
    public class IntHelper
    {
        /// <summary>
        /// Ints the validate.
        /// </summary>
        /// <param name="stringInt">The string int.</param>
        /// <returns>
        /// Whether the int validation successfully done or not.
        /// </returns>
        public static bool IntValidate(string stringInt)
        {
            int value;

            if (int.TryParse(stringInt, out value))
            {
                if (value > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ints the validate.
        /// </summary>
        /// <param name="intValue">The int value.</param>
        /// <returns></returns>
        public static bool IntValidate(int intValue)
        {
            if (intValue > 0)
            {
                return true;
            }
            
            return false;
        }

      

    }
}