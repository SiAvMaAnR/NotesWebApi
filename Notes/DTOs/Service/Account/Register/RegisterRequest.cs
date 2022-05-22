namespace Notes.DTOs.Service.Account.Register
{
    public class RegisterRequest
    {
        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }
        
        public string Surname { get; set; }

        public int Age { get; set; }
    }
}
