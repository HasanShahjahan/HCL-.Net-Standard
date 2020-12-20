using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.Security.Handlers;
using Newtonsoft.Json;
using System;
using System.Net;

namespace LSS.BE.Core.Domain.Helpers
{
    public class HttpHandlerHelper
    {
        public static T GetToken<T>(string uriString, string version, string clientId, string clientSecret)
        {
            var response = HttpHandler.GetTokenAsync(uriString, version, UriAbsolutePath.GetToken, clientId, clientSecret);
            var content = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var tokenError = JsonConvert.DeserializeObject<Common.Exceptions.TokenError>(content);
            }
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public static T PostRequestResolver<T>(string request, string uriString, string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime)
        {
            int time = Convert.ToInt32(dateTime);
            if (time >= accessToken.ExpiresIn) accessToken = GetToken<AccessToken>(uriString, version, clientId, clientSecret);
            
            var response = HttpHandler.PostAsync(request, uriString, version, uriPath, accessToken.Type, accessToken.Token);
            var content = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var tokenError = JsonConvert.DeserializeObject<Common.Exceptions.ApplicationError>(content);
            }
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }
    }
}
