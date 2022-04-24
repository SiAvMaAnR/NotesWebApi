using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs.Auth;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Security;
using Notes.Interfaces;
using System.Security.Claims;

namespace Notes.Api.Presentation.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService service;
        private readonly EFContext context;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public AccountController(IUserService service, EFContext context, ILogger<AccountController> logger, IConfiguration configuration)
        {
            this.service = service;
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpGet("Info"), Authorize]
        public IActionResult Get()
        {
            try
            {
                var user = service.User;

                if (user == null)
                    return NotFound(new
                    {
                        status = TStatusCode.NotFound,
                        text = "User not found!"
                    });

                return Ok(new
                {
                    data = new
                    {
                        user = user,
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


        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (context.Users.Any(user => user.Email == register.Email))
                    return BadRequest(new
                    {
                        title = "This user already exists!",
                        status = TStatusCode.BadRequest
                    });

                if (AuthOptions.CreatePasswordHash(register.Password, out byte[]? passwordHash, out byte[]? passwordSalt))
                {
                    await context.Users.AddAsync(new User()
                    {
                        Email = register.Email,
                        PasswordHash = passwordHash!,
                        PasswordSalt = passwordSalt!,
                        Role = Role.User.ToString(),

                        Person = new Person()
                        {
                            Name = register.Name,
                            Age = register.Age,
                        }
                    });

                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        title = "Success!",
                        status = TStatusCode.OK
                    });
                }

                return BadRequest(new
                {
                    title = "Failed to create an account!",
                    status = TStatusCode.BadRequest
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    title = "Failed to create an account!",
                    status = TStatusCode.BadRequest
                });
            }

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Post([FromBody] LoginDto login)
        {
            try
            {
                User? user = await context.Users.FirstOrDefaultAsync(user => user.Email == login.Email);

                if (user == null)
                    return NotFound(new
                    {
                        title = "User is not found!",
                        status = TStatusCode.NotFound
                    });

                if (!AuthOptions.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                    return BadRequest(new
                    {
                        title = "Incorrect password!",
                        status = TStatusCode.BadRequest
                    });

                string secretKey = configuration.GetSection("Authorization:SecretKey").Value;

                if (string.IsNullOrEmpty(secretKey))
                    return BadRequest(new
                    {
                        title = "Failed to create token!",
                        status = TStatusCode.BadRequest
                    });


                return Ok(new
                {
                    data = new
                    {
                        token = await AuthOptions.CreateTokenAsync(user, secretKey),
                        type = "Bearer"
                    },

                    title = "Success!",
                    status = TStatusCode.OK,
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    title = "Failed to create token!",
                    status = TStatusCode.BadRequest
                });
            }

        }
    }
}
