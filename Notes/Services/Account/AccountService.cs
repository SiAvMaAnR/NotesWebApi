using Notes.Domain.Models;
using Notes.DTOs.Service.Account.Edit;
using Notes.DTOs.Service.Account.Login;
using Notes.DTOs.Service.Account.Logout;
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

        public Task<EditResponse> EditAccountAsync(EditRequest request)
        {
            throw new NotImplementedException();
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
                Token = (isVerify) ? await AuthOptions.CreateTokenAsync(user, configuration) : ""
            };
        }

        public Task<LogoutResponse> LogoutAccountAsync(LogoutRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterResponse> RegisterAccountAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
