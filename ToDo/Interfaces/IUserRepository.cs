using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IUserRepository : IDisposable, IRepository<User>
    {
        User GetUserByEmail(string email);
        User GetUserById(int userId);
    }
}
