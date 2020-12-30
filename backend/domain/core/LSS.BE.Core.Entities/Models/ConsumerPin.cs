using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class ConsumerPin
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("pin")]
        public string Pin { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
