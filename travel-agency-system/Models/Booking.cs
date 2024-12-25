using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public int? RoomId { get; set; }

    public virtual Room? Room { get; set; }

    public virtual User User { get; set; } = null!;
}
