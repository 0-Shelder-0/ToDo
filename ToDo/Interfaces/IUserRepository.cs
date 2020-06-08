using System;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IUserRepository : IDisposable, IEntityRepository<User>
    {
        User GetUserByEmail(string email);
    }
}
