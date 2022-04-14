using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Notes.DeleteNote;
using Notes.DTOs.Notes.GetListNote;
using Notes.DTOs.Notes.GetNote;
using Notes.DTOs.Notes.UpdateNote;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Interfaces;
using Notes.Interfaces;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Notes.Services
{
    public class NotesService : BaseService, INoteService
    {
        public NotesService(IAsyncRepository<Note> repository, EFContext context)
            : base(repository, context)
        {

        }

        public async Task<AddNoteResponse> AddNoteAsync(AddNoteRequest request)
        {
            string currentEmail = request.User.FindFirst(ClaimTypes.Email)?.Value ?? "";

            User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == currentEmail);

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
                    Response = "Success",
                    StatusCode = TStatusCodes.OK
                };
            }
            else
            {
                return new AddNoteResponse()
                {
                    Note = null,
                    Response = "User not found!",
                    StatusCode = TStatusCodes.Bad_Request
                };
            }
        }

        public async Task<DeleteNoteResponse> DeleteNoteAsync(DeleteNoteRequest request)
        {
            return new DeleteNoteResponse()
            {

            };
        }

        public async Task<GetListNoteResponse> GetListNoteAsync(GetListNoteRequest request)
        {
            IEnumerable<Note>? result = (await repository.GetAllAsync())?
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize);

            if (result != null)
            {
                var totalNotes = context.Notes.ToList().Count;
                var tatalPages = (int)Math.Ceiling(((decimal)totalNotes / request.PageSize));

                return new GetListNoteResponse()
                {
                    Notes = result,
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber,
                    TotalNotes = totalNotes,
                    TotalPages = tatalPages,
                    Response = "Success",
                    StatusCode = TStatusCodes.OK
                };
            }
            else
            {
                return new GetListNoteResponse()
                {
                    Notes = null,
                    Response = "Notes not found!",
                    StatusCode = TStatusCodes.Bad_Request
                };
            }

        }

        public async Task<GetNoteResponse> GetNoteAsync(GetNoteRequest request)
        {
            return new GetNoteResponse()
            {

            };
        }

        public async Task<UpdateNoteResponse> UpdateNoteAsync(UpdateNoteRequest request)
        {
            return new UpdateNoteResponse()
            {

            };
        }
    }
}
