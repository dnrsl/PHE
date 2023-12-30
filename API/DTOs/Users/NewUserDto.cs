﻿using API.Models;

namespace API.DTOs.Users
{
    public class NewUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Photo { get; set; }

        public static implicit operator User(NewUserDto dto)
        {
            return new User
            {
                Guid = new Guid(),
                Name = dto.Name,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Photo = dto.Photo,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
