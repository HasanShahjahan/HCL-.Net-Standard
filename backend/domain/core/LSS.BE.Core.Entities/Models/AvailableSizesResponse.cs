using LSS.BE.Core.Common.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LSS.BE.Core.Entities.Models
{
    public class AvailableSizesResponse : ValidationError
    {
        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }
       
    }
}
