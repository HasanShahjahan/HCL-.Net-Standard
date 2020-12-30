using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Domain.Helpers
{
    /// <summary>
    ///   Represents generic serialize for any class.
    ///</summary>
    public class SerializerHelper<T> where T : class
    {
        /// <summary>
        /// Generic serialize object by class.
        /// </summary>
        /// <returns>
        ///  Gets the string result. 
        /// </returns>
        public static string SerializeObject(T request)
        {
            var result = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            return result;
        }
    }
}
