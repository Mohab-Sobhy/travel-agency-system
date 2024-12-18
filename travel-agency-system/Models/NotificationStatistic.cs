using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class NotificationStatistic
{
    public int StatId { get; set; }

    public string Metric { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime? Timestamp { get; set; }
}
