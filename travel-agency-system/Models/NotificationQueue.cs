using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class NotificationQueue
{
    public int QueueId { get; set; }

    public int NotificationId { get; set; }

    public string? Status { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Notification Notification { get; set; } = null!;
}
