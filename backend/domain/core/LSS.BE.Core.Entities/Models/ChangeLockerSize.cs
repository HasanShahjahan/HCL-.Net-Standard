using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class ChangeLockerSize
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("booking_id")]
        public string BookingId { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }
    }
}
