using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using travel_agency_system.DTO;
using travel_agency_system.Models;
using travel_agency_system.Services;

namespace travel_agency_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelManagementContext _context;
        
        public HotelController(HotelManagementContext context)
        {
            _context = context;
        }
        
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> addNewHotel(HotelAddDto hotelAddDto)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!TokenValidator.IsAdmin(token))
            {
                return Unauthorized("You are not authorized to perform this action.");
            }
            
            if (hotelAddDto == null)
            {
                return BadRequest("Invalid hotel data.");
            }

            Hotel newHotel = new Hotel()
            {
                Name = hotelAddDto.Name,
                Location = hotelAddDto.Location
            };

            var result = await _context.Hotels.AddAsync(newHotel);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return Ok(new { message = "Hotel added successfully!", hotel = newHotel });
            }
            else
            {
                return BadRequest("Failed to add the hotel.");
            }
        }
    }
}
