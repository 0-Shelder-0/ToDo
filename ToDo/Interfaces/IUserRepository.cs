using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
    }
}
