using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;
using System;

namespace LSS.BE.Core.Entities.Models
{
    public class ChangeLockerSizeResponse : ValidationError
    {

        [JsonProperty("booking_id")]
        public int BookingId { get; set; }

        [JsonProperty("assigned_lockers")]
        public Array AssignedLockers { get; set; }

        [JsonProperty("locker_preview")]
        public Array LockerPreview { get; set; }
    }
}
