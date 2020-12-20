using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class LspUserAccessResponse
    {
        [JsonProperty("is_request_success")]
        public bool IsRequestSuccess { get; set; }

        [JsonProperty("LspId")]
        public string LspId { get; set; }

        [JsonProperty("ref_code")]
        public string RefCode { get; set; }

        [JsonProperty("expired_at")]
        public string ExpiredAt { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("error_details")]
        public string ErrorDetails { get; set; }
    }
}
