using EntityData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementations
{
    /// <summary>
    /// Methods define in Collection Repository
    /// </summary>
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.collection}" />
    /// <seealso cref="Repository.ICollectionRepository" />
    public class RepositoryRepository :
        GenericRepository<OralCMSDBEntities, repository>, IRepositoryRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>
        /// returns the sepcific user object.
        /// </returns>
        public List<repository> GetRepositoriess()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// Gets the collecrtion to edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public repository GetRepositoryToEdit(int id)
        {
            return FirstToDelete(i => i.Id == id);
        }
    }
}
