using LSS.BE.Core.Common.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LSS.BE.Core.Domain.Interfaces
{
    public interface IHttpHandlerHelper
    {
        TokenResponse GetToken(string uriString, string version, string clientId, string clientSecret);
        string PostRequestResolver(string request, HttpMethod httpMethod, string uriString, string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime);
        string GetRequestResolver(Dictionary<string, string> queryParams, string uriString,  string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime);
    }
}
