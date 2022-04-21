using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Notes.DTOs.Notes.UpdateNote
{
    public class UpdateNoteRequest
    {
        public UpdateNoteRequest(int id,string title, string description, bool isDone)
        {
            Id = id;
            Title = title;
            Description = description;
            IsDone = isDone;
        }
        public UpdateNoteRequest() { }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(300, MinimumLength = 0)]
        public string Description { get; set; }

        [Required]
        public bool IsDone { get; set; }
    }
}
