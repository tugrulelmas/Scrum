using AbiokaScrum.Api.Helper;
using DapperExtensions;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Data.Transactional.Dapper.Operations
{
    public class Operation<T> : IOperation<T> where T : class, new()
    {
        public virtual void Add(T entity) {
            Safely.Run(() =>
            {
                using (IUnitOfWork unitOfWork = GetUnitOfWork())
                {
                    unitOfWork.Repository.Add(entity);
                }
            });
        }

        public virtual IEnumerable<T> Get() {
            return Safely.Run<IEnumerable<T>>(() =>
            {
                IEnumerable<T> result = null;
                using (IUnitOfWork unitOfWork = GetUnitOfWork())
                {
                    result = unitOfWork.Repository.GetAll<T>();
                }
                return result;
            });
        }

        public virtual T GetByKey(object key) {
            return Safely.Run<T>(() =>
            {
                T result = null;
                using (IUnitOfWork unitOfWork = GetUnitOfWork())
                {
                    result = unitOfWork.Repository.GetByKey<T>(key);
                }
                return result;
            });
        }

        public virtual void Remove(T entity) {
            Safely.Run(() =>
            {
                using (IUnitOfWork unitOfWork = GetUnitOfWork())
                {
                    unitOfWork.Repository.Remove<T>(entity);
                }
            });
        }

        public virtual bool Update(T entity) {
            return Safely.Run(() =>
            {
                var result = false;
                using (IUnitOfWork unitOfWork = GetUnitOfWork())
                {
                    result = unitOfWork.Repository.Update<T>(entity);
                }
                return result;
            });
        }

        public virtual IEnumerable<T> GetBy(IPredicate predicate, IList<ISort> sort = null) {
            return Safely.Run(() =>
            {
                IEnumerable<T> result = null;
                using (IUnitOfWork unitOfWork = GetUnitOfWork())
                {
                    result = unitOfWork.Repository.GetBy<T>(predicate, sort);
                }
                return result;
            });
        }

        public virtual void Execute(Action<IRepository> action) {
            Safely.Run(() =>
            {
                using (IUnitOfWork unitOfWork = GetUnitOfWork())
                {
                    unitOfWork.BeginTransaction();
                    action(unitOfWork.Repository);
                    unitOfWork.CommitTransaction();
                }
            });
        }

        private IUnitOfWork GetUnitOfWork() {
            return new DapperUnitOfWork();
        }
    }
}