using Notes.Domain.Models;

namespace Notes.DTOs.Service.Account.Login
{
    public class LoginResponse
    {
        public User? User { get; set; }
        public bool IsVerify { get; set; }
        public string Token { get; set; }
    }
}
