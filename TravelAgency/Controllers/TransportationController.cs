﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;

namespace TravelAgency.Controllers
{
    public class TransportationController : BaseApiController
    {
        private readonly ITransportationRepository _transportRepository;
        public TransportationController(ITransportationRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransportation([FromBody] Transportation transport)
        {
            return Ok(await _transportRepository.AddAsync(transport));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransportation([FromBody] Transportation transport)
        {
            return Ok(await _transportRepository.UpdateAsync(transport));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransportation([FromBody] Transportation transport)
        {
            return Ok(await _transportRepository.DeleteAsync(transport));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransportation(int id)
        {
            return Ok(await _transportRepository.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetTransportations()
        {
            return Ok(await _transportRepository.GetAllAsync());
        }
    }
}
