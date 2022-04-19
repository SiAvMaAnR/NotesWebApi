using Notes.Domain.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Notes.DTOs.Notes.GetListNote
{
    public class GetListNoteRequest
    {
        public GetListNoteRequest(int pageNumber, int pageSize, string sort, ClaimsPrincipal claimsPrincipal)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Sort = sort;
            User = claimsPrincipal;
        }

        public GetListNoteRequest() { }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; } = "asc_date";
        public ClaimsPrincipal User { get; set; }
    }
}
