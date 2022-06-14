namespace Notes.DTOs.Service.Notes.UpdateDoneNote
{
    public class UpdateDoneNoteResponse
    {
        public UpdateDoneNoteResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public UpdateDoneNoteResponse() { }

        public bool IsSuccess { get; set; }
    }
}
