using System.Linq;
using ToDo.Data.Interfaces;
using ToDo.Entities;

namespace ToDo.Data.Repositories
{
    public sealed class UserRepository : EntityRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public User GetUserByEmail(string email)
        {
            return DbContext.Users.FirstOrDefault(user => user.Email == email);
        }
    }
}
