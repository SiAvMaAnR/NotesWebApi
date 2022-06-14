using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Controller.Notes
{
    public class UpdateDoneNoteDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsDone { get; set; }
    }
}
