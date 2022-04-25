using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs.Service.Notes.AddNote;
using Notes.DTOs.Service.Notes.DeleteNote;
using Notes.DTOs.Service.Notes.GetNote;
using Notes.DTOs.Service.Notes.GetNotesList;
using Notes.DTOs.Service.Notes.UpdateNote;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Security;
using Notes.Infrastucture.Interfaces;
using Notes.Infrastucture.Security;
using Notes.Interfaces;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Notes.Services.Notes
{
    public class NotesService : BaseService<Note>, INoteService
    {
        public NotesService(IAsyncRepository<Note> repository, EFContext context, IHttpContextAccessor httpContext, IConfiguration configuration)
            : base(repository, context, httpContext, configuration)
        {
        }

        public async Task<AddNoteResponse> AddNoteAsync(AddNoteRequest request)
        {
            if (user != null)
            {
                Note note = new Note()
                {
                    Title = request.Title,
                    Description = request.Description,
                    IsDone = request.IsDone,
                    CreateDate = DateTime.Now,
                    User = user
                };

                await repository.AddAsync(note);

                return new AddNoteResponse()
                {
                    Note = note,
                };
            }

            return new AddNoteResponse()
            {
                Note = null,
            };
        }

        public async Task<DeleteNoteResponse> DeleteNoteAsync(DeleteNoteRequest request)
        {
            Note? note = await repository.GetAsync(note =>
                note.Id == request.Id &&
                user!.Id == note.UserId);

            if (note != null)
            {
                await repository.DeleteAsync(note);
                return new DeleteNoteResponse(true);
            }

            return new DeleteNoteResponse(false);
        }

        public async Task<GetNotesListResponse> GetNotesListAsync(GetNotesListRequest request)
        {
            IEnumerable<Note>? notes = await repository
                .GetAllAsync(note => user!.Id == note.UserId, note => note.User);

            if (notes != null)
            {
                switch (request.Sort)
                {
                    case "asc_date":
                        notes = notes?.OrderBy(note => note.CreateDate);
                        break;
                    case "desc_date":
                        notes = notes?.OrderByDescending(note => note.CreateDate);
                        break;
                    default:
                        notes = notes?.OrderByDescending(note => note.CreateDate);
                        break;
                };

                var totalNotes = notes?.ToList().Count ?? 0;

                var result = notes?
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize);

                var tatalPages = (int)Math.Ceiling(((decimal)totalNotes / request.PageSize));

                return new GetNotesListResponse()
                {
                    Notes = result,
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber,
                    TotalNotes = totalNotes,
                    TotalPages = tatalPages,
                };
            }

            return new GetNotesListResponse()
            {
                Notes = null,
            };
        }

        public async Task<GetNoteResponse> GetNoteAsync(GetNoteRequest request)
        {
            Note? note = await repository.GetAsync(
                note => note.Id == request.Id && user!.Id == note.UserId,
                note => note.User);

            return new GetNoteResponse()
            {
                Note = note
            };
        }

        public async Task<UpdateNoteResponse> UpdateNoteAsync(UpdateNoteRequest request)
        {
            Note? note = await repository.GetAsync(note => note.Id == request.Id && user!.Id == note.UserId);

            if (note != null)
            {
                note.Title = request.Title;
                note.Description = request.Description;
                note.IsDone = request.IsDone;

                await repository.UpdateAsync(note);

                return new UpdateNoteResponse(true);
            }

            return new UpdateNoteResponse(false);
        }
    }
}
