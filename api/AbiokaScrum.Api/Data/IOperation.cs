using System.Collections.Generic;

namespace AbiokaScrum.Api.Data
{
    public interface IOperation<T> where T : class, new()
    {
        IEnumerable<T> Get();

        T GetByKey(object key);

        void Add(T entity);

        bool Update(T entity);

        void Remove(T entity);
    }
}