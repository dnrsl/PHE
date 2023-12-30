using API.Models;

namespace API.DTOs.Users
{
    public class UserDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Photo { get; set; }

        public static implicit operator User(UserDto dto)
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

        public static explicit operator UserDto(User user)
        {
            return new UserDto
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
