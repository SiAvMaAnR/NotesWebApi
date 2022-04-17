using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Interfaces;
using System.Security.Claims;

namespace Notes.Services.Users
{
    public class UsersService : BaseService
    {
        public UsersService(IAsyncRepository<Note> repository, EFContext context) 
            : base(repository, context)
        {
        }
    }
}
