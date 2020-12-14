using LSS.HCM.Core.Security.Handlers;
using Xunit;

namespace HCM.Core.Security.UnitTest
{
    public class JwtTokenHandlerUnitTest
    {
        private readonly string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MDkzNTU5MjEsInRyYW5zYWN0aW9uX2lkIjoiNzBiMzZjNDEtMDc4Yi00MTFiLTk4MmMtYzViNzc0YWFjNjZmIn0.ujOkQJUq5WY_tZJgKXqe_n4nql3cSAeHMfXGABZO3E4";
        private readonly string secret = "HWAPI_0BwRn5Bg4rJAe5eyWkRz";

        [Fact]
        public void VerifyJwtSecurityToken()
        {
            var (isVerified, transactionId) = JwtTokenHandler.VerifyJwtSecurityToken(secret, token);
            Assert.True(isVerified);
        }

        [Fact]
        public void VerifyJwtTokenContainsTransactionId()
        {
            var (isVerified, transactionId) = JwtTokenHandler.VerifyJwtSecurityToken(secret, token);
            Assert.NotEmpty(transactionId);
        }
    }
}
