namespace Notes.DTOs.Service.Notes.UpdateFavoriteNote
{
    public class UpdateFavoriteNoteResponse
    {
        public UpdateFavoriteNoteResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public UpdateFavoriteNoteResponse() { }

        public bool IsSuccess { get; set; }
    }
}
