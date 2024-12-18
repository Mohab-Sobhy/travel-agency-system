using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class Notification
{
    public string NotificationId { get; set; } = null!;

    public string? UserId { get; set; }

    public string Message { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateOnly Date { get; set; }

    public virtual ICollection<Activation> Activations { get; set; } = new List<Activation>();

    public virtual ICollection<BookingConfirmation> BookingConfirmations { get; set; } = new List<BookingConfirmation>();

    public virtual ICollection<NotificationService> NotificationServices { get; set; } = new List<NotificationService>();

    public virtual ICollection<ResetPassword> ResetPasswords { get; set; } = new List<ResetPassword>();

    public virtual User? User { get; set; }
}
