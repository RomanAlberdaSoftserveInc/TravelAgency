using System.Collections.Generic;
using System.Threading.Tasks;
using Quorum.OnDemand.Importer.Core.Repository;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;
using Dapper;
using System.Linq;
using System.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class TransportationRepository : ITransportationRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(Transportation entity)
        {
            var sql = @"INSERT INTO tblTransport 
                        (type,
                        model,
                        number)
                 VALUES (@Type,
                        @Model,
                        @Number)";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }

        public async Task<bool> DeleteAsync(Transportation entity)
        {
            var id = entity?.Id; 
            return await _unitOfWork.Connection.ExecuteAsync("dbo.spDeleteTransportation", new { transportationdId = id }, commandType: CommandType.StoredProcedure) > 1; // We can get Return Value using DynamicParams
        }

        public async Task<IReadOnlyList<Transportation>> GetAllAsync()
        {
            var sql = @"SELECT trp.id as Id, 
                        trp.arrivalLocation, 
                        trp.depatureLocation, 
                        trp.arrivalTime, 
                        trp.depatureTime, 
                        trp.pricePerPerson, 
                        t.id, 
                        t.model, 
                        t.type, 
                        t.number 
                    FROM tblTransportation trp
                    INNER JOIN tblTransport t
                    ON t.id = trp.transportId";
            var transports = await _unitOfWork.Connection.QueryAsync<Transportation, Transport, Transportation>(sql, (transportation, transport) =>
            {
                transportation.Transport = transport;
                return transportation;
            },
            splitOn: "id");
            return transports.ToList();
        }

        public async Task<Transportation> GetByIdAsync(int id)
        {
            var sql = @"SELECT * FROM tblTransportation trp INNER JOIN tblTransport t ON t.id = trp.transportId WHERE trp.id = @id";
            var transportations = await _unitOfWork.Connection.QueryAsync<Transportation, Transport, Transportation>(sql, (transportation, transport) =>
            {
                transportation.Transport = transport;
                return transportation;
            },
            param: new { id });

            return transportations.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(Transportation entity)
        {
            var sql = @"UPDATE tblTransportation SET number = @Number, type = @Type, model = @Model WHERE id = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }
    }
}
