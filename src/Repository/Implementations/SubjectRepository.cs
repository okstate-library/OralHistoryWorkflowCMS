using EntityData;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementations
{
    /// <summary>
    /// Methods define in subject Repository
    /// </summary>
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.subject}" />
    /// <seealso cref="Repository.ISubjectRepository" />
    public class SubjectRepository :
        GenericRepository<OralCMSDBEntities, subject>, ISubjectRepository
    {
        /// <summary>
        /// Gets the subjects.
        /// </summary>
        /// <returns>
        /// Returns all the subjects.
        /// </returns>
        public List<subject> GetSubjects()
        {
            return this.GetAll().ToList();
        }

    }
}
