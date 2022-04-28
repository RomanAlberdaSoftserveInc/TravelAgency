using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class EatingTypeController : BaseApiController
    {
        private readonly IEatingTypeRepository _eatingRepository;
        public EatingTypeController(IEatingTypeRepository eatingTypeRepository)
        {
            _eatingRepository = eatingTypeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEatingType([FromBody] EatingType eatingType)
        {
            return Ok(await _eatingRepository.AddAsync(eatingType));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEatingType([FromBody] EatingType eatingType)
        {
            return Ok(await _eatingRepository.UpdateAsync(eatingType));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEatingType([FromBody] EatingType eatingType)
        {
            return Ok(await _eatingRepository.DeleteAsync(eatingType));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEatingType(int id)
        {
            return Ok(await _eatingRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetEatingTypes()
        {
            return Ok(await _eatingRepository.GetAllAsync());
        }
    }
}
