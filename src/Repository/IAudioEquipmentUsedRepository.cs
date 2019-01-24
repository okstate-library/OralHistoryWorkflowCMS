using EntityData;
using System.Collections.Generic;

namespace Repository
{
    /// <summary>
    ///  Methods needs to implement in Audio Equipment used Repository
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.interviewer}" />
    public interface IAudioEquipmentUsedRepository : IGenericRepository<audioequipmentused>
    {
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        List<string> List();
    }
}
