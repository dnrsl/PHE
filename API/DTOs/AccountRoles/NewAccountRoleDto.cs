using API.Models;

namespace API.DTOs.AccountRoles
{
    public class NewAccountRoleDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator AccountRole(NewAccountRoleDto dto)
        {
            return new AccountRole
            {
                Guid = new Guid(),
                RoleGuid = dto.RoleGuid,
                AccountGuid = dto.AccountGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewAccountRoleDto(AccountRole accountRole)
        {
            return new NewAccountRoleDto
            {
                AccountGuid = accountRole.AccountGuid,
                RoleGuid = accountRole.RoleGuid,
            };
        }
    }
}
