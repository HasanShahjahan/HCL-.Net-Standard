using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class ChangeLockerStatus
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("hwapi_id")]
        public int HwapiId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
