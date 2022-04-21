using Notes.Domain.Enums;
using Notes.Domain.Models;

namespace Notes.DTOs.Notes.GetNotesList
{
    public class GetNotesListResponse
    {
        public IEnumerable<Note>? Notes { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNotes { get; set; }
        public int TotalPages { get; set; }
    }
}
