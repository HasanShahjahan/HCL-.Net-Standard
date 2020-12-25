using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Entities.Models
{
    public class CompartmentDetail
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("module_door")]
        public string ModuleDoor { get; set; }
    }
}
