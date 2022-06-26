using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Notes.DTOs.Service.Notes.AddNote
{
    public class AddNoteRequest
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
