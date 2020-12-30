using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Common.Exceptions
{
    public class AuthenticationError
    {
        public AuthenticationError()
        {
            Success = false;
            Status = string.Empty;
            Message = string.Empty;
        }

        public AuthenticationError(bool success, string status, string message)
        {
            Success = success;
            Status = status;
            Message = message;
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
