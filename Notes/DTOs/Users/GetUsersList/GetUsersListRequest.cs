namespace Notes.DTOs.Users.GetUsersList
{
    public class GetUsersListRequest
    {
        public GetUsersListRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public GetUsersListRequest() { }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
