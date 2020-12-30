using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.TestCaller.Models
{
    public class LspVerificationResponse
    {
        [JsonProperty("lsp_id")]
        public string LspId { get; set; }

        [JsonProperty("lsp_user_id")]
        public string LspUserId { get; set; }

        [JsonProperty("ref_code")]
        public string RefCode { get; set; }

        [JsonProperty("expired_at")]
        public string ExpiredAt { get; set; }
    }
}
