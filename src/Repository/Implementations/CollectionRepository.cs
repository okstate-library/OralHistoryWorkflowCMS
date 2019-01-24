using EntityData;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementations
{
    /// <summary>
    /// Methods define in Collection Repository
    /// </summary>
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.collection}" />
    /// <seealso cref="Repository.ICollectionRepository" />
    public class CollectionRepository :
        GenericRepository<OralCMSDBEntities, collection>, ICollectionRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>
        /// returns the sepcific user object.
        /// </returns>
        public List<collection> GetCollections()
        {
            return GetAll().ToList();
        }
    }
}
