﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.DTOs.Service.Notes.AddNote;
using Notes.DTOs.Service.Notes.DeleteNote;
using Notes.DTOs.Service.Notes.GetNote;
using Notes.DTOs.Service.Notes.GetNotes;
using Notes.DTOs.Service.Notes.UpdateDoneNote;
using Notes.DTOs.Service.Notes.UpdateFavoriteNote;
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
                    EventDate = request.EventDate,
                    CreateDate = DateTime.Now,
                    IsFavorite = request.IsFavorite,
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

        public async Task<GetNotesResponse> GetNotesAsync(GetNotesRequest request)
        {
            IEnumerable<Note>? notes = await repository
                .GetAllAsync(note =>
                (user?.Id ?? -1) == note.UserId &&
                (request.OnlyFavorite ? note.IsFavorite : true),
                note => note.User);

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

                return new GetNotesResponse()
                {
                    Notes = result,
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber,
                    TotalNotes = totalNotes,
                    TotalPages = tatalPages,
                };
            }

            return new GetNotesResponse()
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
                note.EventDate = request.EventDate;
                note.IsDone = request.IsDone;

                await repository.UpdateAsync(note);

                return new UpdateNoteResponse(true);
            }

            return new UpdateNoteResponse(false);
        }

        public async Task<UpdateDoneNoteResponse> UpdateDoneNoteAsync(UpdateDoneNoteRequest request)
        {
            Note? note = await repository.GetAsync(note => note.Id == request.Id && user!.Id == note.UserId);

            if (note != null)
            {
                note.IsDone = request.IsDone;

                await repository.UpdateAsync(note);

                return new UpdateDoneNoteResponse(true);
            }

            return new UpdateDoneNoteResponse(false);
        }

        public async Task<UpdateFavoriteNoteResponse> UpdateFavoriteNoteAsync(UpdateFavoriteNoteRequest request)
        {
            Note? note = await repository.GetAsync(note => note.Id == request.Id && user!.Id == note.UserId);

            if (note != null)
            {
                note.IsFavorite = request.IsFavorite;

                await repository.UpdateAsync(note);

                return new UpdateFavoriteNoteResponse(true);
            }

            return new UpdateFavoriteNoteResponse(false);
        }
    }
}
