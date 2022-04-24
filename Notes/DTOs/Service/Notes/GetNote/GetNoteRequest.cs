using System.Security.Claims;

namespace Notes.DTOs.Service.Notes.GetNote
{
    public class GetNoteRequest
    {
        public GetNoteRequest(int id)
        {
            Id = id;
        }

        public GetNoteRequest() { }

        public int Id { get; set; }
    }
}
