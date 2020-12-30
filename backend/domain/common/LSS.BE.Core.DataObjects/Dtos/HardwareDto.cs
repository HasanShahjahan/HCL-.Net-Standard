using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class HardwareDto
    {
        public HardwareDto()
        {
            MqttHost = string.Empty;
            Address = string.Empty;
            LockerId = string.Empty;
            Token = string.Empty;
            TokenExp = string.Empty;
        }
        public HardwareDto(string mqttHost, string address, string lockerId, string token, string tokenExp) 
        {
            MqttHost = mqttHost;
            Address = address;
            LockerId = lockerId;
            Token = token;
            TokenExp = tokenExp;
        }
        public string MqttHost { get; set; }
        public string Address { get; set; }
        public string LockerId { get; set; }
        public string Token { get; set; }
        public string TokenExp { get; set; }
    }
}
