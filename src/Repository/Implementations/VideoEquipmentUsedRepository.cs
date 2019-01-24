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
    public class VideoEquipmentUsedRepository :
        GenericRepository<OralCMSDBEntities, videoequipmentused>, IVideoEquipmentUsedRepository
    {
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        public List<string> List()
        {
            return GetAll().ToList().Select(o => o.VideoEquipmentUsedName).ToList();
        }
    }
}
