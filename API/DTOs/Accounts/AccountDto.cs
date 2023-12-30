using API.Models;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool ApprovedByAdmin { get; set; }
        public bool ApprovedByManager { get; set; }

        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                Guid = accountDto.Guid,
                Password = accountDto.Password,
                OTP = accountDto.OTP,
                IsUsed = accountDto.IsUsed,
                ExpiredDate = accountDto.ExpiredDate,
                ApprovedByAdmin = accountDto.ApprovedByAdmin,
                ApprovedByManager = accountDto.ApprovedByManager,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
                OTP = account.OTP,
                IsUsed = account.IsUsed,
                ExpiredDate = account.ExpiredDate,
                ApprovedByAdmin = account.ApprovedByAdmin,
                ApprovedByManager = account.ApprovedByManager,
            };
        }
    }
}
