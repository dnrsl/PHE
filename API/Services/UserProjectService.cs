using API.Contracts;
using API.DTOs.UserProjects;
using API.Models;

namespace API.Services
{
    public class UserProjectService
    {
        private readonly IUserProjectRepository _userProjectRepository;

        public UserProjectService (IUserProjectRepository userProjectRepository)
        {
            _userProjectRepository = userProjectRepository;
        }

        public IEnumerable<UserProjectDto> GetAll()
        {
            var userProjects = _userProjectRepository.GetAll();
            if (userProjects is null)
            {
                return Enumerable.Empty<UserProjectDto>();
            }

            var userProjectDtos = new List<UserProjectDto>();
            foreach (var userProjectDto in userProjects)
            {
                userProjectDtos.Add((UserProjectDto)userProjectDto);
            }
            return userProjectDtos;
        }

        public UserProjectDto? GetByGuid(Guid guid)
        {
            var userProject = _userProjectRepository.GetByGuid(guid);

            if (userProject is null)
            {
                return null;
            }

            return (UserProjectDto) userProject;
        }

        public UserProjectDto? Create(NewUserProjectDto newUserProjectDto)
        {
            var userProject = _userProjectRepository.Create(newUserProjectDto);

            if (userProject is null)
            {
                return null;
            }
            return (UserProjectDto) userProject;
        }

        public int Update(UserProjectDto userProjectDto)
        {
            var userProject = _userProjectRepository.GetByGuid(userProjectDto.Guid);
            if (userProject is null)
            {
                return -1;
            }

            UserProject toUpdate = userProjectDto;
            toUpdate.CreatedDate = userProject.CreatedDate;

            var result = _userProjectRepository.Update(toUpdate);
            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var userProject = _userProjectRepository.GetByGuid(guid);
            if(userProject is null)
            {
                return -1;
            }

            var result = _userProjectRepository.Delete(userProject);
            return result ? 1 : 0;
        }
    }
}
