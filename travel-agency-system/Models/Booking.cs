using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class Booking
{
    public string BookingId { get; set; } = null!;

    public string? UserId { get; set; }

    public string Type { get; set; } = null!;

    public string Details { get; set; } = null!;

    public DateOnly Date { get; set; }

    public virtual User? User { get; set; }
}
