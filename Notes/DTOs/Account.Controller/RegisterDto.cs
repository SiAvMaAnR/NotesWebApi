using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Range(1, 150)]
        [Required]
        public int Age { get; set; }
    }
}
