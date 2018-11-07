using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityData;

namespace Repository.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.collection}" />
    /// <seealso cref="Repository.ICollectionRepository" />
    public class CollectionRepository :
        GenericRepository<OralCMSDBEntities, collection>, ICollectionRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns></returns>
        public List<collection> GetCollections()
        {
            return this.GetAll().ToList();
        }

     
    }
}
