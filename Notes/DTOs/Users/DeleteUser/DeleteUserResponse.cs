namespace Notes.DTOs.Users.DeleteUser
{
    public class DeleteUserResponse
    {
        public DeleteUserResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public DeleteUserResponse() { }


        public bool IsSuccess { get; set; }
    }
}
