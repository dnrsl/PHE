using API.Contracts;
using API.DTOs.Projects;
using API.Models;

namespace API.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<ProjectDto> GetAll()
        {
            var projects = _projectRepository.GetAll();
            if (!projects.Any())
            {
                return Enumerable.Empty<ProjectDto>();
            }

            var projectDtos = new List<ProjectDto>();
            foreach (var project in projects)
            {
                projectDtos.Add((ProjectDto)project);
            }

            return projectDtos;
        }

        public ProjectDto? GetByGuid(Guid guid)
        {
            var projectDto = _projectRepository.GetByGuid(guid);
            if (projectDto is null)
            {
                return null;
            }

            return (ProjectDto)projectDto;
        }

        public ProjectDto? Create (NewProjectDto newProjectDto)
        {
            var projectDto = _projectRepository.Create(newProjectDto);
            if (projectDto is null)
            {
                return null;
            }

            return (ProjectDto)projectDto;
        }

        public int Update(ProjectDto projectDto)
        {
            var project = _projectRepository.GetByGuid(projectDto.Guid);
            if (project is null)
            {
                return -1;
            }

            Project toUpdate = projectDto;
            toUpdate.CreatedDate = project.CreatedDate;
            var result = _projectRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var project = _projectRepository.GetByGuid(guid);
            if (project is null)
            {
                return -1;
            }

            var result = _projectRepository.Delete(project);
            return result ? 1 : 0;
        }
    }
}
