using LSS.BE.Core.Common.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LSS.BE.Core.Domain.Interfaces
{
    /// <summary>
    ///   Represents http handler for get, post and put request.
    ///</summary>
    public interface IHttpHandlerHelper
    {
        /// <summary>
        /// Sets uri string, version, client id and secret.
        /// </summary>
        /// <returns>
        ///  Gets the bearer token response with expiration time. 
        /// </returns>
        TokenResponse GetToken(string version, string clientId, string clientSecret);

        /// <summary>
        /// Sets request, http method(post/put), uri string, version, client id, client secret and access token.
        /// </summary>
        /// <returns>
        ///  Gets the respective result. 
        /// </returns>
        string PostRequestResolver(string request, HttpMethod httpMethod, string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime);
        
        /// <summary>
        /// Sets the query string param, http method(get), uri string, version, client id, client secret and access token.
        /// </summary>
        /// <returns>
        ///  Gets the respective result. 
        /// </returns>
        string GetRequestResolver(Dictionary<string, string> queryParams, string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime);
    }
}
