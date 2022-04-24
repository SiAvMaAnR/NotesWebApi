using Notes.Domain.Models;

namespace Notes.DTOs.Users.GetUsersList
{
    public class GetUsersListResponse
    {
        public IEnumerable<User>? Users { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalUsers { get; set; }
        public int TotalPages { get; set; }
    }
}
