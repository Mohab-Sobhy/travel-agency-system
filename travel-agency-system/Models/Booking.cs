﻿using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public string Type { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? Timestamp { get; set; }

    public int? RoomId { get; set; }

    public int? EventId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Room? Room { get; set; }

    public virtual User User { get; set; } = null!;
}
