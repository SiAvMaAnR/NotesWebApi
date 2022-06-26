using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Controller.Notes
{
    public class AddNoteDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(1000, MinimumLength = 0)]
        public string Description { get; set; }

        public DateTime? EventDate { get; set; }

        [Required]
        public bool IsDone { get; set; }
    }
}
