using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityData;

namespace Repository
{
    /// <summary>
    /// Defines the interface methods of keywords.
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.keyword}" />
    public interface IKeywordRepository : IGenericRepository<keyword>
    {
    }
}
