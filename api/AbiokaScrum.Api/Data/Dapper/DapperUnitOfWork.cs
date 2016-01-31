using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AbiokaScrum.Api.Data.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly static string connectionString;
        private IDbConnection connection;
        private DapperRepository repository;
        private IDbTransaction transaction;

        static DapperUnitOfWork() {
            //TODO: encrypt tihs connection string
            var connectionStringSetting = WebConfigurationManager.ConnectionStrings["AbiokaConnectionString"];
            if (connectionStringSetting == null)
                throw new ArgumentNullException("ConnectionString");

            connectionString = connectionStringSetting.ConnectionString;
        }

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