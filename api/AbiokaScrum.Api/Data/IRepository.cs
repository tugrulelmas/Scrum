using DapperExtensions;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Data
{
    public interface IRepository
    {
        /// <summary>
        /// Add an entity
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entity">Entity</param>
        void Add<T>(T entity) where T : class, new();

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        bool Update<T>(T entity) where T : class, new();

        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        bool Remove<T>(T entity) where T : class, new();

        /// <summary>
        /// Get an entity with key
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        T GetByKey<T>(object key) where T : class, new();

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entity">Entity</param>
        IEnumerable<T> GetAll<T>(IPredicate predicate = null, IList<ISort> sort = null) where T : class, new();

        /// <summary>
        /// Get entities with predicate and order
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <returns>A sequence of data of the supplied type and returns max. 2000 records.</returns>
        IEnumerable<T> GetBy<T>(IPredicate predicate, object order = null) where T : class, new();
    }
}
