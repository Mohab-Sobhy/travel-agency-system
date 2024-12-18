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
        private readonly DBContext _context;

        public UserController(DBContext context)
        {
            _context = context;
        }
        
        [HttpGet("{id}")]
        public IActionResult displayEmpNo(int id)
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
