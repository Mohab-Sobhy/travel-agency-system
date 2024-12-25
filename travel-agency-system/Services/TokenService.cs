namespace travel_agency_system.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string id, string role)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id),
            new Claim(JwtRegisteredClaimNames.Name, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresIn"])),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
