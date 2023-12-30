using Client.Models;

namespace Client.ViewModels.Projects
{
    public class ProjectVM
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserGuid { get; set; }

        public static implicit operator Project(ProjectVM dto)
        {
            return new Project
            {
                Guid = dto.Guid,
                Name = dto.Name,
                Description = dto.Description,
                UserGuid = dto.UserGuid,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator ProjectVM(Project project)
        {
            return new ProjectVM
            {
                Guid = project.Guid,
                Name = project.Name,
                Description = project.Description,
                UserGuid = project.UserGuid
            };
        }
    }
}
