using Client2.Contracts;
using Client2.Models;
using Client2.Utilities.Handlers;
using Client2.ViewModels.Accounts;
using Newtonsoft.Json;
using System.Text;

namespace Client2.Repositories
{
    public class AccountRepository : GeneralRepository<Account, Guid>, IAccountRepository
    {
        public AccountRepository(string request = "accounts/") : base(request)
        {

        }

        public async Task<ResponseHandler<TokenVM>> Login(LoginVM login)
        {
            ResponseHandler<TokenVM> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<TokenVM>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseHandler<RegisterVM>> Register(RegisterVM entity)
        {
            ResponseHandler<RegisterVM> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "register", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<RegisterVM>>(apiResponse);
            }
            return entityVM;
        }
    }
}
