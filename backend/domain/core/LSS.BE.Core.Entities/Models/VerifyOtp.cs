using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class VerifyOtp
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("ref_code")]
        public string RefCode { get; set; }

        [JsonProperty("lsp_id")]
        public string LspId { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
