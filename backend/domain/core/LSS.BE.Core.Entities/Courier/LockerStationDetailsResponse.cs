using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class LockerStationDetailsResponse
    {
        [JsonProperty("is_request_success")]
        public bool IsRequestSuccess { get; set; }

        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("languages")]
        public Languages Languages { get; set; }

        [JsonProperty("otp_collect")]
        public bool OtpCollect { get; set; }

        [JsonProperty("otp_return")]
        public bool OtpReturn { get; set; }

        [JsonProperty("hardware")]
        public Hardware Hardware { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("error_details")]
        public string ErrorDetails { get; set; }
    }
}
