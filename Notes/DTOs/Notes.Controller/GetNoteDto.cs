using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Notes.Controller
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
