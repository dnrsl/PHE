using Client.Models;

namespace Client.ViewModels.UserProjects
{
    public class UserProjectVM
    {
        public Guid Guid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid ProjectGuid { get; set; }

        public static implicit operator UserProject(UserProjectVM dto)
        {
            return new UserProject
            {
                Guid = dto.Guid,
                UserGuid = dto.UserGuid,
                ProjectGuid = dto.ProjectGuid,
                ModifiedDate = DateTime.Now,
            };
        }

        public static explicit operator UserProjectVM(UserProject userProject)
        {
            return new UserProjectVM
            {
                Guid = userProject.Guid,
                UserGuid = userProject.UserGuid,
                ProjectGuid = userProject.ProjectGuid,
            };
        }
    }
}
