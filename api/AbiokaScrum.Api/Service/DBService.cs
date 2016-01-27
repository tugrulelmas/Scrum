using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Helper;
using DapperExtensions;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Service
{
    public class DBService
    {
        public static IEnumerable<T> Get<T>() where T : class, new() {
            return Safely.Run<IEnumerable<T>>(() =>
            {
                IEnumerable<T> result = null;
                using (IUnitOfWork unitOfWork = GetUnitOfWork()) {
                    result = unitOfWork.Repository.GetAll<T>();
                }
                return result;
            });
        }

        public static T GetByKey<T>(object key) where T : class, new() {
            return Safely.Run<T>(() =>
            {
                T result = null;
                using (IUnitOfWork unitOfWork = GetUnitOfWork()) {
                    result = unitOfWork.Repository.GetByKey<T>(key);
                }
                return result;
            });
        }

        public static void Add<T>(T entity) where T : class, new() {
            using (IUnitOfWork unitOfWork = GetUnitOfWork()) {
                var crudService = new CustomRepository(unitOfWork.Repository);
                crudService.Add<T>(entity);
            }
        }

        public static bool Update<T>(T entity) where T : class, new() {
            var result = false;
            using (IUnitOfWork unitOfWork = GetUnitOfWork()) {
                var crudService = new CustomRepository(unitOfWork.Repository);
                result = crudService.Update<T>(entity);
            }
            return result;
        }

        public static void Remove<T>(T entity) where T : class, new() {
            using (IUnitOfWork unitOfWork = GetUnitOfWork()) {
                var crudService = new CustomRepository(unitOfWork.Repository);
                crudService.Remove<T>(entity);
            }
        }

        public static IEnumerable<T> GetBy<T>(IPredicate predicate) where T : class, new() {
            return Safely.Run<IEnumerable<T>>(() =>
            {
                IEnumerable<T> result = null;
                using (IUnitOfWork unitOfWork = DBService.GetUnitOfWork()) {
                    var crudService = new CustomRepository(unitOfWork.Repository);
                    result = crudService.GetBy<T>(predicate);
                }
                return result;
            });
        }

        public static void Execute(Action<CustomRepository> action) {
            Safely.Run(() =>
            {
                using (IUnitOfWork unitOfWork = GetUnitOfWork()) {
                    var customRepository = new CustomRepository(unitOfWork.Repository);
                    unitOfWork.BeginTransaction();
                    action(customRepository);
                    unitOfWork.CommitTransaction();
                }
            });
        }

        internal static IUnitOfWork GetUnitOfWork() {
            return new Data.Dapper.DapperUnitOfWork();
            //return DependencyContainer.Container.Resolve<IUnitOfWork>();
        }
    }
}