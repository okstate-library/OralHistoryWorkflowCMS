using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityData;

namespace Repository
{
    /// <summary>
    /// 
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
