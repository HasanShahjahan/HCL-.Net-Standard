using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class BookingStatus
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("booking_id")]
        public int BookingId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("lsp_id")]
        public string LspId { get; set; }

        [JsonProperty("lsp_user_id")]
        public string LspUserId { get; set; }

        [JsonProperty("mobile_number")]
        public string MobileNumber { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
