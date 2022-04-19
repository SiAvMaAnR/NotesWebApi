namespace Notes.DTOs.Notes.UpdateNote
{
    public class UpdateNoteResponse
    {
        public UpdateNoteResponse(bool isUpdate)
        {
            IsUpdate = isUpdate;
        }
        public UpdateNoteResponse() { }

        public bool IsUpdate { get; set; }
    }
}
