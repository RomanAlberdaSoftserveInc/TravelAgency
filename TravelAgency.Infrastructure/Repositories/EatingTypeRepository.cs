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
    public class EatingTypeRepository : IEatingTypeRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public EatingTypeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(EatingType entity)
        {
            var sql = @"INSERT INTO tblEatingType (type) VALUES (@Type)";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }

        public async Task<bool> DeleteAsync(EatingType entity)
        {
            var id = entity?.Id;
            var sql = @"DELETE FROM tblEatingType WHERE id = @id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { id }, _unitOfWork.Transaction) > 0;
        }

        public async Task<IReadOnlyList<EatingType>> GetAllAsync()
        {
            var sql = "SELECT * FROM tblEatingType";
            var transports = await _unitOfWork.Connection.QueryAsync<EatingType>(sql, _unitOfWork.Transaction);
            return transports.ToList();
        }

        public async Task<EatingType> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM tblEatingType WHERE id = @id";
            return await _unitOfWork.Connection.QueryFirstAsync<EatingType>(sql, new { id }, _unitOfWork.Transaction);
        }

        public async Task<int> UpdateAsync(EatingType entity)
        {
            var sql = @"UPDATE tblEatingType SET type = @Type WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }
}
