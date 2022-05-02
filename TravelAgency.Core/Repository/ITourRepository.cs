using System.Threading.Tasks;
using TravelAgency.Core.Entities;

namespace TravelAgency.Core.Repository
{
    public interface ITourRepository : IGenericRepository<Tour>
    {
        Task<TourPhoto> AddPhoto(int id);
        Task<TourPhoto> DeletePhoto(int id);
        Task<TourPhoto> GetPhoto(int id);
    }
}
