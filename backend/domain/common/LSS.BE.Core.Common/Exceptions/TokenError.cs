using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Common.Exceptions
{
    public class TokenError
    {
        public TokenError()
        {
            Error = string.Empty;
            ErrorDescription = string.Empty;
            Message = string.Empty;
        }

        public TokenError(string error, string errorDescription, string  message)
        {
            Error = error;
            ErrorDescription = errorDescription;
            Message = message;
        }

        [JsonProperty("error")]
        public string Error { get; set; }

       [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

       [JsonProperty("message")]
        public string Message { get; set; }

    }
}
