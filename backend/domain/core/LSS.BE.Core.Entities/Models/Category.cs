using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class Category
    {
        public Category()
        {
            Size = string.Empty;
            Name = string.Empty;
            Width = 0;
            Height = 0;
            Length = 0;
            SizeOrder = 0;
            AvailableLockerTotal = 0;
        }
        public Category(string size, string name, float width, float height, float length, int sizeOrder, int availableLockerTotal)
        {
            Size = size;
            Name = name;
            Width = width;
            Height = height;
            Length = length;
            SizeOrder = sizeOrder;
            AvailableLockerTotal = availableLockerTotal;
        }

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
