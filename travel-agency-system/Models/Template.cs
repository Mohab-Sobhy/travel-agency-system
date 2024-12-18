using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class Template
{
    public int TemplateId { get; set; }

    public string Content { get; set; } = null!;

    public string? Placeholders { get; set; }

    public string Language { get; set; } = null!;

    public string Channel { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
