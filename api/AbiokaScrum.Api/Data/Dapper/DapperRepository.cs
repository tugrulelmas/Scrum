using AbiokaScrum.Api.Entities;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace AbiokaScrum.Api.Data.Dapper
{
    public class DapperRepository : IRepository
    {
        private IDbConnection dbConnection;

        public DapperRepository(IDbConnection dbConnection) {
            this.dbConnection = dbConnection;
        }

        public IDbTransaction Transaction { get; set; }

        public void Add<T>(T entity) where T : class, new() {
            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(T))) {
                ((IDeletableEntity)entity).IsDeleted = false;
            }
            if (typeof(IBaseEntity).IsAssignableFrom(typeof(T))) {
                ((IBaseEntity)entity).CreateDate = DateTime.Now;
            }

            dbConnection.Insert<T>(entity, transaction: Transaction);
        }

        public IEnumerable<T> GetAll<T>() where T : class, new() {
            IPredicate predicate = null;
            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(T))) {
                predicate = Predicates.Field<T>(f => ((IDeletableEntity)f).IsDeleted, Operator.Eq, false);
            }

            return dbConnection.GetList<T>(predicate: predicate, transaction: Transaction);
        }

        public IEnumerable<T> GetBy<T>(IPredicate predicate, IList<ISort> sort = null) where T : class, new() {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            if (predicate != null) {
                pg.Predicates.Add(predicate);
            }

            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(T))) {
                pg.Predicates.Add(Predicates.Field<T>(f => ((IDeletableEntity)f).IsDeleted, Operator.Eq, false));
            }

            return dbConnection.GetList<T>(predicate: pg, sort: sort, transaction: Transaction);
        }

        public T GetByKey<T>(object key) where T : class, new() {
            return dbConnection.Get<T>(key, transaction: Transaction);
        }

        public bool Remove<T>(T entity) where T : class, new() {
            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(T))) {
                ((IDeletableEntity)entity).IsDeleted = true;
                return Update<T>(entity);
            }
            else {
                return dbConnection.Delete<T>(entity, transaction: Transaction);
            }
        }

        public bool Update<T>(T entity) where T : class, new() {
            return dbConnection.Update<T>(entity, transaction: Transaction);
        }
    }
}