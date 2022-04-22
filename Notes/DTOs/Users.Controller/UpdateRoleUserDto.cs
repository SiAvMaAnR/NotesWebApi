using Notes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Users.Controller
{
    public class UpdateRoleUserDto
    {
        [Required]
        public int Id { get; set; }

        [EnumDataType(typeof(Roles))]
        public Roles Role { get; set; }
    }
}
