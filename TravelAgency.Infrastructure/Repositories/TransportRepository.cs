using System.Collections.Generic;
using System.Threading.Tasks;
using Quorum.OnDemand.Importer.Core.Repository;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;
using Dapper;
using System.Data;
using System.Linq;

namespace TravelAgency.Infrastructure.Repositories
{
    public class TransportRepository : ITransportRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(Transport entity)
        {
            var sql = @"INSERT INTO tblTransport 
                        (type,
                        model,
                        number)
                 VALUES (@Type,
                        @Model,
                        @Number)";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }

        public async Task<bool> DeleteAsync(Transport entity)
        {
            var id = entity?.Id;
            var sql = @"DELETE FROM tblTransport WHERE id = @id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { id }, _unitOfWork.Transaction) > 0;
        }

        public async Task<IReadOnlyList<Transport>> GetAllAsync()
        {
            var sql = "SELECT * FROM tblTransport";
            var transports = await _unitOfWork.Connection.QueryAsync<Transport>(sql, _unitOfWork.Transaction);
            return transports.ToList();
        }

        public async Task<Transport> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM tblTransport WHERE id = @id";
            return await _unitOfWork.Connection.QueryFirstAsync<Transport>(sql, new { id }, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<Transport>> GetCarTransports()
        {
            return await _unitOfWork.Connection.QueryAsync<Transport>("dbo.spGetCarTransport", commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(Transport entity)
        {
            var sql = @"UPDATE tblTransport SET number = @Number, type = @Type, model = @Model WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }
}
