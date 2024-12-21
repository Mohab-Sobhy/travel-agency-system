using System.ComponentModel.DataAnnotations;

namespace travel_agency_system.DTO;

public class UserRegistrationDto
{
    [Required(ErrorMessage = "Please provide your name.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email address is required.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required.")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Password cannot be empty.")]
    public string Password { get; set; } = null!;

    public string? Role { get; set; } = null!;
    
    public string? AdminVerificationCode { get; set; } = null!;
}