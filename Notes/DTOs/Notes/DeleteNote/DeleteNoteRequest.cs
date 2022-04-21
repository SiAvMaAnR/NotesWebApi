using System.Security.Claims;

namespace Notes.DTOs.Notes.DeleteNote
{
    public class DeleteNoteRequest
    {
        public DeleteNoteRequest(int id)
        {
            Id = id;
        }
        public DeleteNoteRequest() { }
        public int Id { get; set; }
    }
}
