using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LSS.BE.Core.Domain.Handlers
{
    public class HttpHandler
    {
        public static HttpResponseMessage PostRequestResolver(string request, string uriString, string version, string uriPath, string clientId, string clientSecret)
        {

            HttpClient client = new HttpClient();
            Uri baseUri = new Uri(uriString);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //Post body content
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            var content = new FormUrlEncodedContent(values);

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/" + version + "/" + uriPath);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            //make the request
            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            return response;

            //if (response.StatusCode != HttpStatusCode.OK) return response.StatusCode.ToString();
            //response.EnsureSuccessStatusCode();
            //string responseBody = response.Content.ReadAsStringAsync().Result;
            //return responseBody;

        }
    }
}
