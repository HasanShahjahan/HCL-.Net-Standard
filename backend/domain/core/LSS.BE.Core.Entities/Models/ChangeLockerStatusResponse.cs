using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;
using System;

namespace LSS.BE.Core.Entities.Models
{
    public class ChangeLockerStatusResponse : ValidationError
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("hwapi_id")]
        public string HwapiId{ get; set; }

        [JsonProperty("door_number")]
        public int  DoorNumber { get; set; }

        [JsonProperty("status")]
        public int LockerStatus { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
