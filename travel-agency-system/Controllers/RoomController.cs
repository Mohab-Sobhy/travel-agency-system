using Microsoft.AspNetCore.Mvc;
using travel_agency_system.DTO;
using travel_agency_system.Models;

namespace travel_agency_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly HotelManagementContext _context;

        public RoomController(HotelManagementContext context)
        {
            _context = context;
        }

        // POST: api/Room
        [HttpPost("Add")]
        public async Task<IActionResult> AddRoom(AddRoomDto room)
        {
            if (room == null)
            {
                return BadRequest("Room data is null");
            }

            try
            {
                _context.Rooms.Add(new Room()
                {
                    HotelId = room.HotelId,
                    RoomType = room.RoomType,
                    Price = room.Price,
                    IsAvailable = room.IsAvailable
                });
                await _context.SaveChangesAsync();
                
                return Ok("room added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}