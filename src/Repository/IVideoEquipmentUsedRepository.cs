using EntityData;
using System.Collections.Generic;

namespace Repository
{
    /// <summary>
    ///  Methods needs to implement in video Equipment used Repository
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.videoequipmentused}" />
    public interface IVideoEquipmentUsedRepository : IGenericRepository<videoequipmentused>
    {
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        List<string> List();
    }
}
