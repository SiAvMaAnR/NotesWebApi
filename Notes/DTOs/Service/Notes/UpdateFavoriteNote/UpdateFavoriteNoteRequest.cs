using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Service.Notes.UpdateFavoriteNote
{
    public class UpdateFavoriteNoteRequest
    {
        public UpdateFavoriteNoteRequest(int id, bool isFavorite)
        {
            this.Id = id;
            this.IsFavorite = isFavorite;
        }

        public UpdateFavoriteNoteRequest() { }

        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsFavorite { get; set; }
    }
}
