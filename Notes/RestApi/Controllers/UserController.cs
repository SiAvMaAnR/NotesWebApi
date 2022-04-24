using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs.Users.Controller;
using Notes.DTOs.Users.DeleteUser;
using Notes.DTOs.Users.GetUser;
using Notes.DTOs.Users.GetUsersList;
using Notes.DTOs.Users.SetRoleUser;
using Notes.Infrastructure.ApplicationContext;
using Notes.Interfaces;

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
        public async Task<IActionResult> Get(int pageNumber, int pageSize)
        {
            try
            {
                var result = await service.GetUsersListAsync(new GetUsersListRequest(pageNumber, pageSize));

                if (result.Users == null)
                    return NotFound(new
                    {
                        status = TStatusCode.NotFound,
                        text = "Users not found!"
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
                    status = TStatusCode.OK,
                    text = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCode.BadRequest,
                    text = "Failed to get users!"
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
                        status = TStatusCode.NotFound,
                        text = "User not found!"
                    });

                return Ok(new
                {
                    data = new
                    {
                        user = result.User,
                    },
                    status = TStatusCode.OK,
                    text = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCode.BadRequest,
                    text = "Failed to get user!"
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
                        status = TStatusCode.BadRequest,
                        text = "Incorrect data!"
                    });

                var result = await service.SetRoleUserAsync(new SetRoleUserRequest()
                {
                    Id = updateRoleUserDto.Id,
                    Role = updateRoleUserDto.Role,
                });

                if (!result.IsSuccess) throw new Exception();

                return Ok(new
                {
                    status = TStatusCode.OK,
                    text = "Success!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCode.BadRequest,
                    text = "Failed to update user!"
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
                        status = TStatusCode.BadRequest,
                        text = "Failed to delete user!"
                    });

                return Ok(new
                {
                    status = TStatusCode.OK,
                    text = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCode.BadRequest,
                    text = "Failed to delete user!"
                });
            }
        }
    }
}
