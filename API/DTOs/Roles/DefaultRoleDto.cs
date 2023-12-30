using API.Models;

namespace API.DTOs.Roles
{
    public class DefaultRoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public static implicit operator Role(DefaultRoleDto dto)
        {
            return new Role
            {
                Guid = dto.Guid,
                Name = dto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
