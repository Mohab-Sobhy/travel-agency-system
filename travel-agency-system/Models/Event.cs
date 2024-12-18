using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
