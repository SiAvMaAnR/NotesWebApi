using Notes.Domain.Models;
using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Users.GetUser;
using Notes.DTOs.Users.GetUsersList;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Interfaces;
using Notes.Interfaces;
using System.Security.Claims;

namespace Notes.Services.Users
{
    public class UsersService : BaseService, IUserService
    {
        public UsersService(IAsyncRepository<Note> repository, EFContext context) 
            : base(repository, context)
        {
        }

        public async Task<GetUsersListResponse> GetUsersListAsync(AddNoteRequest request)
        {
            return new GetUsersListResponse();
        }

        public Task<GetUsersListResponse> GetUsersListAsync(GetUsersListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserResponse> GetUsersListAsync(GetUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
