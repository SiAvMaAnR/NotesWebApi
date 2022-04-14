using Notes.Domain.Models;
using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Notes.DeleteNote;
using Notes.DTOs.Notes.GetListNote;
using Notes.DTOs.Notes.GetNote;
using Notes.DTOs.Notes.UpdateNote;
using System.Linq.Expressions;

namespace Notes.Interfaces
{
    public interface INoteService
    {
        Task<AddNoteResponse> AddNoteAsync(AddNoteRequest request);
        Task<DeleteNoteResponse> DeleteNoteAsync(DeleteNoteRequest request);
        Task<GetListNoteResponse> GetListNoteAsync(GetListNoteRequest request);
        Task<GetNoteResponse> GetNoteAsync(GetNoteRequest request);
        Task<UpdateNoteResponse> UpdateNoteAsync(UpdateNoteRequest request);
    }
}
