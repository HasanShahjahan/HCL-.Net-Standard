using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LSS.BE.Core.Security.Handlers
{
    public interface IHttpHandler
    {
        HttpResponseMessage GetTokenAsync(string uriString, string version, string uriPath, string clientId, string clientSecret);
        HttpResponseMessage PostAsync(string request, HttpMethod httpMethod, string uriString, string version, string uriPath, string type, string token);
        HttpResponseMessage GetAsync(string uriString, Dictionary<string, string> queryParams, string version, string uriPath, string type, string token);
    }
}
