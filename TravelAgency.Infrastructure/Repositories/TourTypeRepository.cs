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
    public class TourTypeRepository : ITourTypeRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TourTypeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(TourType entity)
        {
            var sql = @"INSERT INTO tblTourType (type) VALUES (@Type)";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }

        public async Task<bool> DeleteAsync(TourType entity)
        {
            var id = entity?.Id;
            var sql = @"DELETE FROM tblTourType WHERE id = @id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { id }, _unitOfWork.Transaction) > 0;
        }

        public async Task<IReadOnlyList<TourType>> GetAllAsync()
        {
            var sql = "SELECT * FROM tblTourType";
            var transports = await _unitOfWork.Connection.QueryAsync<TourType>(sql, _unitOfWork.Transaction);
            return transports.ToList();
        }

        public async Task<TourType> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM tblTourType WHERE id = @id";
            return await _unitOfWork.Connection.QueryFirstAsync<TourType>(sql, new { id }, _unitOfWork.Transaction);
        }

        public async Task<int> UpdateAsync(TourType entity)
        {
            var sql = @"UPDATE tblTourType SET type = @Type WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }
}
