using Microsoft.EntityFrameworkCore;
using Npgsql;
using SampleDb.Managers.Db;
using SampleDb.Models.Entity;

namespace SampleDb.Repositories.Db
{
    public class EFRepository : IDbRepository
    {
        private DbContext DbContext { get; }
        private IDbParametersCreator DbParametersCreator { get; }

        private const string SQL_TEMPLATE = "INSERT INTO {0} VALUES (@to, @from)";

        public EFRepository(DbContext dbContext, IDbParametersCreator dbParametersCreator)
        {
            DbContext = dbContext;
            DbParametersCreator = dbParametersCreator;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() 
            where T : BaseEntity
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> AddAsync<T>(T entity) 
            where T : BaseEntity
        {
            await DbContext.Set<T>().AddAsync(entity);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SubscribeAsync<T, K>(Guid from, Guid to)
            where T : BaseEntity
            where K : BaseEntity
        {
            var subscribe = await DbContext.Set<K>().FirstOrDefaultAsync(e => e.Id == to);
            if (subscribe == null)
            {
                throw new Exception($"Подписка с идентификатором {to} не найдена");
            }

            var subscriber = await DbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == from);
            if (subscriber == null)
            {
                throw new Exception($"Подписчик с идентификатором {from} не найден");
            }

            var tableName = $"{typeof(K).Name.ToLower()}_{typeof(T).Name.ToLower()}";
            var sqlTemplate = string.Format(SQL_TEMPLATE, tableName);

            var fromParam = DbParametersCreator.GetParameter("from", from);
            var toParam = DbParametersCreator.GetParameter("to", to);
            
            var isSuccess = await DbContext.Database.ExecuteSqlRawAsync(sqlTemplate, fromParam, toParam) > 0;
            return isSuccess;
        }
    }
}
