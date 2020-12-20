using LSS.BE.Core.Domain.Handlers;
using System;
using System.Net;
using System.Text.Json;
using Xunit;

namespace LSS.BE.Core.Domain.UnitTest
{
    public class HttpHandlerUnitTest
    {
        public readonly string clientId = "d5cd3087-749c-4efe-baf7-b38126a69b8a";
        public readonly string clientSecret = "sgKEx9BXzV76oSXLt7NEv4HOF4g594IzYW9ffNU3";
        public readonly string uriString = "http://18.138.61.187";
        public readonly string version = "v1";

        [Fact]
        public void GetTokenUnauthorized()
        {
            var response = HttpHandler.PostRequestResolver(string.Empty, uriString, version, "token", clientId, clientSecret);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var hasan  = JsonSerializer.Deserialize<LSS.BE.Core.Common.Exceptions.ApplicationException>(content);
            }
        }

        [Fact]
        public void GetToken()
        {
            var result = HttpHandler.PostRequestResolver(string.Empty, uriString, version, "token", clientId, clientSecret);
        }
    }
}
