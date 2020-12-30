using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class OpeningHours
    {
        public OpeningHours()
        {
            Open = string.Empty;
            Close = string.Empty;
        }
        public OpeningHours(string open, string close)
        {
            Open = open;
            Close = close;
        }

        [JsonProperty("open")]
        public string Open { get; set; }

        [JsonProperty("close")]
        public string Close { get; set; }
    }
}
