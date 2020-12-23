using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class Hardware
    {
        [JsonProperty("mqtt_host")]
        public string MqttHost { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("locker_id")]
        public string LockerId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("token_exp")]
        public string TokenExp { get; set; }
    }
}
