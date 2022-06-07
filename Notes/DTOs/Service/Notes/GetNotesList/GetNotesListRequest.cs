using Notes.Domain.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Notes.DTOs.Service.Notes.GetNotesList
{
    public class GetNotesListRequest
    {
        public GetNotesListRequest(int pageNumber, int pageSize, string sort)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Sort = sort;
        }

        public GetNotesListRequest() { }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; } = "asc_date";
    }
}
