using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Courier
{
    public class Categories
    {

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("width")]
        public float Width { get; set; }

        [JsonProperty("height")]
        public float Height { get; set; }

        [JsonProperty("length")]
        public float Length { get; set; }

        [JsonProperty("size_order")]
        public int SizeOrder { get; set; }

        [JsonProperty("available_locker_total")]
        public int AvailableLockerTotal { get; set; }
    }
}
