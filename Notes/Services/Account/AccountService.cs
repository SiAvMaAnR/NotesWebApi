using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs.Service.Account.Edit;
using Notes.DTOs.Service.Account.Login;
using Notes.DTOs.Service.Account.Register;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Security;
using Notes.Infrastucture.Interfaces;
using Notes.Interfaces;

namespace Notes.Services.Account
{
    public class AccountService : BaseService<User>, IAccountService
    {
        public AccountService(IAsyncRepository<User> repository, EFContext context, IHttpContextAccessor httpContext, IConfiguration configuration)
            : base(repository, context, httpContext, configuration)
        {
        }

        public async Task<EditResponse> EditAccountAsync(EditRequest request)
        {
            if (this.user != null)
            {
                user.Login = request.Login;
                user.Person.Firstname = request.Firstname;
                user.Person.Surname = request.Surname;
                user.Person.Age = request.Age;

                await repository.UpdateAsync(user);
                return new EditResponse(true);
            }
            return new EditResponse(false);
        }

        public async Task<LoginResponse> LoginAccountAsync(LoginRequest request)
        {
            User? user = await repository.GetAsync(user => user.Email == request.Email);

            bool isVerify = (user != null)
                ? AuthOptions.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)
                : false;

            return new LoginResponse()
            {
                User = user,
                IsVerify = isVerify,
                Token = (isVerify) ? await AuthOptions.CreateTokenAsync(user, new Dictionary<string, string>()
                {
                    {"secretKey",configuration.GetSection("Authorization:SecretKey").Value },
                    {"audience", configuration.GetSection("Authorization:Audience").Value },
                    {"issuer" , configuration.GetSection("Authorization:Issuer").Value},
                    {"lifeTime" , configuration.GetSection("Authorization:LifeTime").Value},
                }) : ""
            };
        }

        public async Task<RegisterResponse> RegisterAccountAsync(RegisterRequest request)
        {
            if (AuthOptions.CreatePasswordHash(request.Password, out byte[]? passwordHash, out byte[]? passwordSalt))
            {
                await repository.AddAsync(new User()
                {
                    Email = request.Email,
                    Login = request.Login,
                    PasswordHash = passwordHash!,
                    PasswordSalt = passwordSalt!,
                    Role = Role.User.ToString(),

                    Person = new Person()
                    {
                        Firstname = request.Firstname,
                        Surname = request.Surname,
                        Age = request.Age,
                    }
                });
                return new RegisterResponse(true);
            }
            return new RegisterResponse(false);
        }
    }
}
