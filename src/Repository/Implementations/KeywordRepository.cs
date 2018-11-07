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
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.keyword}" />
    /// <seealso cref="Repository.ISubjectRepository" />
    public class KeywordRepository :
        GenericRepository<OralCMSDBEntities, keyword>, IKeywordRepository
    {

        /// <summary>
        /// Gets the keywords.
        /// </summary>
        /// <returns></returns>
        public List<keyword> GetKeywords()
        {
            return this.GetAll().ToList();
        }

    }
}
