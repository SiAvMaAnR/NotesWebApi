using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Auth
{
    public class LoginDto
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [MaxLength(30)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
