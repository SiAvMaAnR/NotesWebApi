namespace Notes.DTOs.Service.Account.Edit
{
    public class EditResponse
    {
        public EditResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public EditResponse() { }
        public bool IsSuccess { get; set; } = false;
    }
}
