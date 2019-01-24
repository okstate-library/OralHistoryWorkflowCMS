// -----------------------------------------------------------------------
// <copyright file="JSONObjectMapepr.cs" company="">
// </copyright>
// -----------------------------------------------------------------------

namespace Core
{
    using Newtonsoft.Json;

    /// <summary>
    ///
    /// </summary>
    public static class JSONObjectMapper
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination GetObject<TDestination>(string source)
        {
            TDestination returnObject = default(TDestination);

            if (!string.IsNullOrEmpty(source))
            {
                returnObject = JsonConvert.DeserializeObject<TDestination>(source);
            }

            return returnObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetString<TSource>(TSource source)
        {
            string returnString = string.Empty;

            returnString = JsonConvert.SerializeObject(source);

            return returnString;
        }


        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetIndentedString<TSource>(TSource source, Formatting type)
        {
            string returnString = string.Empty;

            returnString = JsonConvert.SerializeObject(source, type);

            return returnString;
        }
    }
}