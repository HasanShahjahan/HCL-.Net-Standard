using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;

namespace LSS.BE.Core.Entities.Models
{
    public class VerifyOtpResponse : ValidationError
    {
        [JsonProperty("ref_code")]
        public string RefCode { get; set; }
    }
}
