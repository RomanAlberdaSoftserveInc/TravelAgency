using System.Collections.Generic;
using System.Threading.Tasks;
using Quorum.OnDemand.Importer.Core.Repository;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;
using Dapper;
using System.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class TransportRepository : ITransportRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> AddAsync(Transport entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(Transport entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Transport>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Transport> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Transport>> GetCarTransports()
        {
            return await _unitOfWork.Connection.QueryAsync<Transport>("dbo.spGetCarTransport", commandType: CommandType.StoredProcedure);
        }
    }
}
