using Notes.Domain.Models;

namespace Notes.DTOs.Service.Users.GetUser
{
    public class GetUserResponse
    {
        public GetUserResponse(User? user)
        {
            User = user;
        }

        public GetUserResponse() { }

        public User? User { get; set; }
    }
}
