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
    public class TourRepository : ITourRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TourRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(Tour entity)
        {
            var dataTable = new DataTable("TourTransportationType");
            dataTable.Columns.Add("transportationId", typeof(int));

            entity.Transportations.Select(x => x).ToList().ForEach(x =>
            {
                dataTable.Rows.Add(x.Id);

            });

            var parameters = new Dictionary<string, object>()
            {
                ["country"] = entity.Country,
                ["city"] = entity.City,
                ["includedInsurance"] = entity.IncludedInsurance,
                ["hotelId"] = entity.Hotel.Id == 0 ? null : entity.Hotel.Id,
                ["tourTypeId"] = entity.TourType.Id,
                ["tourTransportIds"] = dataTable.AsTableValuedParameter("TourTransportationType"),
                ["createdAt"] = entity.CreatedAt,
                ["updatedAt"] = entity.UpdatedAt,
            };
            var s = await _unitOfWork.Connection.ExecuteScalarAsync<int>("dbo.spAddTour", parameters, commandType: CommandType.StoredProcedure);
            return s;
        }

        public async Task<bool> DeleteAsync(Tour entity)
        {
            var id = entity?.Id;
            var sql = @"DELETE FROM tblHotel WHERE id = @id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { id }, _unitOfWork.Transaction) > 0;
        }

        public async Task<IReadOnlyList<Tour>> GetAllAsync()
        {
            var sql = @"select t.id, 
                               t.country, 
                               t.city,
                               t.includedInsurance,
                               t.tourTypeId,
                               t.createdAt,
                               t.updatedAt,
                               tt.id, 
                               tt.type,
							   h.id,
							   h.name,
							   h.stars,
							   h.country,
							   h.city,
							   h.address,
							   h.description,
							   h.pricePerDay,
							   h.eatingType,
							   trp.id,
							   trp.arrivalLocation,
							   trp.depatureLocation,
							   trp.arrivalTime,
							   trp.depatureTime,
							   trp.pricePerPerson,
							   transp.id,
							   transp.model,
							   transp.type,
							   transp.number
                        from tblTour t
                        LEFT JOIN tblTourType tt
                        ON t.tourTypeId = tt.id
						LEFT JOIN tblHotel h
                        ON h.id = t.hotelId
						LEFT JOIN tblTourTransportation ttrp
                        ON t.id = ttrp.tourId
						LEFT JOIN tblTransportation trp
                        ON ttrp.transportation_id = trp.id
						LEFT JOIN tblTransport transp
                        ON trp.transportId = transp.id";

            var hotels = await _unitOfWork.Connection.QueryAsync<Tour, TourType, Hotel, Transportation, Transport, Tour>(sql, (tour, tourType, hotel, transportation, transport) =>
             {
                 if (transportation != null)
                 {
                     transportation.Transport = transport;
                 }
                 tour.TourType = tourType;
                 tour.Hotel = hotel;
                 tour.Transportations.Add(transportation);
                 return tour;
             },
             splitOn: "id");

            var result = hotels.GroupBy(p => p.Id).Select(g =>
            {
                var groupedPost = g.First();
                groupedPost.Transportations = g.Select(p => p.Transportations.Single()).ToList();
                return groupedPost;
            });
            return result.ToList();
        }

        public async Task<Tour> GetByIdAsync(int id)
        {
            var sql = @"select t.id, 
                               t.country, 
                               t.city,
                               t.includedInsurance,
                               t.tourTypeId,
                               t.createdAt,
                               t.updatedAt,
                               tt.id, 
                               tt.type,
							   h.id,
							   h.name,
							   h.stars,
							   h.country,
							   h.city,
							   h.address,
							   h.description,
							   h.pricePerDay,
							   h.eatingType,
							   trp.id,
							   trp.arrivalLocation,
							   trp.depatureLocation,
							   trp.arrivalTime,
							   trp.depatureTime,
							   trp.pricePerPerson,
							   transp.id,
							   transp.model,
							   transp.type,
							   transp.number
                        from tblTour t
                        LEFT JOIN tblTourType tt
                        ON t.tourTypeId = tt.id
						LEFT JOIN tblHotel h
                        ON h.id = t.hotelId
						LEFT JOIN tblTourTransportation ttrp
                        ON t.id = ttrp.tourId
						LEFT JOIN tblTransportation trp
                        ON ttrp.transportation_id = trp.id
						LEFT JOIN tblTransport transp
                        ON trp.transportId = transp.id
                        WHERE t.id = @id";

            var hotels = await _unitOfWork.Connection.QueryAsync<Tour, TourType, Hotel, Transportation, Transport, Tour>(sql, (tour, tourType, hotel, transportation, transport) =>
           {
               if (transportation != null)
               {
                   transportation.Transport = transport;
               }
               tour.TourType = tourType;
               tour.Hotel = hotel;
               tour.Transportations.Add(transportation);
               return tour;
           },
             param: new { id },
             splitOn: "id");

            var result = hotels.GroupBy(p => p.Id).Select(g =>
            {
                var groupedPost = g.First();
                groupedPost.Transportations = g.Select(p => p.Transportations.Single()).ToList();
                return groupedPost;
            });
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(Tour entity)
        {
            var sql = @"UPDATE tblTour SET type = @Type WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }

}
