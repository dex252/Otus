using SampleDb.Models.Entity;

namespace SampleDb.Repositories.Db
{
    public interface IDbRepository
    {
        /// <summary>
        /// Вывести содержимое таблицы
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync<T>() 
            where T : BaseEntity;

        /// <summary>
        /// Добавить поле в таблицу
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync<T>(T entity) 
            where T : BaseEntity;

        /// <summary>
        /// Подписать объект from на объекта to (создать между ними связь)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="from">кто подписывается</param>
        /// <param name="to">на кого подписывается</param>
        /// <returns></returns>
        Task<bool> SubscribeAsync<T, K>(Guid from, Guid to)
             where T : BaseEntity
             where K : BaseEntity;
    }
}
