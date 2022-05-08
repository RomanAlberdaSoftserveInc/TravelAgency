using System.Collections.Generic;
using TravelAgency.Models;
using TravelAgency.Core.Entities;
using System.Threading.Tasks;

namespace TravelAgency.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<Manager>> GetAll();
        Task<Manager> GetByIdAsync(int id);
    }
}
