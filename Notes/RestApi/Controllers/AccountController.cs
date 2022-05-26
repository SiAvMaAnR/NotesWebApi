using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs.Controller.Account;
using Notes.DTOs.Service.Account.Edit;
using Notes.DTOs.Service.Account.Login;
using Notes.DTOs.Service.Account.Register;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Security;
using Notes.Infrastucture.Security;
using Notes.Interfaces;
using System.Security.Claims;

namespace Notes.Api.Presentation.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;
        private readonly EFContext context;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public AccountController(IAccountService service, EFContext context, ILogger<AccountController> logger, IConfiguration configuration)
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
                        message = "Account not found!"
                    });

                return Ok(new
                {
                    data = new { user = user },
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to get account!"
                });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        message = "Incorrect data!"
                    });

                if (context.Users.Any(user => user.Email == register.Email))
                    return BadRequest(new
                    {
                        message = "This user already exists!"
                    });

                var result = await service.RegisterAccountAsync(new RegisterRequest()
                {
                    Email = register.Email,
                    Login = register.Login,
                    Password = register.Password,
                    Firstname = register.Firstname,
                    Surname = register.Surname,
                    Age = register.Age
                });

                if (result.IsAdded)
                {
                    return Ok(new
                    {
                        message = "Success!",
                    });
                }

                return BadRequest(new
                {
                    message = "Failed to create an account!",
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to create an account!",
                });
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Post([FromBody] LoginDto login)
        {
            try
            {
                var result = await service.LoginAccountAsync(new LoginRequest()
                {
                    Email = login.Email,
                    Password = login.Password,
                });

                if (result.User == null)
                    return NotFound(new
                    {
                        message = "Account is not found!"
                    });

                if (!result.IsVerify)
                    return BadRequest(new
                    {
                        message = "Incorrect password!"
                    });

                return Ok(new
                {
                    data = new
                    {
                        token = result.Token,
                        type = "Bearer"
                    },
                    message = "Success!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Failed to create token!"
                });
            }

        }

        [HttpPut("Edit"), Authorize]
        public async Task<IActionResult> Put([FromBody] EditDto edit)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        message = "Incorrect data!"
                    });

                var result = await service.EditAccountAsync(new EditRequest()
                {
                    Login = edit.Login,
                    Firstname = edit.Firstname,
                    Surname = edit.Surname,
                    Age = edit.Age,
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
                    message = "Failed to update account!"
                });
            }
        }


        [HttpGet("IsAuthorized"), Authorize]
        public async Task<IActionResult> IsAuthorized()
        {
            return await Task.FromResult(Ok());
        }
    }
}
