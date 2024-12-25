namespace travel_agency_system.Services;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public static class TokenValidator
{
    public static bool IsAdmin(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        if (handler.CanReadToken(token))
        {
            var jwtToken = handler.ReadJwtToken(token);
            
            var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name);
            
            if (nameClaim != null && nameClaim.Value == "ADMIN")
            {
                return true;
            }
        }
        
        return false;
    }
    
    public static string GetUserIDFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        if (handler.CanReadToken(token))
        {
            var jwtToken = handler.ReadJwtToken(token);
            
            var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            
            if (subClaim != null)
            {
                return subClaim.Value;
            }
        }
        
        return null;
    }
}