namespace Notes.DTOs.Service.Account.Register
{
    public class RegisterResponse
    {
        public RegisterResponse(bool isAdded)
        {
            IsAdded = isAdded;
        }
        public RegisterResponse() { }

        public bool IsAdded { get; set; } = false;
    }
}
