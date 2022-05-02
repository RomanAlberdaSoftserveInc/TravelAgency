using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class ManagerController : BaseApiController
    {
        private readonly IManagerRepository _managerRepository;
        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTour([FromBody] Manager manager)
        {
            return Ok(await _managerRepository.AddAsync(manager));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTour([FromBody] Manager manager)
        {
            return Ok(await _managerRepository.UpdateAsync(manager));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTour([FromBody] Manager manager)
        {
            return Ok(await _managerRepository.DeleteAsync(manager));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourById(int id)
        {
            return Ok(await _managerRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            return Ok(await _managerRepository.GetAllAsync());
        }
    }
}
