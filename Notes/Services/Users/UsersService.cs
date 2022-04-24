using Microsoft.EntityFrameworkCore;
using Notes.Domain.Models;
using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Users.DeleteUser;
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
        public UsersService(IAsyncRepository<User> repository, EFContext context, IHttpContextAccessor httpContext)
            : base(repository, context, httpContext)
        {
        }

        public async Task<GetUsersListResponse> GetUsersListAsync(GetUsersListRequest request)
        {
            IEnumerable<User>? users = await repository.GetAllAsync(user => user.Person);

            if (users != null)
            {
                var totalUsers = users?.ToList().Count ?? 0;

                var result = users?
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize);

                var tatalPages = (int)Math.Ceiling(((decimal)totalUsers / request.PageSize));

                return new GetUsersListResponse()
                {
                    Users = result,
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber,
                    TotalUsers = totalUsers,
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
            User? user = await repository.GetAsync(
                user => user.Id == request.Id,
                user => user.Person);

            return new GetUserResponse(user);
        }

        public async Task<SetRoleUserResponse> SetRoleUserAsync(SetRoleUserRequest request)
        {
            User? user = await repository.GetAsync(user => user.Id == request.Id);

            if (user != null && this.user!.Id != user.Id)
            {
                user.Role = request.Role.ToString();
                await repository.UpdateAsync(user);
                return new SetRoleUserResponse(true);
            }

            return new SetRoleUserResponse(false);
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request)
        {
            User? user = await repository.GetAsync(user => user.Id == request.Id);

            if (user != null)
            {
                await repository.DeleteAsync(user);
                return new DeleteUserResponse(true);
            }

            return new DeleteUserResponse(false);
        }
    }
}
