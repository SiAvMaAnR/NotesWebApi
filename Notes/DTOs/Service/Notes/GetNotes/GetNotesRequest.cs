using Notes.Domain.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Notes.DTOs.Service.Notes.GetNotes
{
    public class GetNotesRequest
    {
        public GetNotesRequest(int pageNumber, int pageSize, string sort, bool onlyFavorite)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Sort = sort;
            OnlyFavorite = onlyFavorite;
        }

        public GetNotesRequest() { }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; } = "asc_date";
        public bool OnlyFavorite { get; set; }
    }
}
