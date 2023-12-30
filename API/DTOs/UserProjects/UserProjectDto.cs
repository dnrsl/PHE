using API.Models;

namespace API.DTOs.UserProjects
{
    public class UserProjectDto
    {
        public Guid Guid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid ProjectGuid { get; set; }

        public static implicit operator UserProject(UserProjectDto dto)
        {
            return new UserProject
            {
                Guid = dto.Guid,
                UserGuid = dto.UserGuid,
                ProjectGuid = dto.ProjectGuid,
                ModifiedDate = DateTime.Now,
            };
        }

        public static explicit operator UserProjectDto(UserProject userProject)
        {
            return new UserProjectDto
            {
                Guid = userProject.Guid,
                UserGuid = userProject.UserGuid,
                ProjectGuid = userProject.ProjectGuid,
            };
        }
    }
}
