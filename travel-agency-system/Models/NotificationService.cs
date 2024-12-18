using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class NotificationService
{
    public int ServiceId { get; set; }

    public string? NotificationId { get; set; }

    public string ServiceType { get; set; } = null!;

    public virtual Notification? Notification { get; set; }
}
