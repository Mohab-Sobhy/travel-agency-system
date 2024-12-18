using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using travel_agency_system.Models;
using System.Linq;

namespace travel_agency_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TravelAgencySystemContext _context;

        public UserController(TravelAgencySystemContext context)
        {
            _context = context;
        }
        
        [HttpGet("{id}")]
        public IActionResult displayEmpNo(String id)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }
    }
}
