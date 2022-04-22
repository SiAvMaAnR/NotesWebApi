using Microsoft.EntityFrameworkCore;
using Notes.Domain.Models;
using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Users.GetUser;
using Notes.DTOs.Users.GetUsersList;
using Notes.DTOs.Users.SetRoleUser;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Interfaces;
using Notes.Infrastucture.Security;
using Notes.Interfaces;
using System.Security.Claims;

namespace Notes.Services.Users
{
    public class UsersService : BaseService<User>, IUserService
    {
        private readonly User? user;

        public UsersService(IAsyncRepository<User> repository, EFContext context, IHttpContextAccessor httpContext)
            : base(repository, context)
        {
            this.user = CurrentUser.GetUser(context, httpContext.HttpContext!.User);
        }

        public async Task<GetUsersListResponse> GetUsersListAsync(GetUsersListRequest request)
        {
            IEnumerable<User>? users = await repository.GetAllAsync(x => true);

            if (users != null)
            {
                var totalNotes = users?.ToList().Count ?? 0;

                var result = users?
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize);

                var tatalPages = (int)Math.Ceiling(((decimal)totalNotes / request.PageSize));

                return new GetUsersListResponse()
                {
                    Users = result,
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber,
                    TotalNotes = totalNotes,
                    TotalPages = tatalPages,
                };
            }

            return new GetUsersListResponse()
            {
                Users = null,
            };
        }

        public async Task<GetUserResponse> GetUserAsync(GetUserRequest request)
        {
            User? user = await repository.GetAsync(user => user.Id == request.Id);
            return new GetUserResponse(user);
        }

        public async Task<SetRoleUserResponse> SetRoleUserAsync(SetRoleUserRequest request)
        {
            User? user = await repository.GetAsync(user => user.Id == request.Id);

            if (user != null)
            {
                user.Role = request.Role.ToString();
                await repository.UpdateAsync(user);
                return new SetRoleUserResponse(true);
            }

            return new SetRoleUserResponse(false);
        }
    }
}
