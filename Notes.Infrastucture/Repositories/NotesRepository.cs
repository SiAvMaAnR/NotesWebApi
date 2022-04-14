using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Repositories;

namespace Notes.Infrastucture.Repositories
{
    public class NotesRepository : BaseRepository<Note>
    {
        public NotesRepository(EFContext context) : base(context)
        {
        }
    }
}
