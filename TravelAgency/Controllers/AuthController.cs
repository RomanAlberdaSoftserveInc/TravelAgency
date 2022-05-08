using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Attributes;
using TravelAgency.Core.Entities;
using TravelAgency.Core.Repository;
using TravelAgency.Models;
using TravelAgency.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace TravelAgency.Controllers
{
    [Authorize]
    public class AuthController : BaseApiController
    {
        private IUserService _userService;
        private IManagerRepository _managerRepository;

        public AuthController(IUserService userService, IManagerRepository managerRepository)
        {
            _userService = userService;
            _managerRepository = managerRepository;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var hash = BCryptNet.HashPassword("admin");
            var response = await _userService.Authenticate(model);
            return Ok(response);
        }

        //[Authorize(Roles = "HRManager,Finance")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            // only admins can access other user records
            var currentUser = (Manager)HttpContext.Items["User"];
            var userWithRoles = await _managerRepository.GetUserWithRolesAsync(currentUser.Id);
            if (id != currentUser.Id && userWithRoles.Roles.ToList().Any(x => x.Name == "Admin"))
                return Unauthorized(new { message = "Unauthorized" });

            var user = _userService.GetByIdAsync(id);
            return Ok(user);
        }
    }
}
