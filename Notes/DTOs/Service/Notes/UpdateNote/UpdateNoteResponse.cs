namespace Notes.DTOs.Service.Notes.UpdateNote
{
    public class UpdateNoteResponse
    {
        public UpdateNoteResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public UpdateNoteResponse() { }

        public bool IsSuccess { get; set; }
    }
}
