using Client2.Models;
using Client2.Utilities.Handlers;
using Client2.ViewModels.Accounts;

namespace Client2.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account, Guid>
    {
        Task<ResponseHandler<TokenVM>> Login(LoginVM loginVM);
        Task<ResponseHandler<RegisterVM>> Register(RegisterVM registerVM);
    }
}
