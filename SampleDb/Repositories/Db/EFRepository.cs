using Microsoft.EntityFrameworkCore;
using SampleDb.Models.Entity;

namespace SampleDb.Repositories.Db
{
    public class EFRepository<T> : IDbRepository<T> where T : BaseEntity
    {
        private DbContext DbContext { get; }

        public EFRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            return await DbContext.SaveChangesAsync() > 0;
        }

    }
}
