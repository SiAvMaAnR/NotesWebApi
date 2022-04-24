using Notes.Domain.Models;
using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Users.DeleteUser;
using Notes.DTOs.Users.GetUser;
using Notes.DTOs.Users.GetUsersList;
using Notes.DTOs.Users.SetRoleUser;

namespace Notes.Interfaces
{
    public interface IUserService
    {
        User? User { get; }
        Task<GetUsersListResponse> GetUsersListAsync(GetUsersListRequest request);
        Task<GetUserResponse> GetUserAsync(GetUserRequest request);
        Task<SetRoleUserResponse> SetRoleUserAsync(SetRoleUserRequest request);
        Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request);
    }
}
