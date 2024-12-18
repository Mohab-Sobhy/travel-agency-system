using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class EventApi
{
    public string EventId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string TicketsAvailable { get; set; } = null!;
}
