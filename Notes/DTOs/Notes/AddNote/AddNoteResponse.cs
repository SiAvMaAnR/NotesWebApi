using Notes.Domain.Enums;
using Notes.Domain.Models;

namespace Notes.DTOs.Notes.AddNote
{
    public class AddNoteResponse
    {
        public Note? Note { get; set; }
        public string Response { get; set; }
        public TStatusCodes StatusCode { get; set; }
    }
}
