using EntityData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementations
{
    /// <summary>
    /// Methods define in User Repository
    /// </summary>
    public class SubseryRepository :
        GenericRepository<OralCMSDBEntities, subsery>, ISubseryRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns the subseries.
        /// </returns>
        public List<subsery> GetSubseries()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// Gets the subseries to edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public subsery GetSubseriesToEdit(int id)
        {
            return FirstToDelete(i => i.Id == id);
        }
    }
}
