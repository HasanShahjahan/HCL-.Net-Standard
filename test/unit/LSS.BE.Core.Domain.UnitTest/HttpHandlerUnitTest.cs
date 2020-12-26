using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Security.Handlers;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace LSS.BE.Core.Domain.UnitTest
{
    public class HttpHandlerUnitTest
    {
        public readonly string clientId = "d5cd3087-749c-4efe-baf7-b38126a69b8a";
        public readonly string clientSecret = "sgKEx9BXzV76oSXLt7NEv4HOF4g594IzYW9ffNU3";
        public readonly string uriString = "http://18.138.61.187";
        public readonly string version = "v1";
        public readonly HttpHandler httpHandler = new HttpHandler("http://18.138.61.187");

        [Fact]
        public void GetTokenUnauthorized()
        {
            var response = httpHandler.GetTokenAsync(uriString, version, "token", clientId, clientSecret);
            var content = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var tokenError = JsonConvert.DeserializeObject<Common.Exceptions.TokenError>(content);
                Assert.Equal("invalid_client", tokenError.Error);
            }
        }

        [Fact]
        public void GetToken()
        {
            var response = httpHandler.GetTokenAsync(uriString, version, "token", clientId, clientSecret);
            var content = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var tokenError = JsonConvert.DeserializeObject<Common.Exceptions.TokenError>(content);
                Assert.Equal("invalid_client", tokenError.Error);
            }
            var result = JsonConvert.DeserializeObject<AccessToken>(content);
            Assert.Equal(200, (int)response.StatusCode);
        }
    }
}
