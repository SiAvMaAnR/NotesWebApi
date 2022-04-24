namespace Notes.DTOs.Service.Users.DeleteUser
{
    public class DeleteUserRequest
    {
        public DeleteUserRequest(int id)
        {
            Id = id;
        }
        public DeleteUserRequest() { }

        public int Id { get; set; }
    }
}
