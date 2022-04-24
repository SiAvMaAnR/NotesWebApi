using Notes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Controller.Users
{
    public class UpdateRoleUserDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    }
}
