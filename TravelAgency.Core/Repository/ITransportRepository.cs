using System.Collections.Generic;
using TravelAgency.Core.Entities;

namespace TravelAgency.Core.Repository
{
    public interface ITransportRepository : IGenericRepository<Transport>
    {
        public IEnumerable<Transport> GetCarTransports();
    }
}
