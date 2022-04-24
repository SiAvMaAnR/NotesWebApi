using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Controller.Notes
{
    public class GetNoteDto
    {
        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int PageSize { get; set; }

        public string Sort { get; set; } = "asc_date";
    }
}
