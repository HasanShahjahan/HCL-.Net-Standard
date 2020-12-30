using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Common.Base
{
    public class ErrorDetails
    {
        public ErrorDetails() 
        {
            LockerStationId = null;
            LspId = null;
            BookingId = null;
        }

        [JsonProperty("locker_station_id")]
        public string[] LockerStationId { get; set; }

        [JsonProperty("LspId")]
        public string[] LspId { get; set; }

        [JsonProperty("booking_id")]
        public string[] BookingId { get; set; }
    }
}
