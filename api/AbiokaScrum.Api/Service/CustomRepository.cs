using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Helper;
using DapperExtensions;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Service
{
    public class CustomRepository
    {
        public IRepository repository;

        public CustomRepository(IRepository repository) {
            this.repository = repository;
        }

        public void Add<T>(T entity) where T : class, new() {
            Safely.Run(() =>
            {
                repository.Add<T>(entity);
            });
        }

        public bool Update<T>(T entity) where T : class, new() {
            return Safely.Run<bool>(() =>
            {
                return repository.Update<T>(entity);
            });
        }

        public void Remove<T>(T entity) where T : class, new() {
            Safely.Run(() =>
            {
                repository.Remove<T>(entity);
            });
        }

        public IEnumerable<T> GetBy<T>(IPredicate predicate, IList<ISort> sort = null) where T : class, new() {
            return Safely.Run(() =>
            {
                return repository.GetBy<T>(predicate, sort);
            });
        }
    }
}