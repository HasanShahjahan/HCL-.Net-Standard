using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;

namespace LSS.BE.Core.Entities.Models
{
    public class LspUserAccessResponse : ValidationError
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
