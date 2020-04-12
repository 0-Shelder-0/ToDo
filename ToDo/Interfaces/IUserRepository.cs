using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IUserRepository : IDisposable, IEntityRepository<User>
    {
        User GetUserByEmail(string email);
        List<Board> GetBoards(int userId);
    }
}
