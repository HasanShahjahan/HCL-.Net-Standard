using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class BookingStatusResponse
    {

        [JsonProperty("is_request_success")]
        public bool IsRequestSuccess { get; set; }

        [JsonProperty("booking_id")]
        public int  BookingId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error_message")]
        public int ErrorMessage { get; set; }

        [JsonProperty("error_details")]
        public int ErrorDetails { get; set; }

    }
}
