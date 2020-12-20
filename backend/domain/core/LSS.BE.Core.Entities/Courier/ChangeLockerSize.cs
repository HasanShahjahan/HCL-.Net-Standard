using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class ChangeLockerSize
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationLd { get; set; }

        [JsonProperty("lsp_id")]
        public string LspId { get; set; }

        [JsonProperty("booking_id")]
        public string BookingId { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }
    }
}
