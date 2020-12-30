using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class Hardware
    {
        public Hardware()
        {
            MqttHost = string.Empty;
            Address = string.Empty;
            LockerId = string.Empty;
            Token = string.Empty;
            TokenExp = string.Empty;
        }
        public Hardware(string mqttHost, string address, string lockerId, string token, string tokenExp)
        {
            MqttHost = mqttHost;
            Address = address;
            LockerId = lockerId;
            Token = token;
            TokenExp = tokenExp;
        }

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
