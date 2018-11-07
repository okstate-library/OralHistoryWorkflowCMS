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
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.subject}" />
    /// <seealso cref="Repository.ISubjectRepository" />
    public class SubjectRepository :
        GenericRepository<OralCMSDBEntities, subject>, ISubjectRepository
    {

        /// <summary>
        /// Gets the subjects.
        /// </summary>
        /// <returns></returns>
        public List<subject> GetSubjects()
        {
            return this.GetAll().ToList();
        }
             
    }
}
