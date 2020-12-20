using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class AvailableSizesResponse
    {
        [JsonProperty("is_request_success")]
        public bool IsRequestSuccess { get; set; }

        [JsonProperty("categories")]
        public Categories Categories { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("error_details")]
        public string ErrorDetails { get; set; }
    }
}
