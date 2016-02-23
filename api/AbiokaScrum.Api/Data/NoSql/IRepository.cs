using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.NoSql
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
        IEnumerable<T> Get<T>() where T : class, new();

        IEnumerable<T> GetBy<T>(Expression<Func<T, bool>> predicate) where T : class, new();
    }
}