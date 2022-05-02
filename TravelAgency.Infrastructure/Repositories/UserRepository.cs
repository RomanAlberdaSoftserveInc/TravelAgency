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
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(User entity)
        {
            var parameters = new Dictionary<string, object>()
            {
                ["name"] = entity.Name,
                ["surname"] = entity.Surname,
                ["email"] = entity.Email,
                ["address"] = entity.Address,
                ["phoneNumber"] = entity.PhoneNumber,
                ["passport"] = entity.Passport,
            };
            return await _unitOfWork.Connection.ExecuteAsync("dbo.spAddHotel", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteAsync(User entity)
        {
            var id = entity?.Id;
            var sql = @"DELETE FROM tblHotel WHERE id = @id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { id }, _unitOfWork.Transaction) > 0;
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = @"select h.id, 
                               h.name, 
                               h.stars,
                               h.description, 
                               h.pricePerDay, 
                               h.address,
                               h.country, 
                               h.city,
                               h.eatingType
                        from tblHotel h";

            var hotels = await _unitOfWork.Connection.QueryAsync<User>(sql);
            return hotels.ToList();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = @"select h.id, 
                               h.name, 
                               h.stars,
                               h.description, 
                               h.pricePerDay, 
                               h.address,
                               h.country, 
                               h.city,
                               h.eatingType
                        from tblHotel h
                        WHERE h.id = @id";

            var hotels = await _unitOfWork.Connection.QueryAsync<User>(sql, new { id }, _unitOfWork.Transaction);

            return hotels.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(User entity)
        {
            var sql = @"UPDATE tblHotel SET type = @Type WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }

}
