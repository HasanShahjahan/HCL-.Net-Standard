using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class Languages
    {
        public Languages()
        {
            English = string.Empty;
            Melaya = string.Empty;
            中文 = string.Empty;
            தமிழ் = string.Empty;
        }
        public Languages(string english, string melaya, string _中文, string _தமிழ்)
        {
            English = english;
            Melaya = melaya;
            中文 = _中文;
            தமிழ் = _தமிழ்;
        }

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
