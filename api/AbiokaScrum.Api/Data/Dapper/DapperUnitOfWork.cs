using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Data.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        //TODO: encrypt tihs connection string
        private const string connectionString = "Data Source=.\\SQLEXPRESS;User Id=sa;Password=sapass;Initial Catalog=Scrum;";
        private IDbConnection connection;
        private DapperRepository repository;
        private IDbTransaction transaction;

        public DapperUnitOfWork() {
            connection = new SqlConnection(connectionString);
            if (connection.State == ConnectionState.Closed) {
                connection.Open();
            }
            repository = new DapperRepository(connection);
        }

        public IRepository Repository
        {
            get
            {
                return repository;
            }
        }

        public void BeginTransaction() {
            transaction = connection.BeginTransaction();
            repository.Transaction = transaction;
        }

        public void CommitTransaction() {
            if (transaction == null) {
                throw new Exception("No transaction available");
            }

            transaction.Commit();
            transaction = null;
            repository.Transaction = null;
        }

        public void Dispose() {
            if (transaction != null) {
                transaction.Rollback();
                transaction.Dispose();

                transaction = null;
                repository.Transaction = null;
            }

            connection.Dispose();
        }

        public void RollbackTransaction() {
            if (transaction == null) {
                throw new Exception("No transaction available");
            }

            transaction.Rollback();
            transaction = null;
            repository.Transaction = null;
        }
    }
}