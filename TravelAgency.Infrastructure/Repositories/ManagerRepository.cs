using Dapper;
using Quorum.OnDemand.Importer.Core.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Infrastructure.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(Manager entity)
        {
            var parameters = new Dictionary<string, object>()
            {
                ["name"] = entity.Name,
                ["surname"] = entity.Surname,
                ["email"] = entity.Email,
                ["phoneNumber"] = entity.PhoneNumber,
                ["agency"] = entity.Agency.Id,
            };
            return await _unitOfWork.Connection.ExecuteAsync("dbo.spAddManager", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteAsync(Manager entity)
        {
            var id = entity?.Id;
            var sql = @"DELETE FROM tblManager WHERE id = @id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { id }, _unitOfWork.Transaction) > 0;
        }

        public async Task<IReadOnlyList<Manager>> GetAllAsync()
        {
            var sql = @"SELECT m.id, m.name, m.surname, m.password, m.email, m.phoneNumber, r.id, r.name, a.id, a.name, a.helpNumber, a.address
		                FROM tblManager as m
		                INNER JOIN tblRoleManager rm
		                ON rm.managerId = m.id
		                INNER JOIN tblRole r
		                ON r.id = rm.roleId
		                INNER JOIN tblAgency a
		                ON a.id = m.agencyId";
            var managers = await _unitOfWork.Connection.QueryAsync<Manager, Role, Agency, Manager>(sql, (manager, role, agency) =>
            {
                manager.Roles.Add(role);
                if(manager.Agency == null)
                {
                    manager.Agency = new Agency();
                }
                manager.Agency = agency;
                return manager;
            },
            splitOn: "id");

            var result = managers.GroupBy(p => p.Id).Select(g =>
            {
                var groupedPost = g.First();
                groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                return groupedPost;
            });
            return result.ToList();
        }

        public async Task<Manager> GetByIdAsync(int id)
        {
            var sql = @"SELECT m.id, m.name, m.surname, m.email, m.phoneNumber, r.id, r.name, a.id, a.name, a.helpNumber, a.address
		                FROM tblManager as m
		                INNER JOIN tblRoleManager rm
		                ON rm.managerId = m.id
		                INNER JOIN tblRole r
		                ON r.id = rm.roleId
		                INNER JOIN tblAgency a
		                ON a.id = m.agencyId
                        WHERE m.id = @id";
            var managers = await _unitOfWork.Connection.QueryAsync<Manager, Role, Agency, Manager>(sql, (manager, role, agency) =>
            {
                manager.Roles.Add(role);
                manager.Agency = agency;
                return manager;
            },
            param: new { id },
            splitOn: "id");

            var result = managers.GroupBy(p => p.Id).Select(g =>
            {
                var groupedPost = g.First();
                groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                return groupedPost;
            });
            return result.FirstOrDefault();
        }

        public async Task<Manager> GetUserWithRolesAsync(int Id)
        {
            var sql = @"SELECT m.id, m.email, r.id, r.name FROM tblManager m
                        INNER JOIN tblRoleManager rm
                        ON rm.managerId = m.id
                        INNER JOIN tblRole r
                        ON rm.roleId = r.id
                        WHERE m.id = @id";
            var managers = await _unitOfWork.Connection.QueryAsync<Manager, Role, Manager>(sql, (manager, role) =>
            {
                manager.Roles.Add(role);
                return manager;
            },
            param: new { Id },
            splitOn: "id");

            var result = managers.GroupBy(p => p.Id).Select(g =>
            {
                var groupedPost = g.First();
                groupedPost.Roles = g.Select(p => p.Roles.Single()).ToList();
                return groupedPost;
            });
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(Manager entity)
        {
            var sql = @"UPDATE tblManager SET type = @Type WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }

}
