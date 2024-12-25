using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class NotificationsLog
{
    public int Id { get; set; }

    public string ReceiverId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;
}
