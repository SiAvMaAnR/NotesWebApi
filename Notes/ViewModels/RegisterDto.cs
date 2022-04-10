using System.ComponentModel.DataAnnotations;

namespace Notes.ViewModels
{
    public class RegisterDto
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [MaxLength(30)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [MaxLength(30)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Range(1, 150)]
        [Required]
        public int Age { get; set; }
    }
}
