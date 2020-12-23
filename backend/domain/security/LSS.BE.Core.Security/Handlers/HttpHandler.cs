using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;

namespace LSS.BE.Core.Security.Handlers
{
    public class HttpHandler
    {
        public static HttpResponseMessage GetTokenAsync(string uriString, string version, string uriPath, string clientId, string clientSecret)
        {

            HttpClient client = new HttpClient();
            Uri baseUri = new Uri(uriString);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            var content = new FormUrlEncodedContent(values);

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/" + version + "/" + uriPath);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            return response;
        }
        
        public static HttpResponseMessage PostAsync(string request, HttpMethod httpMethod, string uriString, string version, string uriPath, string type, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uriString);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string mediaType = "application/json";
            var content = new StringContent(request, Encoding.UTF8, mediaType);
            var requestMessage = new HttpRequestMessage(httpMethod, "/" + version + "/" + uriPath);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(type, token);
            requestMessage.Content = content;

            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            return response;
        }

        public static HttpResponseMessage GetAsync(string uriString, Dictionary<string, string> queryParams, string version, string uriPath, string type, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uriString);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString("/" + version + "/" + uriPath, queryParams));
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(type, token);

            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            return response;
        }
    }
}
