using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.NoSql.Mongo.Operations
{
    public class Operation<T> : IOperation<T> where T : class, new()
    {
        protected readonly MongoRepository repository;

        public Operation() {
            repository = new MongoRepository();
        }

        public virtual void Add(T entity) {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Get() {
            return repository.Get<T>();
        }

        public virtual IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate) {
            return repository.GetBy<T>(predicate);
        }

        public IEnumerable<T> GetBy(FilterDefinition<T> filter) {
            return repository.GetBy<T>(filter);
        }

        public virtual T GetByKey(object key) {
            throw new NotImplementedException();
        }

        public virtual void Remove(T entity) {
            throw new NotImplementedException();
        }

        public virtual bool Update(T entity) {
            throw new NotImplementedException();
        }
    }
}