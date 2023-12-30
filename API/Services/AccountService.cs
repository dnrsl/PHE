using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.AccountRoles;
using API.DTOs.Users;
using API.Models;
using System.Security.Claims;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly PheDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, IAccountRoleRepository accountRoleRepository, ITokenHandler tokenHandler, PheDbContext pheDbContext)
        {
            _accountRepository = accountRepository;
            _dbContext = pheDbContext;
            _userRepository = userRepository;
            _accountRoleRepository = accountRoleRepository;
            _tokenHandler = tokenHandler;
        }

        public IEnumerable<AccountDto> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if(!accounts.Any())
            {
                return Enumerable.Empty<AccountDto>();
            }

            var accountDtos = new List<AccountDto>();
            foreach (var account in accounts)
            {
                accountDtos.Add((AccountDto)account);
            }
            return accountDtos;
        }

        public AccountDto? GetByGuid(Guid guid)
        {
            var accountDto = _accountRepository.GetByGuid(guid);
            if(accountDto is null)
            {
                return null;
            }
            return (AccountDto)accountDto;
        }

        public AccountDto? Create(NewAccountDto newAccountDto)
        {
            var accountDto = _accountRepository.Create(newAccountDto);
            if (accountDto is null)
            {
                return null;
            }
            return (AccountDto) accountDto;

        }

        public int Update(AccountDto accountDto)
        {
            var account = _accountRepository.GetByGuid(accountDto.Guid);
            if( account is null )
            {
                return -1;
            }
            Account toUpdate = accountDto;
            toUpdate.CreatedDate = account.CreatedDate;
            var result = _accountRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if( account is null )
            {
                return -1;
            }

            var result = _accountRepository.Delete(account);
            return result ? 1 : 0;
        }

        public RegisterDto? Register(RegisterDto registerDto)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var userToCreate = new NewUserDto
                {
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    Telephone = registerDto.Telephone,
                    Photo = registerDto.Photo
                };

                var userResult = _userRepository.Create(userToCreate);

                var accountResult = _accountRepository.Create(new NewAccountDto
                {
                    Guid = userResult.Guid,
                    IsUsed = true,
                    ExpiredDate = DateTime.Now,
                    OTP = 0000,
                    Password = registerDto.Password
                });

                var accountRoleResult = _accountRoleRepository.Create(new NewAccountRoleDto
                {
                    AccountGuid = accountResult.Guid,
                    RoleGuid = Guid.Parse("24706f51-2651-4cd2-9ca0-c8e510969b7d")
                });

                transaction.Commit();
            }

            catch
            {
                transaction.Rollback();
                return null;
            }

            return (RegisterDto)registerDto;
        }

        public string Login(LoginDto loginDto)
        {
            try
            {
                var getUser = _userRepository.GetByEmail(loginDto.Email);

                if (getUser is null)
                {
                    return "0";
                }

                var getAccount = _accountRepository.GetByGuid(getUser.Guid);
                if(loginDto.Password != getAccount.Password)
                {
                    return "0";
                }

                var getRoles = _accountRoleRepository.GetRoleNamesByAccountGuid(getUser.Guid);

                var claims = new List<Claim>
                {
                    new Claim("Guid", getAccount.Guid.ToString()),
                };

                foreach (var role in getRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var generatedToken = _tokenHandler.GenerateToken(claims);
                if(generatedToken is null)
                {
                    return "-1";
                }
                return generatedToken;
            }
            catch
            {
                return "0";
            }
        }

    }
}
