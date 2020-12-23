using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class Languages
    {

        [JsonProperty("English")]
        public string English { get; set; }

        [JsonProperty("Melaya")]
        public string Melaya { get; set; }

        [JsonProperty("中文")]
        public string 中文 { get; set; }

        [JsonProperty("தமிழ்")]
        public string தமிழ் { get; set; }
    }
}
