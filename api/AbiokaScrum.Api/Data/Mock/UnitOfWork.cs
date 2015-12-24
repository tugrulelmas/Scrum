using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Data.Mock
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepository repository;

        public UnitOfWork() {
            repository = new Repository();
        }

        public IRepository Repository {
            get { return repository; }
        }

        public void BeginTransaction() {

        }

        public void CommitTransaction() {

        }

        public void RollbackTransaction() {

        }

        public void Dispose() {
            //CommitTransaction();
        }
    }
}