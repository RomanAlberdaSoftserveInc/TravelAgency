using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class TransportationController : ControllerBase
    {
        private readonly ITransportRepository _transportRepository;
        public TransportationController(ITransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }

        [HttpGet("transports")]
        public async Task<IActionResult> GetTransports()
        {
            return Ok(await _transportRepository.GetCarTransports());
        }
    }
}
