using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace LSS.HCM.Core.Security.Handlers
{
    public sealed class JwtTokenHandler 
    {
        public static (bool, string) VerifyJwtSecurityToken(string jwtSecret, string token)
        {
            string claimsIdentity = string.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signinKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
                var claims = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken).Claims.ToList();
                claimsIdentity = claims.FirstOrDefault(c => c.Type == "transaction_id").Value;
                return (true, claimsIdentity);
            }
            catch
            {
                return (false, claimsIdentity);
            }
        }
    }
}
