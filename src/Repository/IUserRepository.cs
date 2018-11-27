using EntityData;

namespace Repository
{
    /// <summary>
    /// Methods needs to implement in User Repository
    /// </summary>
    /// <seealso cref="EntityData.IGenericRepository{EntityData.user}" />
    public interface IUserRepository : IGenericRepository<user>
    {
    }
}
