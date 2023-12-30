using Client.Models;

namespace Client.ViewModels.Roles
{
    public class NewRoleVM
    {
        public string Name { get; set; }

        public static implicit operator Role(NewRoleVM dto)
        {
            return new Role
            {
                Guid = new Guid(),
                Name = dto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
