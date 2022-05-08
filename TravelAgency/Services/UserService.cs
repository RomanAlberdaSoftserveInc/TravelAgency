using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using TravelAgency.Core.Entities;
using TravelAgency.Models;
using TravelAgency.Helpers;
using TravelAgency.Core.Repository;
using System.Threading.Tasks;

namespace TravelAgency.Services
{
    public class UserService : IUserService
    {
        private IManagerRepository _managerRepository;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings,
            IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }


        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var users = await _managerRepository.GetAllAsync();
            var user = users.SingleOrDefault(x => x.Email == model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.Password))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public async Task<IEnumerable<Manager>> GetAll()
        {
            return await _managerRepository.GetAllAsync();
        }

        public async Task<Manager> GetByIdAsync(int id)
        {
            var user = await _managerRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
