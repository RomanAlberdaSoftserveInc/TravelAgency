using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;

namespace TravelAgency.Core.Repository
{
    public interface IManagerRepository : IGenericRepository<Manager>
    {
        public Task<Manager> GetUserWithRolesAsync(int Id);
    }
}
