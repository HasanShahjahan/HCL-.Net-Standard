using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Common.Exceptions
{
    public class ApplicationException
    {
        [JsonProperty("error")]
        public string Error { get; set; }

       [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

       [JsonProperty("message")]
        public string Message { get; set; }

    }
}
