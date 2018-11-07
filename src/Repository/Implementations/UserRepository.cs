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
    public class UserRepository :
        GenericRepository<OralCMSDBEntities, user>, IUserRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public user GetUser(string username)
        {
            return this.FirstOrDefault(u => u.Username == username);
        }
        
        public user GetUserById(int userId)
        {
            return this.FirstOrDefault(u => u.UserId == userId);
        }

        public user GetUserToEdit(int userId)
        {
            return this.FirstToDelete(i => i.UserId == userId);
        }
    }
}
