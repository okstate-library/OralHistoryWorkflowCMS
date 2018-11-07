using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityData;

namespace Repository
{
    /// <summary>
    /// Define subject interface repository.
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.subject}" />
    public interface ISubjectRepository : IGenericRepository<subject>
    {
    }
}
