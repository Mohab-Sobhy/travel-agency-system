namespace travel_agency_system.Services;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public static class TokenValidator
{
    public static bool IsAdmin(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        // التأكد من أن الـ Token صالح
        if (handler.CanReadToken(token))
        {
            var jwtToken = handler.ReadJwtToken(token);
            
            // استخراج الـ Claim الذي اسمه "name" (اسم المستخدم أو الدور)
            var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name);
            
            // التحقق إذا كانت قيمة الـ Claim هي "ADMIN"
            if (nameClaim != null && nameClaim.Value == "ADMIN")
            {
                return true; // الـ Token يحتوي على "ADMIN"
            }
        }
        
        return false; // إذا لم يكن الـ Token يحتوي على "ADMIN"
    }

    // فانكشن جديدة لإرجاع قيمة "sub" من الـ Token
    public static string GetUserIDFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        // التأكد من أن الـ Token صالح
        if (handler.CanReadToken(token))
        {
            var jwtToken = handler.ReadJwtToken(token);
            
            // استخراج الـ Claim الذي اسمه "sub"
            var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            
            // إرجاع قيمة الـ Claim إذا وجد
            if (subClaim != null)
            {
                return subClaim.Value;
            }
        }
        
        return null; // إذا لم يتم العثور على "sub" أو كان الـ Token غير صالح
    }
}