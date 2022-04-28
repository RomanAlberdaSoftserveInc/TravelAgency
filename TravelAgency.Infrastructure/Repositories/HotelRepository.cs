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
    public class HotelRepository : IHotelRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(Hotel entity)
        {
            var parameters = new Dictionary<string, object>()
            {
                ["name"] = entity.Name,
                ["stars"] = entity.Stars,
                ["description"] = entity.Description,
                ["pricePerDay"] = entity.PricePerDay,
                ["country"] = entity.Country,
                ["city"] = entity.City,
                ["address"] = entity.Address,
                ["eatingTypeId"] = entity.EatingType.Id,
            };
            return await _unitOfWork.Connection.ExecuteAsync("dbo.spAddHotel", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteAsync(Hotel entity)
        {
            var id = entity?.Id;
            var sql = @"DELETE FROM tblHotel WHERE id = @id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { id }, _unitOfWork.Transaction) > 0;
        }

        public async Task<IReadOnlyList<Hotel>> GetAllAsync()
        {
            var sql = @"select h.id, 
                               h.name, 
                               h.stars,
                               h.description, 
                               h.pricePerDay, 
                               h.address,
                               h.country, 
                               h.city,
                               e.id, 
                               e.type 
                        from tblHotel h
                        INNER JOIN tblEatingType e
                        ON h.eatingTypeId = e.id";

            var hotels = await _unitOfWork.Connection.QueryAsync<Hotel, EatingType, Hotel>(sql, (hotel, eatingType) =>
            {
                hotel.EatingType = eatingType;
                return hotel;
            },
            splitOn: "id");
            return hotels.ToList();
        }

        public async Task<Hotel> GetByIdAsync(int id)
        {
            var sql = @"select h.id, 
                               h.name, 
                               h.stars,
                               h.description, 
                               h.pricePerDay, 
                               h.address,
                               h.country, 
                               h.city,
                               e.id, 
                               e.type 
                        from tblHotel h
                        INNER JOIN tblEatingType e
                        ON h.eatingTypeId = e.id
                        WHERE h.id = @id";

            var hotels = await _unitOfWork.Connection.QueryAsync<Hotel, EatingType, Hotel>(sql, (hotel, eatingType) =>
            {
                hotel.EatingType = eatingType;
                return hotel;
            },
            param: new { id },
            splitOn: "id"); 
            return hotels.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(Hotel entity)
        {
            var sql = @"UPDATE tblHotel SET type = @Type WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }

}
