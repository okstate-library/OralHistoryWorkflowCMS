    using System;

namespace Core
{
    /// <summary>
    ///
    /// </summary>
    public class GuidHelper
    {
        /// <summary>
        /// Unique identifiers the validate.
        /// </summary>
        /// <param name="stringGuid">The string unique identifier.</param>
        /// <returns>
        /// Whether the GUID value is valid or not.
        /// </returns>
        public static bool GuidValidate(string stringGuid)
        {
            Guid newGuid;

            if (Guid.TryParse(stringGuid, out newGuid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}