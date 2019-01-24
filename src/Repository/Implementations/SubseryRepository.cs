using EntityData;
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

    }
}
