using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.DTOs.Users.Controller;
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


        [HttpGet("{pageNumber:int}/{pageSize:int}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(int pageNumber, int pageSize)
        {
            try
            {
                var result = await service.GetUsersListAsync(new GetUsersListRequest(pageNumber, pageSize));

                if (result.Users == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "Users not found!"
                    });

                return Ok(new
                {
                    data = new
                    {
                        users = result.Users,
                        pageNumber = result.PageNumber,
                        pageSize = result.PageSize,
                        totalNotes = result.TotalNotes,
                        totalPages = result.TotalPages
                    },
                    status = TStatusCodes.OK,
                    text = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Failed to get users!"
                });
            }
        }


        [HttpGet("id:int"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var result = await service.GetUserAsync(new GetUserRequest(id));

                if (result.User == null)
                    return NotFound(new
                    {
                        status = TStatusCodes.Not_Found,
                        text = "User not found!"
                    });

                return Ok(new
                {
                    data = new
                    {
                        user = result.User,
                    },
                    status = TStatusCodes.OK,
                    text = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Failed to get user!"
                });
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutRoleUser([FromBody] UpdateRoleUserDto updateRoleUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        status = TStatusCodes.Bad_Request,
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
                    status = TStatusCodes.OK,
                    text = "Success!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    status = TStatusCodes.Bad_Request,
                    text = "Failed to update note!"
                });
            }
        }
    }
}
