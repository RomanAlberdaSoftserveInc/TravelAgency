using Microsoft.Extensions.Configuration;
using Quorum.OnDemand.Importer.Core.Repository;
using System.Data;
using System.Data.SqlClient;

namespace TravelAgency.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDbConnection _connection;
        protected IDbTransaction _transaction;

        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("LocalDbConnection"));
        }
        public IDbConnection Connection
        {
            get
            {
                return this._connection;
            }
        }
        public IDbTransaction Transaction
        {
            get
            {
                return this._transaction;
            }
        }
        public virtual void Begin()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }
        public virtual void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
            }
            Dispose();
        }

        public virtual void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
            Dispose();
        }

        public virtual void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _transaction = null;

            if (_connection != null)
            {
                _connection.Dispose();
            }
            _connection = null;
        }
    }
}
