using Notes.Domain.Models;

namespace Notes.DTOs.Service.Account.Info
{
    public class InfoResponse
    {
        public InfoResponse(User? user)
        {
            User = user;
        }

        public InfoResponse() { }

        public User? User { get; set; }
    }
}
