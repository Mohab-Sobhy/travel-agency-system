using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travel_agency_system.DTO;
using travel_agency_system.Models;
using travel_agency_system.Services;

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
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> AddRoom(AddRoomDto room)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!TokenValidator.IsAdmin(token))
            {
                return Unauthorized("You are not authorized to perform this action.");
            }
            
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
        
        [HttpGet("filter")]
        public async Task<IActionResult> FilterRooms(
            [FromQuery] string? hotelName,
            [FromQuery] string? roomType,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? isAvailable,
            [FromQuery] string? hotelLocation)
        {
            var query = _context.Rooms.Include(r => r.Hotel).AsQueryable();

            if (!string.IsNullOrEmpty(hotelName))
                query = query.Where(r => r.Hotel.Name.Contains(hotelName));

            if (!string.IsNullOrEmpty(roomType))
                query = query.Where(r => r.RoomType.Contains(roomType));

            if (minPrice.HasValue)
                query = query.Where(r => r.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(r => r.Price <= maxPrice.Value);

            if (isAvailable.HasValue)
                query = query.Where(r => r.IsAvailable == isAvailable.Value);

            if (!string.IsNullOrEmpty(hotelLocation))
                query = query.Where(r => r.Hotel.Location.Contains(hotelLocation));

            var result = await query
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomType = r.RoomType,
                    Price = r.Price,
                    IsAvailable = r.IsAvailable,
                    HotelName = r.Hotel.Name,
                    HotelLocation = r.Hotel.Location
                })
                .ToListAsync();

            return Ok(result);
        }
        
    }
    
}