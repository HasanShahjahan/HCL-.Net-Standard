using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class LspUserAccess
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationid  { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("pin")]
        public string Pin { get; set; }
    }
}
