using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Interfaces;
using Notes.Infrastucture.Security;

namespace Notes.Services
{
    public class BaseService<TEntity> where TEntity : class
    {
        protected readonly IAsyncRepository<TEntity> repository;
        protected readonly EFContext context;
        protected readonly User? user;

        public BaseService(IAsyncRepository<TEntity> repository, EFContext context, IHttpContextAccessor httpContext)
        {
            this.repository = repository;
            this.context = context;
            this.user = CurrentUser.GetUser(context, httpContext.HttpContext!.User);
        }

        public User? User
        {
            get => user;
        }
    }
}
