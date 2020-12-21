using Newtonsoft.Json;

namespace LSS.BE.Core.Common.Base
{
    public class AccessToken 
    {
        [JsonProperty("token_type")]
        public string Type { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}
