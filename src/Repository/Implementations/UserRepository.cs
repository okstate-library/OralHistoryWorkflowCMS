using EntityData;

namespace Repository.Implementations
{
    /// <summary>
    /// Defines the user repository methods.
    /// </summary>
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.user}" />
    /// <seealso cref="Repository.IUserRepository" />
    public class UserRepository :
        GenericRepository<OralCMSDBEntities, user>, IUserRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// Returns the specific user supplied to the username.
        /// </returns>
        public user GetUser(string username)
        {
            return this.FirstOrDefault(u => u.Username == username);
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Return the user for the supplied user id.
        /// </returns>
        public user GetUserById(int userId)
        {
            return this.FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Gets the user to edit.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Retursn the user for edit purpose for supplied user id.
        /// </returns>
        public user GetUserToEdit(int userId)
        {
            return this.FirstToDelete(i => i.UserId == userId);
        }
    }
}
