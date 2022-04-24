using Notes.Domain.Enums;

namespace Notes.DTOs.Users.SetRoleUser
{
    public class SetRoleUserRequest
    {
        public int Id { get; set; }

        public Role Role { get; set; }
    }
}
