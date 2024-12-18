using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class BookingConfirmation
{
    public int ConfirmationId { get; set; }

    public string? NotificationId { get; set; }

    public string Message { get; set; } = null!;

    public virtual Notification? Notification { get; set; }
}
