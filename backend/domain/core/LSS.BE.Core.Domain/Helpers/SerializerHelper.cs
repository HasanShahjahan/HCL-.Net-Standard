using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Domain.Helpers
{
    public class SerializerHelper<T> where T : class
    {
        public static string SerializeObject(T request)
        {
            var result = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            return result;
        }
    }
}
