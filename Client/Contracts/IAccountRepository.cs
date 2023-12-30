using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Accounts;

namespace Client.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account, Guid>
    {
        Task<ResponseHandler<TokenVM>> Login(LoginVM loginVM);
        Task<ResponseHandler<RegisterVM>> Register(RegisterVM registerVM);
    }
}
