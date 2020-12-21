using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;

namespace LSS.BE.Core.Entities.Courier
{
    public class LspUserAccessResponse : ValidationError
    {
        [JsonProperty("LspId")]
        public string LspId { get; set; }

        [JsonProperty("ref_code")]
        public string RefCode { get; set; }

        [JsonProperty("expired_at")]
        public string ExpiredAt { get; set; }
    }
}
