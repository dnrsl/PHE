using Client.Models;

namespace Client.ViewModels.Roles
{
    public class DefaultRoleVM
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public static implicit operator Role(DefaultRoleVM dto)
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
