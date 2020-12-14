using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Security.Handlers
{
    public interface IJwtTokenHandler
    {
        string GenerateJwtSecurityToken(string userId);
        (bool, string) VerifyJwtSecurityToken(string jwtSecret, string token);
    }
}
