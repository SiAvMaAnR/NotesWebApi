using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Controller.Notes
{
    public class UpdateFavoriteNoteDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsFavorite { get; set; }
    }
}
