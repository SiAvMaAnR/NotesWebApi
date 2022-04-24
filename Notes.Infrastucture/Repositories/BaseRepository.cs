using Microsoft.EntityFrameworkCore;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Extentions;
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

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Get entity with include
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await dbSet.MultipleInclude(includeProperties)
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Get list entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(dbSet
                .AsNoTracking()
                .Where(predicate));
        }

        /// <summary>
        /// Get list entities
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            return await Task.FromResult(dbSet.AsNoTracking());
        }

        /// <summary>
        /// Get list with include
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>?> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Task.FromResult(dbSet
                .MultipleInclude(includeProperties)
                .AsNoTracking());
        }

        /// <summary>
        /// Get list with include
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>?> GetAllAsync(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Task.FromResult(dbSet
                .MultipleInclude(includeProperties)
                .AsNoTracking()
                .Where(predicate));
        }
    }
}
