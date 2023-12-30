using API.DTOs.Roles;
using API.DTOs.UserProjects;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/user-projects")]
    public class UserProjectController : ControllerBase
    {
        private readonly UserProjectService _userProjectService;

        public UserProjectController(UserProjectService userProjectService)
        {
            _userProjectService = userProjectService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userProjectService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<UserProjectDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found."
                });
            }

            return Ok(new ResponseHandler<IEnumerable<UserProjectDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success! Data retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _userProjectService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<UserProjectDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found."
                });
            }

            return Ok(new ResponseHandler<UserProjectDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success! Data retrieved successfully.",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewUserProjectDto newUserProjectDto)
        {
            var result = _userProjectService.Create(newUserProjectDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewUserProjectDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error: Unable to retrieve data from the database."
                });
            }

            return Ok(new ResponseHandler<NewUserProjectDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success! Data has been created successfully.",
                Data = newUserProjectDto
            });
        }

        [HttpPut]
        public IActionResult Update(UserProjectDto userProjectDto)
        {
            var result = _userProjectService.Update(userProjectDto);
            if (result is -1)
            {
                return NotFound(new ResponseHandler<UserProjectDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found."
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UserProjectDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error: Unable to retrieve data from the database."
                });
            }

            return Ok(new ResponseHandler<UserProjectDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success! Data has been updated successfully."
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _userProjectService.Delete(guid);
            if (result is -1)
            {
                return NotFound(new ResponseHandler<UserProjectDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is not found."
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UserProjectDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error: Unable to retrieve data from the database."
                });
            }

            return Ok(new ResponseHandler<UserProjectDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success! Data has been deleted successfully."
            });
        }
    }
}
