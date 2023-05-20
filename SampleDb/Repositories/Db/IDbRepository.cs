using SampleDb.Models.Entity;

namespace SampleDb.Repositories.Db
{
    public interface IDbRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Вывести содержимое всех таблиц
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Добавить поле в таблицу
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(T entity);
    }
}
