namespace Notes.DTOs.Users.SetRoleUser
{
    public class SetRoleUserResponse
    {
        public SetRoleUserResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public SetRoleUserResponse() { }

        public bool IsSuccess { get; set; }
    }
}
