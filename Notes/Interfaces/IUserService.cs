using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Users.GetUser;
using Notes.DTOs.Users.GetUsersList;

namespace Notes.Interfaces
{
    public interface IUserService
    {
        Task<GetUsersListResponse> GetUsersListAsync(GetUsersListRequest request);

        Task<GetUserResponse> GetUsersListAsync(GetUserRequest request);
    }
}
