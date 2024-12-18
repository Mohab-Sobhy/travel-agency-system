using System;
using System.Collections.Generic;

namespace travel_agency_system.Models;

public partial class Registration
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool VerificationStatus { get; set; }
}
