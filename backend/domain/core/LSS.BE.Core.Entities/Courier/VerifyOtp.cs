using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class VerifyOtp
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationid { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("ref_code")]
        public string RefCode { get; set; }

        [JsonProperty("lsp_id")]
        public string LspId { get; set; }

        [JsonProperty("booking_id")]
        public string BookingId { get; set; }
    }
}
