using System.Security.Claims;

namespace Notes.DTOs.Notes.DeleteNote
{
    public class DeleteNoteRequest
    {
        public DeleteNoteRequest(ClaimsPrincipal user, int id)
        {
            User = user;
            Id = id;
        }
        public DeleteNoteRequest() { }

        public ClaimsPrincipal User { get; set; }
        public int Id { get; set; }
    }
}
