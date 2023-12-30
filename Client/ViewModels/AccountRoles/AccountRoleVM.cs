using Client.Models;

namespace Client.ViewModels.AccountRoles
{
    public class AccountRoleVM
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator AccountRole(AccountRoleVM dto)
        {
            return new AccountRole
            {
                Guid = dto.Guid,
                AccountGuid = dto.AccountGuid,
                RoleGuid = dto.RoleGuid,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator AccountRoleVM(AccountRole accountRole)
        {
            return new AccountRoleVM
            {
                Guid = accountRole.Guid,
                AccountGuid = accountRole.AccountGuid,
                RoleGuid = accountRole.RoleGuid
            };
        }
    }
}
