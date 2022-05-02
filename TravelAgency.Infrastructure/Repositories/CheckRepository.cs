using Dapper;
using Quorum.OnDemand.Importer.Core.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Models;
using TravelAgency.Core.Repository;
using TravelAgency.Infrastructure.CustomMapping;

namespace TravelAgency.Infrastructure.Repositories
{
    public class CheckRepoitory : ICheckRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapping _mapping;

        public CheckRepoitory(IUnitOfWork unitOfWork)
        {
            _mapping = new Mapping();
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(Check entity)
        {
            var parameters = new Dictionary<string, object>()
            {
                ["price"] = entity.Price,
                ["personCount"] = entity.PersonCount,
                ["tourId"] = entity.Tour.Id,
                ["userId"] = entity.User.Id,
                ["producedBy"] = entity.Manager.Id,
                ["createdAt"] = entity.CreatedAt,
            };
            return await _unitOfWork.Connection.ExecuteAsync("dbo.spAddCheck", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteAsync(Check entity)
        {
            var id = entity?.Id;
            var sql = @"DELETE FROM tblCheck WHERE id = @id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { id }, _unitOfWork.Transaction) > 0;
        }

        public async Task<IReadOnlyList<Check>> GetAllAsync()
        {
            var checks = await _unitOfWork.Connection.QueryAsync<Check, ManagerModel, UserModel, TourModel, Check>("select * from viewCheck", (check, manager, user, tour) =>
            {
                check.Manager = _mapping.MapManager(manager);
                check.Tour = _mapping.MapTour(tour);
                check.User = _mapping.MapUser(user);

                return check;
            },
            splitOn: "id,managerId,userId,tourId");

            return checks.ToList();
        }

        public async Task<Check> GetByIdAsync(int id)
        {
            var checks = await _unitOfWork.Connection.QueryAsync<Check, ManagerModel, UserModel, TourModel, Check>("select * from fnGetChecks(@checkId)", (check, manager, user, tour) =>
            {
                check.Manager = _mapping.MapManager(manager);
                check.Tour = _mapping.MapTour(tour);
                check.User = _mapping.MapUser(user);

                return check;
            },
            param: new { checkId = id },
            splitOn: "id,managerId,userId,tourId",
            commandType: CommandType.Text);

            return checks.ToList().FirstOrDefault();
        }

        public async Task<int> UpdateAsync(Check entity)
        {
            var sql = @"UPDATE tblCheck SET type = @Type WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }
}
