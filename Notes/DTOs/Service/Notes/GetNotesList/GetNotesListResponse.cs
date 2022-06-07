using Notes.Domain.Enums;
using Notes.Domain.Models;

namespace Notes.DTOs.Service.Notes.GetNotesList
{
    public class GetNotesListResponse
    {
        public GetNotesListResponse(IEnumerable<Note>? notes, int pageNumber, int pageSize, int totalNotes, int totalPages)
        {
            Notes = notes;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalNotes = totalNotes;
            TotalPages = totalPages;
        }

        public GetNotesListResponse() { }

        public IEnumerable<Note>? Notes { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNotes { get; set; }
        public int TotalPages { get; set; }
    }
}
