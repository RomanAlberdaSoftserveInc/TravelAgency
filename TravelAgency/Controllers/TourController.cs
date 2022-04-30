using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class TourController : BaseApiController
    {
        private readonly ITourRepository _tourRepository;
        public TourController(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTour([FromBody] Tour tour)
        {
            return Ok(await _tourRepository.AddAsync(tour));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTour([FromBody] Tour tour)
        {
            return Ok(await _tourRepository.UpdateAsync(tour));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTour([FromBody] Tour tour)
        {
            return Ok(await _tourRepository.DeleteAsync(tour));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourById(int id)
        {
            return Ok(await _tourRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            return Ok(await _tourRepository.GetAllAsync());
        }
    }
}
