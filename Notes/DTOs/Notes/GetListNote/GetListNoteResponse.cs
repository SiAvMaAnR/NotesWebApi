using Notes.Domain.Enums;
using Notes.Domain.Models;

namespace Notes.DTOs.Notes.GetListNote
{
    public class GetListNoteResponse
    {
        public IEnumerable<Note>? Notes { get; set; }
        public string Response { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNotes { get; set; }
        public int TotalPages { get; set; }
        public TStatusCodes StatusCode { get; set; }
    }
}
