using Client.Models;

namespace Client.ViewModels.Users
{
    public class UserVM
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Photo { get; set; }

        public static implicit operator User(UserVM dto)
        {
            return new User
            {
                Guid = dto.Guid,
                Name = dto.Name,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Photo = dto.Photo,
                ModifiedDate = DateTime.Now,
            };
        }

        public static explicit operator UserVM(User user)
        {
            return new UserVM
            {
                Guid = user.Guid,
                Name = user.Name,
                Email = user.Email,
                Telephone = user.Telephone,
                Photo = user.Photo
            };
        }
    }
}
