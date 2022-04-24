using Notes.Domain.Models;
using Notes.DTOs.Service.Users.DeleteUser;
using Notes.DTOs.Service.Users.GetUser;
using Notes.DTOs.Service.Users.GetUsersList;
using Notes.DTOs.Service.Users.SetRoleUser;

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
