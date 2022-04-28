using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class HotelController : BaseApiController
    {
        private readonly IHotelRepository _hotelRepository;
        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatetourType([FromBody] Hotel hotel)
        {
            return Ok(await _hotelRepository.AddAsync(hotel));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatetourType([FromBody] Hotel hotel)
        {
            return Ok(await _hotelRepository.UpdateAsync(hotel));
        }

        [HttpDelete]
        public async Task<IActionResult> DeletetourType([FromBody] Hotel hotel)
        {
            return Ok(await _hotelRepository.DeleteAsync(hotel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GettourType(int id)
        {
            return Ok(await _hotelRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GettourTypes()
        {
            return Ok(await _hotelRepository.GetAllAsync());
        }
    }
}
