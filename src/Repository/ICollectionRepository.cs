using EntityData;
using System.Collections.Generic;

namespace Repository
{
    /// <summary>
    ///  Methods needs to implement in Collection Repository
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.collection}" />
    public interface ICollectionRepository : IGenericRepository<collection>
    {
        /// <summary>
        /// Gets the collections.
        /// </summary>
        /// <returns></returns>
        List<collection> GetCollections();
    }
}
