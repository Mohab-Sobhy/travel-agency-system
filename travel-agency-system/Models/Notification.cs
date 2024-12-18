using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Type { get; set; } = null!;

    public int TemplateId { get; set; }

    public int UserId { get; set; }

    public string? Status { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual ICollection<NotificationQueue> NotificationQueues { get; set; } = new List<NotificationQueue>();

    public virtual Template Template { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
