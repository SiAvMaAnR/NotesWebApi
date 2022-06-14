using Notes.Domain.Models;
using Notes.DTOs.Service.Notes.AddNote;
using Notes.DTOs.Service.Notes.DeleteNote;
using Notes.DTOs.Service.Notes.GetNote;
using Notes.DTOs.Service.Notes.GetNotesList;
using Notes.DTOs.Service.Notes.UpdateDoneNote;
using Notes.DTOs.Service.Notes.UpdateNote;

namespace Notes.Interfaces
{
    public interface INoteService : IBaseService
    {
        Task<AddNoteResponse> AddNoteAsync(AddNoteRequest request);
        Task<DeleteNoteResponse> DeleteNoteAsync(DeleteNoteRequest request);
        Task<GetNotesListResponse> GetNotesListAsync(GetNotesListRequest request);
        Task<GetNoteResponse> GetNoteAsync(GetNoteRequest request);
        Task<UpdateNoteResponse> UpdateNoteAsync(UpdateNoteRequest request);
        Task<UpdateDoneNoteResponse> UpdateDoneNoteAsync(UpdateDoneNoteRequest request);
    }
}
