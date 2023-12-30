using Client.Models;

namespace Client.ViewModels.UserProjects
{
    public class NewUserProjectVM
    {
        public Guid UserGuid { get; set; }
        public Guid ProjectGuid { get; set; }

        public static implicit operator UserProject(NewUserProjectVM dto)
        {
            return new UserProject
            {
                Guid = new Guid(),
                UserGuid = dto.UserGuid,
                ProjectGuid = dto.ProjectGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }

        public static explicit operator NewUserProjectVM(UserProject userProject)
        {
            return new NewUserProjectVM
            {
                UserGuid = userProject.UserGuid,
                ProjectGuid = userProject.ProjectGuid,
            };
        }
    }
}
