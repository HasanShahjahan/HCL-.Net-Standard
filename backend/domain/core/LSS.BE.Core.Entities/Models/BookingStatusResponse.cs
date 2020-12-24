using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;

namespace LSS.BE.Core.Entities.Models
{
    public class BookingStatusResponse : ValidationError
    {
        [JsonProperty("booking_id")]
        public int  BookingId { get; set; }

        [JsonProperty("status")]
        public int BookingStatus { get; set; }

    }
}
