namespace API.DTOs.Projects
{
    public class SubmitProjectDto
    {
        public Guid UserGuid { get; set; }
        public Guid ProjectGuid { get; set; }
    }
}
