using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class ChangeLockerSizeResponse
    {
        [JsonProperty("is_request_success")]
        public bool IsRequestSuccess { get; set; }

        [JsonProperty("booking_id")]
        public int BookingId { get; set; }

        [JsonProperty("assigned_lockers")]
        public Array AssignedLockers { get; set; }

        [JsonProperty("locker_preview")]
        public Array LockerPreview { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("error_details")]
        public string ErrorDetails { get; set; }

    }
}
