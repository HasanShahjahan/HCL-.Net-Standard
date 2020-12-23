using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class LockerStationDetailsResponse : ValidationError
    {

        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("languages")]
        public Languages Languages { get; set; }

        [JsonProperty("otp_collect")]
        public bool OtpCollect { get; set; }

        [JsonProperty("otp_return")]
        public bool OtpReturn { get; set; }

        [JsonProperty("is_mrt")]
        public bool IsMrt { get; set; }

        [JsonProperty("hardware")]
        public Hardware Hardware { get; set; }

        [JsonProperty("open_hours")]
        public OpeningHours OpeningHours { get; set; }


    }
}
