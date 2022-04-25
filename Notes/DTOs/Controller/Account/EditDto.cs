using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Controller.Account
{
    public class EditDto
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Range(1, 150)]
        [Required]
        public int Age { get; set; }
    }
}
