using ToDo.Entities;

namespace ToDo.Data.Interfaces
{
    public interface IUserRepository : IEntityRepository<User>
    {
        User GetUserByEmail(string email);
    }
}
