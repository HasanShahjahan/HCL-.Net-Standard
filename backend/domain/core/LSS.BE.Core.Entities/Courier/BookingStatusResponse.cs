using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;

namespace LSS.BE.Core.Entities.Courier
{
    public class BookingStatusResponse : ValidationError
    {
        [JsonProperty("booking_id")]
        public int  BookingId { get; set; }

        [JsonProperty("status")]
        public int BookingStatus { get; set; }

    }
}
