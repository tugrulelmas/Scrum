using System;

namespace AbiokaScrum.Api.Data.Transactional
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repository { get; }

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}