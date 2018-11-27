using EntityData;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementations
{

    /// <summary>
    /// Methods define in Keywords Repository
    /// </summary>
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.keyword}" />
    /// <seealso cref="Repository.ISubjectRepository" />
    public class KeywordRepository :
        GenericRepository<OralCMSDBEntities, keyword>, IKeywordRepository
    {

        /// <summary>
        /// Gets the keywords.
        /// </summary>
        /// <returns>
        /// Returns all keywords.
        /// </returns>
        public List<keyword> GetKeywords()
        {
            return this.GetAll().ToList();
        }

    }
}
