using Microsoft.EntityFrameworkCore;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        private readonly EFContext context;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(EFContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
