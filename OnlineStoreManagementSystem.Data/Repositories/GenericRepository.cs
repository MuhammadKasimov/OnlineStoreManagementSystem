using Microsoft.EntityFrameworkCore;
using OnlineStoreManagementSystem.Contexts;
using OnlineStoreManagementSystem.Data.IRepositories;
using OnlineStoreManagementSystem.Domain.Commons;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Auditable
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true)
        {
            var query = expression is null ? dbSet : dbSet.Where(expression);

            if (includes != null)
                foreach (var include in includes)
                    if (!string.IsNullOrEmpty(include))
                        query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, bool isTracking = true, string[] includes = null) =>
            await GetAll(expression, includes, false).FirstOrDefaultAsync();

        public async ValueTask<T> CreateAsync(T entity) =>
            (await dbContext.AddAsync(entity)).Entity;

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var entity = await GetAsync(e => e.Id == id);

            if (entity == null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public T Update(T entity) =>
            dbSet.Update(entity).Entity;

        public async ValueTask SaveChangesAsync() =>
            await dbContext.SaveChangesAsync();
    }
}
