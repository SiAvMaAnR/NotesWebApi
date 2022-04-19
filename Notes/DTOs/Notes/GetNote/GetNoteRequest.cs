using System.Security.Claims;

namespace Notes.DTOs.Notes.GetNote
{
    public class GetNoteRequest
    {
        public GetNoteRequest(int id, ClaimsPrincipal claimsPrincipal)
        {
            Id = id;
            User = claimsPrincipal;
        }

        public GetNoteRequest() { }

        public int Id { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
}
