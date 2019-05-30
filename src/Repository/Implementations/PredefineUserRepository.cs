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
    public class PredefineUserRepository :
        GenericRepository<OralCMSDBEntities, predefineduser>, IPredefineUserRepository
    {
        public List<predefineduser> GetPredefinedUsers(int usertype)
        {
            return FindBy(p => p.UserType == usertype).ToList();
        }

        public List<predefineduser> GetPredefinedUsers()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        public List<string> List()
        {
            return GetAll().ToList().Select(o => o.Name).ToList();
        }
    }
}
