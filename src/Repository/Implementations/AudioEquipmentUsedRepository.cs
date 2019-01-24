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
    public class AudioEquipmentUsedRepository :
        GenericRepository<OralCMSDBEntities, audioequipmentused>, IAudioEquipmentUsedRepository
    {
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        public List<string> List()
        {
            return GetAll().ToList().Select(o => o.AudioEquipmentUsedName).ToList();
        }
    }
}
