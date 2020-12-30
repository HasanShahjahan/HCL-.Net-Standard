using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LSS.BE.Core.Entities.Models
{
    public class LockerResponse : ValidationError
    {

        [JsonProperty("booking_id")]
        public int BookingId { get; set; }

        [JsonProperty("assigned_lockers")]
        public List<int> AssignedLockers { get; set; }

        [JsonProperty("hardware_door_number")]
        public string HardwareDoorNumber { get; set; }

        [JsonProperty("locker_preview")]
        public List<List<List<CompartmentDetail>>> LockerPreview { get; set; }
    }
}
