using Notes.Domain.Enums;
using Notes.Domain.Models;

namespace Notes.DTOs.Service.Notes.GetNotes
{
    public class GetNotesResponse
    {
        public GetNotesResponse(IEnumerable<Note>? notes, int pageNumber, int pageSize, int totalNotes, int totalPages)
        {
            Notes = notes;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalNotes = totalNotes;
            TotalPages = totalPages;
        }

        public GetNotesResponse() { }

        public IEnumerable<Note>? Notes { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNotes { get; set; }
        public int TotalPages { get; set; }
    }
}
