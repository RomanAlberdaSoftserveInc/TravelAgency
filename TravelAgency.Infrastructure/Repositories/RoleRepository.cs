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
    public class RoleRepository : IRoleRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapping _mapping;

        public RoleRepository(IUnitOfWork unitOfWork)
        {
            _mapping = new Mapping();
            _unitOfWork = unitOfWork;
        }

        public  Task<int> AddAsync(Role entity)
        {
            return null;
        }

        public  Task<bool> DeleteAsync(Role entity)
        {
            return null;
        }

        public async Task<IReadOnlyList<Role>> GetAllAsync()
        {
            var sql = @"SELECT * FROM tblRole";
            var roles = await _unitOfWork.Connection.QueryAsync<Role>(sql);
            return roles.ToList();
        }

        public  Task<Role> GetByIdAsync(int id)
        {
            return null;
        }

        public  Task<int> UpdateAsync(Role entity)
        {
            return null;
        }
    }
}
