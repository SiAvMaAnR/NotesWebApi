using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Domain.Enums;
using Notes.DTOs.Controller.Users;
using Notes.DTOs.Service.Users.DeleteUser;
using Notes.DTOs.Service.Users.GetUser;
using Notes.DTOs.Service.Users.GetUsersList;
using Notes.DTOs.Service.Users.SetRoleUser;
using Notes.Infrastructure.ApplicationContext;
using Notes.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Notes.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly EFContext context;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public UserController(IUserService service, EFContext context, ILogger<UserController> logger, IConfiguration configuration)
        {
            this.service = service;
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get([Required] int pageNumber, [Required] int pageSize)
        {
            try
            {
                var result = await service.GetUsersListAsync(new GetUsersListRequest(pageNumber, pageSize));

                if (result.Users == null)
                    return NotFound(new
                    {
                        message = "Users not found!"
                    });

                return Ok(new
                {
                    data = new
                    {
                        users = result.Users,
                        pageNumber = result.PageNumber,
                        pageSize = result.PageSize,
                        totalUsers = result.TotalUsers,
                        totalPages = result.TotalPages
                    },
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to get users!"
                });
            }
        }

        [HttpGet("{id:int}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await service.GetUserAsync(new GetUserRequest(id));

                if (result.User == null)
                    return NotFound(new
                    {
                        message = "User not found!"
                    });

                return Ok(new
                {
                    data = new { user = result.User },
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to get user!"
                });
            }
        }

        [HttpPut("/api/User"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateRoleUserDto updateRoleUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        message = "Incorrect data!"
                    });

                var result = await service.SetRoleUserAsync(new SetRoleUserRequest()
                {
                    Id = updateRoleUserDto.Id,
                    Role = updateRoleUserDto.Role,
                });

                if (!result.IsSuccess) throw new Exception();

                return Ok(new
                {
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to update user!"
                });
            }
        }

        [HttpDelete("/api/User"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                var result = await service.DeleteUserAsync(new DeleteUserRequest(id));

                if (!result.IsSuccess)
                    return BadRequest(new
                    {
                        message = "Failed to delete user!"
                    });

                return Ok(new
                {
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to delete user!"
                });
            }
        }
    }
}
