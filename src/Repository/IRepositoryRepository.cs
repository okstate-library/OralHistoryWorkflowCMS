using EntityData;
using System.Collections.Generic;

namespace Repository
{
    /// <summary>
    ///  Methods needs to implement in Collection Repository
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.collection}" />
    public interface IRepositoryRepository : IGenericRepository<repository>
    {
        /// <summary>
        /// Gets all repositories.
        /// </summary>
        /// <returns></returns>
        List<repository> GetRepositoriess();
    }
}
