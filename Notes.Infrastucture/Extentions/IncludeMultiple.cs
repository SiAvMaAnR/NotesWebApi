using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Infrastucture.Extentions
{
    public static class QueryableExtention
    {
        public static IQueryable<TEntity> MultipleInclude<TEntity>(this DbSet<TEntity> dbSet, 
            params Expression<Func<TEntity, object>>[] includeProperties) 
            where TEntity : class
        {
            return includeProperties.Aggregate(dbSet.AsNoTracking(),
                (current, includeProperty) => current.Include(includeProperty));
        }

    }
}
