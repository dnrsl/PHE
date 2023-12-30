using Client.Models;

namespace Client.ViewModels.Projects
{
    public class NewProjectVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserGuid { get; set; }

        public static implicit operator Project(NewProjectVM dto)
        {
            return new Project
            {
                Guid = new Guid(),
                Name = dto.Name,
                Description = dto.Description,
                UserGuid = dto.UserGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
