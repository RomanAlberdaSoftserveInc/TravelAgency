using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class TourTypeController : BaseApiController
    {
        private readonly ITourTypeRepository _tourTypeRepository;
        public TourTypeController(ITourTypeRepository tourTypeRepository)
        {
            _tourTypeRepository = tourTypeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatetourType([FromBody] TourType tourType)
        {
            return Ok(await _tourTypeRepository.AddAsync(tourType));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatetourType([FromBody] TourType tourType)
        {
            return Ok(await _tourTypeRepository.UpdateAsync(tourType));
        }

        [HttpDelete]
        public async Task<IActionResult> DeletetourType([FromBody] TourType tourType)
        {
            return Ok(await _tourTypeRepository.DeleteAsync(tourType));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GettourType(int id)
        {
            return Ok(await _tourTypeRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GettourTypes()
        {
            return Ok(await _tourTypeRepository.GetAllAsync());
        }
    }
}
