using EntityData;
using System.Collections.Generic;

namespace Repository
{
    /// <summary>
    ///  Methods needs to implement in Collection Repository
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.predefineduser}" />
    public interface IPredefineUserRepository : IGenericRepository<predefineduser>
    {
        /// <summary>
        /// Gets the predefined users .
        /// </summary>
        /// <returns></returns>
        List<predefineduser> GetPredefinedUsers(int usertype);

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        List<string> List();
    }
}
