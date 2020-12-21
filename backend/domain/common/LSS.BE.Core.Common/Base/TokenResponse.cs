using LSS.BE.Core.Common.Exceptions;

namespace LSS.BE.Core.Common.Base
{
    public class TokenResponse
    {
        public int StatusCode { get; set; }
        public AccessToken AccessToken { get; set; }
        public TokenError Error { get; set; }
    }
}
