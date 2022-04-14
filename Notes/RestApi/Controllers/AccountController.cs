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

        [HttpGet, Authorize(Roles = "User,Admin")]
        public ActionResult<string> Get()
        {
            return Ok(new
            {
                userNameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "NONE",
                userName = User.FindFirstValue(ClaimTypes.Name) ?? "NONE",
                userRole = User.FindFirstValue(ClaimTypes.Role) ?? "NONE",
                userEmail = User.FindFirstValue(ClaimTypes.Email) ?? "NONE"
            });

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try
            {
                if (context.Users.Any(user => user.Email == register.Email))
                    return BadRequest(new
                    {
                        Response = "This user already exists!",
                    });

                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Response = "Incorrect data!",
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
                    return Ok("Success");
                }

                return BadRequest(new
                {
                    errorMessage = "Failed to create an account!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    errorMessage = "Failed to create an account!"
                });
            }

        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto login)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(user => user.Email == login.Email);

                if (user == null)
                    return BadRequest(new { errorMessage = "User is not found!" });

                if (!AuthOptions.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                    return BadRequest(new { errorMessage = "Incorrect password!" });

                string secretKey = configuration.GetSection("Authorization:SecretKey").Value;

                if (string.IsNullOrEmpty(secretKey))
                    return BadRequest(new { errorMessage = "Failed to create token!" });

                context.Entry(user).Reference(x => x.Person).Load();

                return Ok(new
                {
                    token = await AuthOptions.CreateTokenAsync(user, secretKey),
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    errorMessage = "Failed to create token!"
                });
            }

        }
    }
}
