using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class HotelApi
{
    public string HotelId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Availability { get; set; } = null!;
}
