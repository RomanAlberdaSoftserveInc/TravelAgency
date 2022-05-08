using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Attributes;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class CheckController : BaseApiController
    {
        private readonly ICheckRepository _checkRepository;
        public CheckController(ICheckRepository checkRepository)
        {
            _checkRepository = checkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheck([FromBody] Check check)
        {
            return Ok(await _checkRepository.AddAsync(check));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCheck([FromBody] Check check)
        {
            return Ok(await _checkRepository.UpdateAsync(check));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCheck([FromBody] Check check)
        {
            return Ok(await _checkRepository.DeleteAsync(check));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheckById(int id)
        {
            return Ok(await _checkRepository.GetByIdAsync(id));
        }

        [Authorize(TravelAgency.Core.Enums.Role.TravelAgent, TravelAgency.Core.Enums.Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetChecks()
        {
            var result = await _checkRepository.GetAllAsync();
            return Ok(result);
        }
    }
}
