using Notes.Domain.Models;
using Notes.DTOs.Notes.AddNote;
using Notes.DTOs.Notes.DeleteNote;
using Notes.DTOs.Notes.GetListNote;
using Notes.DTOs.Notes.GetNote;
using Notes.DTOs.Notes.UpdateNote;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Security;
using Notes.Infrastucture.Interfaces;
using Notes.Interfaces;
using System.Security.Claims;

namespace Notes.Services
{
    public class BaseService
    {
        protected readonly IAsyncRepository<Note> repository;
        protected readonly EFContext context;

        public BaseService(IAsyncRepository<Note> repository, EFContext context)
        {
            this.repository = repository;
            this.context = context;
        }
    }
}
