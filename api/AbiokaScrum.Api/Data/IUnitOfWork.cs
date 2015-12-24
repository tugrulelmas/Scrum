using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repository { get; }

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}