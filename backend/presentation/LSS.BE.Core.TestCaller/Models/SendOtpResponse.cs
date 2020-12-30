using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.TestCaller.Models
{
    public class SendOtpResponse
    {
        [JsonProperty("is_request_success")]
        public string IsRequestSuccess { get; set; }

        [JsonProperty("ref_code")]
        public string RefCode { get; set; }
    }
}
