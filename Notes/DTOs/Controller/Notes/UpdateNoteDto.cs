using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Controller.Notes
{
    public class UpdateNoteDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(300, MinimumLength = 0)]
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        [Required]
        public bool IsDone { get; set; }
    }
}
