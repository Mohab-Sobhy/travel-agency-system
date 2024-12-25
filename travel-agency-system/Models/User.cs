using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace travel_agency_system.Models;

public partial class User : IdentityUser
{
    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<NotificationsLog> NotificationsLogs { get; set; } = new List<NotificationsLog>();
}
