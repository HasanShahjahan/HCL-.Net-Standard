using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class SendOtp
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("lsp_id")]
        public string LspId { get; set; }

        [JsonProperty("booking_id")]
        public string BookingId { get; set; }

        

        
    }
}
