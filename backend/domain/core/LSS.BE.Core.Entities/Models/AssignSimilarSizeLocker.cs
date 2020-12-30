using Newtonsoft.Json;

namespace LSS.BE.Core.Entities.Models
{
    public class AssignSimilarSizeLocker
    {
        [JsonProperty("locker_station_id")]
        public string LockerStationId { get; set; }

        [JsonProperty("booking_id")]
        public int BookingId { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
