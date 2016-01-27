using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using DapperExtensions;

namespace AbiokaScrum.Api.Data.Mock
{
    public class Repository : IRepository
    {
        public void Add<T>(T entity) where T : class, new() {
            
        }

        public bool Update<T>(T entity) where T : class, new() {
            return true;
        }

        public bool Remove<T>(T entity) where T : class, new() {
            return true;
        }

        public T GetByKey<T>(object key) where T : class, new() {
            return GetCollection<T>().GetByKey(key);
        }

        public IEnumerable<T> GetAll<T>() where T : class, new() {
            return GetCollection<T>().GetAll();
        }

        public IEnumerable<T> GetBy<T>(IPredicate predicate, IList<ISort> sort = null) where T : class, new() {
            throw new NotImplementedException();
        }

        private CollectionBase<T> GetCollection<T>() where T : class, new() {
            if (typeof(T) == typeof(List)) {
                return new ListCollection() as CollectionBase<T>;
            } else if (typeof(T) == typeof(Label)) {
                return new LabelCollection() as CollectionBase<T>;
            } else if (typeof(T) == typeof(Board)) {
                return new BoardCollection() as CollectionBase<T>;
            } else if (typeof(T) == typeof(User)) {
                return new UserCollection() as CollectionBase<T>;
            } else if (typeof(T) == typeof(Card)) {
                return new CardCollection() as CollectionBase<T>;
            }
            throw new NotSupportedException();
        }
    }
}