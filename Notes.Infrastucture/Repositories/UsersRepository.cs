using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Infrastucture.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(EFContext context) : base(context)
        {
        }
    }
}
