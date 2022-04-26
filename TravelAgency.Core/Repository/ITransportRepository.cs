using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;

namespace TravelAgency.Core.Repository
{
    public interface ITransportRepository : IGenericRepository<Transport>
    {
        public Task<IEnumerable<Transport>> GetCarTransports();
    }
}
