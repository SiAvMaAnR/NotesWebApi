using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs.Auth;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Security;
using System.Security.Claims;

namespace Notes.Api.Presentation.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EFContext context;
        private readonly IConfiguration configuration;

        public AccountController(EFContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }


        [HttpGet("/my"), Authorize(Roles = "User,Admin")]
        public IActionResult GetAccount()
        {
            try
            {
                return Ok(new
                {
                    data = new
                    {
                        userNameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "NONE",
                        userName = User.FindFirstValue(ClaimTypes.Name) ?? "NONE",
                        userRole = User.FindFirstValue(ClaimTypes.Role) ?? "NONE",
                        userEmail = User.FindFirstValue(ClaimTypes.Email) ?? "NONE"
                    },
                    title = "Success!",
                    status = TStatusCodes.OK
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    title = "User not found!",
                    status = TStatusCodes.Bad_Request
                });
            }
            

        }


        [HttpPost("/register")]
        public async Task<IActionResult> PostRegister([FromBody] RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (context.Users.Any(user => user.Email == register.Email))
                    return BadRequest(new
                    {
                        title = "This user already exists!",
                        status = TStatusCodes.Bad_Request
                    });

                if (AuthOptions.CreatePasswordHash(register.Password, out byte[]? passwordHash, out byte[]? passwordSalt))
                {
                    await context.Users.AddAsync(new User()
                    {
                        Email = register.Email,
                        PasswordHash = passwordHash!,
                        PasswordSalt = passwordSalt!,
                        Role = Roles.User.ToString(),
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
                        status = TStatusCodes.OK
                    });
                }

                return BadRequest(new
                {
                    title = "Failed to create an account!",
                    status = TStatusCodes.Bad_Request
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    title = "Failed to create an account!",
                    status = TStatusCodes.Bad_Request
                });
            }

        }


        [HttpPost("/login")]
        public async Task<IActionResult> PostLogin([FromBody] LoginDto login)
        {
            try
            {
                User? user = await context.Users.FirstOrDefaultAsync(user => user.Email == login.Email);

                if (user == null)
                    return NotFound(new
                    {
                        title = "User is not found!",
                        status = TStatusCodes.Not_Found
                    });

                if (!AuthOptions.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                    return BadRequest(new
                    {
                        title = "Incorrect password!",
                        status = TStatusCodes.Bad_Request
                    });

                string secretKey = configuration.GetSection("Authorization:SecretKey").Value;

                if (string.IsNullOrEmpty(secretKey))
                    return BadRequest(new
                    {
                        title = "Failed to create token!",
                        status = TStatusCodes.Bad_Request
                    });


                return Ok(new
                {
                    data = new
                    {
                        token = await AuthOptions.CreateTokenAsync(user, secretKey),
                        type = "Bearer"
                    },

                    title = "Success!",
                    status = TStatusCodes.OK,
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    title = "Failed to create token!",
                    status = TStatusCodes.Bad_Request
                });
            }

        }
    }
}
