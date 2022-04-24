using Notes.Domain.Models;
using Notes.DTOs.Service.Notes.AddNote;
using Notes.DTOs.Service.Notes.DeleteNote;
using Notes.DTOs.Service.Notes.GetNote;
using Notes.DTOs.Service.Notes.GetNotesList;
using Notes.DTOs.Service.Notes.UpdateNote;
using System.Linq.Expressions;

namespace Notes.Interfaces
{
    public interface INoteService
    {
        Task<AddNoteResponse> AddNoteAsync(AddNoteRequest request);
        Task<DeleteNoteResponse> DeleteNoteAsync(DeleteNoteRequest request);
        Task<GetNotesListResponse> GetNotesListAsync(GetNotesListRequest request);
        Task<GetNoteResponse> GetNoteAsync(GetNoteRequest request);
        Task<UpdateNoteResponse> UpdateNoteAsync(UpdateNoteRequest request);
    }
}
