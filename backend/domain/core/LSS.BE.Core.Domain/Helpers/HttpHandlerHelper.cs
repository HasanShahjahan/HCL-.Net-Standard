using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.Exceptions;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.Domain.Interfaces;
using LSS.BE.Core.Security.Handlers;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace LSS.BE.Core.Domain.Helpers
{
    /// <summary>
    ///   Represents http handler for get, post and put request.
    ///</summary>
    public class HttpHandlerHelper: IHttpHandlerHelper
    {
        /// <summary>
        ///   Initialize the handler interface.
        ///</summary>
        private readonly IHttpHandler _httpHandler;

        /// <summary>
        ///   Initialization information for Http Handler.
        ///</summary>
        public HttpHandlerHelper(string uriString)
        {
            _httpHandler = new HttpHandler(uriString);
        }

        /// <summary>
        /// Sets uri string, version, client id and secret.
        /// </summary>
        /// <returns>
        ///  Gets the bearer token response with expiration time. 
        /// </returns>
        public TokenResponse GetToken(string version, string clientId, string clientSecret)
        {
            
            Log.Information("[Get Token][Token Information]: " + "[Version:" + version + "]" + "[ClientId:" + clientId + "]" + "[ClientSecret:" + clientSecret + "]");
            var tokenResponse = new TokenResponse();
            var response = _httpHandler.GetTokenAsync(version, UriAbsolutePath.GetToken, clientId, clientSecret);
            var content = response.Content.ReadAsStringAsync().Result;
            Log.Information("[Get Token][Token Response :" + content + "]");

            tokenResponse.StatusCode = (int)response.StatusCode;
            if (response.StatusCode != HttpStatusCode.OK) tokenResponse.Error = JsonConvert.DeserializeObject<TokenError>(content);
            tokenResponse.AccessToken = JsonConvert.DeserializeObject<AccessToken>(content);
            return tokenResponse;
        }

        /// <summary>
        /// Sets refresh token by uri string, version, client id and secret.
        /// </summary>
        /// <returns>
        ///  Gets the bearer token response with expiration time. 
        /// </returns>
        private void GetRefreshToken(string version, string clientId, string clientSecret, AccessToken accessToken, DateTime dateTime)
        {
            DateTime currentDateTime = DateTime.Now;
            TimeSpan ts = currentDateTime - dateTime;
            int total = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
            if (total >= accessToken.ExpiresIn)
            {
                Log.Information("[Get Refresh Token][Expires In : " + total + "]");
                var tokenResponse = GetToken(version, clientId, clientSecret);
                accessToken.Type = tokenResponse.AccessToken.Type;
                accessToken.Token = tokenResponse.AccessToken.Token;
            }
        }

        /// <summary>
        /// Sets the query string param, http method(get), uri string, version, client id, client secret and access token.
        /// </summary>
        /// <returns>
        ///  Gets the respective result. 
        /// </returns>
        public string GetRequestResolver(Dictionary<string, string> queryParams, string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime)
        {
            GetRefreshToken(version, clientId, clientSecret, accessToken, dateTime);
            var response = _httpHandler.GetAsync(queryParams, version, uriPath, accessToken.Type, accessToken.Token);
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        /// <summary>
        /// Sets request, http method(post/put), uri string, version, client id, client secret and access token.
        /// </summary>
        /// <returns>
        ///  Gets the respective result. 
        /// </returns>
        public string PostRequestResolver(string request, HttpMethod httpMethod, string version, string clientId, string clientSecret, string uriPath, AccessToken accessToken, DateTime dateTime)
        {
            GetRefreshToken(version, clientId, clientSecret, accessToken, dateTime);
            var response = _httpHandler.PostAsync(request, httpMethod, version, uriPath, accessToken.Type, accessToken.Token);
            var content = response.Content.ReadAsStringAsync().Result;
            return content;
        }
    }
}
