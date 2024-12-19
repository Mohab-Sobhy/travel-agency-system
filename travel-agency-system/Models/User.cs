using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace travel_agency_system.Models;

public class User : IdentityUser
{
    public string Name { get; set; } // خاصية إضافية

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
