using System.ComponentModel.DataAnnotations;

namespace Notes.DTOs.Service.Notes.UpdateDoneNote
{
    public class UpdateDoneNoteRequest
    {
        public UpdateDoneNoteRequest(int id, bool isDone)
        {
            this.Id = id;
            this.IsDone = isDone;
        }

        public UpdateDoneNoteRequest() { }

        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsDone { get; set; }
    }
}
