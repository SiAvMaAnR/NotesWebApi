using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Security;
using Notes.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet, Authorize]
        public ActionResult<string> Get()
        {
            return Ok(new
            {
                userName = HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? "NONE",
                userRole = HttpContext?.User.FindFirstValue(ClaimTypes.Role) ?? "NONE"
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
                        error_message = "Такой пользователь уже существует!"
                    });

                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        errorMessage = "Не корректные данные!"
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
                    return Ok("Пользователь успешно добавлен");
                }

                return BadRequest(new
                {
                    errorMessage = "Не удалось создать аккаунт!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    errorMessage = "Не удалось создать аккаунт!"
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
                    return BadRequest(new
                    {
                        errorMessage = "Пользователь не найден!"
                    });

                if (!AuthOptions.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                    return BadRequest(new
                    {
                        errorMessage = "Не верный пароль!"
                    });


                string secretKey = configuration.GetSection("Authorization:SecretKey").Value;

                if (string.IsNullOrEmpty(secretKey))
                    return BadRequest(new
                    {
                        errorMessage = "Не удалось создать токен!"
                    });

                return Ok(new
                {
                    token = await AuthOptions.CreateTokenAsync(user, secretKey),
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    errorMessage = "Не удалось создать токен!"
                });
            }

        }



    }
}
