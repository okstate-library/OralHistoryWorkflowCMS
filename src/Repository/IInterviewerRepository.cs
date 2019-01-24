using EntityData;
using System.Collections.Generic;

namespace Repository
{
    /// <summary>
    ///  Methods needs to implement in Collection Repository
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.interviewer}" />
    public interface IInterviewerRepository : IGenericRepository<interviewer>
    {
        /// <summary>
        /// Gets the interviewer.
        /// </summary>
        /// <returns></returns>
        List<interviewer> GetInterviewer();

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        List<string> List();
    }
}
