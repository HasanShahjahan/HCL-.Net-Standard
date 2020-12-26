using LSS.BE.Core.Common.Exceptions;
using System;

namespace LSS.BE.Core.Common.Base
{
    public class TokenResponse
    {
        public int StatusCode { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public AccessToken AccessToken { get; set; }
        public TokenError Error { get; set; }
    }
}
