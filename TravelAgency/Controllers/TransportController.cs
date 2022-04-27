using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class TransportController : BaseApiController
    {
        private readonly ITransportRepository _transportRepository;
        public TransportController(ITransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransport([FromBody] Transport transport)
        {
            return Ok(await _transportRepository.AddAsync(transport));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransport([FromBody] Transport transport)
        {
            return Ok(await _transportRepository.UpdateAsync(transport));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransport([FromBody] Transport transport)
        {
            return Ok(await _transportRepository.DeleteAsync(transport));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransport(int id)
        {
            return Ok(await _transportRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetTransports()
        {
            return Ok(await _transportRepository.GetAllAsync());
        }

        [HttpGet("cars")]
        public async Task<IActionResult> GetCarTransports()
        {
            return Ok(await _transportRepository.GetCarTransports());
        }
    }
}
