using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class CharacteristicRepository : GeneralRepository<Characteristic>, ICharacteristicRepository
    {
        public CharacteristicRepository(PheDbContext context) : base(context) { }
    }
}
