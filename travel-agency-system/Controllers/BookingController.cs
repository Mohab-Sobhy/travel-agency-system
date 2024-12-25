using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using travel_agency_system.DTO;
using travel_agency_system.Models;
using travel_agency_system.Notifications;
using travel_agency_system.Services;

namespace travel_agency_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly HotelManagementContext _dbContext;
        private readonly NotificationManager _notificationManager;

        public BookingController(HotelManagementContext dbContext, NotificationManager notificationManager)
        {
            _dbContext = dbContext;
            _notificationManager = notificationManager;
        }
        
        [Authorize]
        [HttpPost("Room")]
        public async Task<IActionResult> bookRoom(BookingRoomDto bookingRoomDto)
        {
            if (bookingRoomDto == null)
            {
                return BadRequest("Invalid hotel data.");
            }
            
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string userId = TokenValidator.GetUserIDFromToken(token);
            
            Booking newBooking = new Booking()
            {
                UserId = userId,
                Timestamp = DateTime.Now,
                RoomId = bookingRoomDto.RoomID
            };
            
            User user = _dbContext.Users.FirstOrDefault(p => p.Id == userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            Room room = _dbContext.Rooms.FirstOrDefault(p => p.RoomId == bookingRoomDto.RoomID);
            if (room == null)
            {
                return BadRequest("Room not found.");
            }
            else if (room.IsAvailable == false)
            {
                return BadRequest("Room is not available.");
            }
            room.IsAvailable = false;
            
            Hotel hotel = _dbContext.Hotels.FirstOrDefault(p => p.HotelId == room.HotelId);
            if (hotel == null)
            {
                return BadRequest("Hotel not found.");
            }
            
            
            _dbContext.Bookings.Add(newBooking);
            await _dbContext.SaveChangesAsync();
            Booking lastBook = await _dbContext.Bookings.OrderByDescending(e => e.BookingId).FirstOrDefaultAsync();

            Notification newNotification = new Notification(user.Name , $"Dear {user.Name}, your booking of the {hotel.Name} hotel is confirmed, Your Booking ID is {lastBook.BookingId} . Thanks for using our store :)");
            _notificationManager.AddNotification(newNotification);

            NotificationsLog newLog = new NotificationsLog
            {
                ReceiverId = user.Id,
                Status = "In Queue"
            };
            _dbContext.NotificationsLogs.Add(newLog);
            
            return Ok("You Will Receive Confirmation Notification :)");
        }
    }
}
