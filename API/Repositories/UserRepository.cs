using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class UserRepository : GeneralRepository<User>, IUserRepository
    {
        public UserRepository(PheDbContext context) : base(context) { }
    }
}
