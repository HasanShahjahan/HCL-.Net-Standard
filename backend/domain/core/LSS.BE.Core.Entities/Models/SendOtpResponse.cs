using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class SendOtpResponse : ValidationError
    {
        [JsonProperty("ref_code")]
        public string RefCode { get; set; }
    }
}
