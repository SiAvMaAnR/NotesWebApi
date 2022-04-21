namespace Notes.DTOs.Notes.DeleteNote
{
    public class DeleteNoteResponse
    {
        public DeleteNoteResponse(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
        public DeleteNoteResponse() { }

        public bool IsDeleted { get; set; }
    }
}
