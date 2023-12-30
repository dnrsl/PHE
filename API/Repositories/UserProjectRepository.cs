using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class UserProjectRepository : GeneralRepository<UserProject>, IUserProjectRepository
    {
        public UserProjectRepository(PheDbContext context) : base(context) { }
    }
}
