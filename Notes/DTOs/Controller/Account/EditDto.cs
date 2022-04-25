using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Controller.Account
{
    public class EditDto
    {
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Login { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Range(1, 150)]
        [Required]
        public int Age { get; set; }
    }
}
