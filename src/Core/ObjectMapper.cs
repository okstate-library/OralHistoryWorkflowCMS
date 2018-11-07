#region imports

using AutoMapper;

#endregion imports

namespace Core
{
    /// <summary>
    ///
    /// </summary>
    public static class ObjectMapper
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(
            TSource source)
        {
            Mapper.CreateMap<TSource, TDestination>();

            return Mapper.Map<TSource, TDestination>(source);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(
            TSource source, TDestination destination)
        {
            Mapper.CreateMap<TSource, TDestination>();

            return Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}