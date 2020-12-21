using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.Exceptions;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.Security.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace LSS.BE.Core.Domain.Helpers
{
    public class HttpHandlerHelper
    {
        public static TokenResponse GetToken(string uriString, string version, string clientId, string clientSecret)
        {
            var tokenResponse = new TokenResponse();
            var response = HttpHandler.GetTokenAsync(uriString, version, UriAbsolutePath.GetToken, clientId, clientSecret);
            var content = response.Content.ReadAsStringAsync().Result;

            tokenResponse.StatusCode = (int)response.StatusCode;
            if (response.StatusCode != HttpStatusCode.OK) tokenResponse.Error = JsonConvert.DeserializeObject<TokenError>(content);
            tokenResponse.AccessToken = JsonConvert.DeserializeObject<AccessToken>(content);
            return tokenResponse;
        }

        public static string PostRequestResolver(string request, string uriString, string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime)
        {
            GetRefreshToken(uriString, version, clientId, clientSecret, accessToken, dateTime);
            var response = HttpHandler.PostAsync(request, uriString, version, uriPath, accessToken.Type, accessToken.Token);
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        private static void GetRefreshToken(string uriString, string version, string clientId, string clientSecret, AccessToken accessToken, DateTime dateTime)
        {
            DateTime currentDateTime = DateTime.Now;
            TimeSpan ts = currentDateTime - dateTime;
            int total = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
            if (total >= accessToken.ExpiresIn)
            {
                var tokenResponse = GetToken(uriString, version, clientId, clientSecret);
                accessToken.Type = tokenResponse.AccessToken.Type;
                accessToken.Token = tokenResponse.AccessToken.Token;
            }
        }

        public static string GetRequestResolver(string uriString, Dictionary<string, string> queryParams, string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime)
        {
            GetRefreshToken(uriString, version, clientId, clientSecret, accessToken, dateTime);
            var response = HttpHandler.GetAsync(uriString, queryParams, version, uriPath, accessToken.Type, accessToken.Token);
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }
    }
}
