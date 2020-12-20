using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class VerifyOtpResponse
    {
        [JsonProperty("is_request_success")]
        public string IsRequestSuccess { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("error_details")]
        public string ErrorDetails { get; set; }
    }
}
