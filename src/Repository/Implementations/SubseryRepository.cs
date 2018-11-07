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
    public class SubseryRepository :
        GenericRepository<OralCMSDBEntities, subsery>, ISubseryRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<subsery> GetSubseries()
        {
            return this.GetAll().ToList();
        }
             
    }
}
