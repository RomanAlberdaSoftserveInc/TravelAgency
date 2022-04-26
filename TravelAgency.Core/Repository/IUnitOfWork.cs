using System;
using System.Data;

namespace Quorum.OnDemand.Importer.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}